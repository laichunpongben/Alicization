using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using Alicization.Model.Wealth;
using Alicization.Model.GlobalObjects;
using Alicization.Model.Demographics.Players;
using Alicization.Model.Wealth.Assets;
using Alicization.Model.Geographics.Locations;
using Alicization.Model.Demographics.Corporations;

namespace Alicization.Model.Markets
{
    public class PhysicalAssetMarket : IMarket, IVirtualEntity, IWealthEntity
    {
        private static PhysicalAssetMarket instance = null;

        public int entityId { get; private set; }
        public EntityType entityType { get; private set; }

        internal WealthWallet wealth { get; private set; }

        internal ConcurrentDictionary<AssetLocation, BidAskOrders> orders { get; private set; }

        internal List<PhysicalAssetMarketTransaction> transactions { get; private set; }

        internal ConcurrentDictionary<AssetLocation, double> quotes { get; private set; }

        private PhysicalAssetMarket()
        {
            ; //singleton
        }

        internal static PhysicalAssetMarket GetInstance()
        {
            return (instance == null) ? instance = new PhysicalAssetMarket() : instance;
        }

        internal void Initialize()
        {
            entityId = -1;
            entityType = EntityType.Market;

            orders = new ConcurrentDictionary<AssetLocation, BidAskOrders>();

            transactions = new List<PhysicalAssetMarketTransaction>();

            quotes = new ConcurrentDictionary<AssetLocation, double>();
        }

        public WealthWallet GetWealthWallet()
        {
            return wealth;
        }

        public double GetWealthWalletBalance()
        {
            return wealth.balance;
        }

        public void TransferPhysicalAsset(IEntity target, ILocation location, IPhysicalAsset asset, double quantity)
        {
            if (World.physicalAssetOwnerships.IsSufficientLocalInventories(this, location, asset, quantity)) {
                EntityDouble entityDouble = new EntityDouble(this, target);
                AssetLocation aLoc = new AssetLocation(asset, location);
                World.physicalAssetOwnerships.TransferOwnership(entityDouble, aLoc, quantity); 
            }
        }

        public void TransferVirtualAsset(IEntity target, IVirtualAsset asset, double quantity)
        {
            if (World.virtualAssetOwnerships.IsSufficient(this, asset, quantity)) {
                EntityDouble entityDouble = new EntityDouble(this, target);
                World.virtualAssetOwnerships.TransferOwnership(entityDouble, asset, quantity);
            }
        }

        public bool HasSufficientCash(double amount)
        {
            return wealth.IsSufficientCash(amount);
        }

        public bool HasSufficientLocalInventories(ILocation location, IPhysicalAsset asset, double quantity)
        {
            return World.physicalAssetOwnerships.IsSufficientLocalInventories(this, location, asset, quantity);
        }

        public bool HasSufficientVirtualAsset(IVirtualAsset asset, double quantity)
        {
            return World.virtualAssetOwnerships.IsSufficient(this, asset, quantity);
        }

        internal void AddAssetBidOrder(PhysicalAssetMarketOrder assetMarketOrder)
        {
            AssetLocation aLoc = assetMarketOrder.assetOwnership.assetLocation;
            BidAskOrders bidAskOrders = orders[aLoc];
            bidAskOrders.AddBidOrder(assetMarketOrder);
        }

        internal void AddAssetAskOrder(PhysicalAssetMarketOrder assetMarketOrder)
        {
            AssetLocation aLoc = assetMarketOrder.assetOwnership.assetLocation;
            BidAskOrders bidAskOrders = orders[aLoc];
            bidAskOrders.AddAskOrder(assetMarketOrder);
        }

        internal void AddTransaction(PhysicalAssetMarketTransaction assetMarketTransaction)
        {
            transactions.Add(assetMarketTransaction);
        }

        private void AddOrUpdateMarketPrice(PhysicalAssetMarketTransaction assetMarketTransaction)
        {
            AssetLocation aLoc = assetMarketTransaction.assetLocation;
            double price = assetMarketTransaction.transactionPrice;
            quotes.AddOrUpdate(aLoc, price, (k, v) => price);
        }

        private IEnumerable<Tuple<int, double>> ConvertMarketPriceIndexDoubles()
        {
            var reverseAssets = World.assets.ReverseDictionary();
            var reverseLocations = World.locations.ReverseDictionary();
            return quotes.Select(i => new Tuple<int, double>(reverseAssets[i.Key.physicalAsset] * reverseLocations[i.Key.location], i.Value));
        }

        private void RenewPhysicalAssetBidOrder(ref BidAskOrders bidAskOrders, PhysicalAssetMarketOrder bidOrder, double remainingBidQuantity)
        {
            if (remainingBidQuantity > 0) {
                IPriceOrder newBidOrder = new PhysicalAssetMarketOrder(bidOrder, remainingBidQuantity);
                bidAskOrders.AddBidOrder(newBidOrder);
            }
        }

        private void RenewPhysicalAssetAskOrder(ref BidAskOrders bidAskOrders, PhysicalAssetMarketOrder askOrder, double remainingAskQuantity)
        {
            if (remainingAskQuantity > 0) {
                IPriceOrder newAskOrder = new PhysicalAssetMarketOrder(askOrder, remainingAskQuantity);
                bidAskOrders.AddAskOrder(newAskOrder);
            }
        }

        private EntityDouble GetEntityDouble(PhysicalAssetMarketOrder bidOrder, PhysicalAssetMarketOrder askOrder)
        {
            return new EntityDouble(askOrder.assetOwnership.owner, bidOrder.assetOwnership.owner);
        }

        private void MatchAndClearOrderPairAndCreateTransaction(ref BidAskOrders bidAskOrders)
        {
            PhysicalAssetMarketOrder maxBidOrder = (PhysicalAssetMarketOrder) bidAskOrders.RemoveAndGetMaxBidOrder();
            PhysicalAssetMarketOrder minAskOrder = (PhysicalAssetMarketOrder) bidAskOrders.RemoveAndGetMinAskOrder();
            double transactionQuantity = bidAskOrders.ComputeMatchingQuantity(maxBidOrder, minAskOrder);
            double transactionPrice = bidAskOrders.ComputeMidPointPrice(maxBidOrder, minAskOrder);
            double remainingBidQuantity = bidAskOrders.ComputeRemainingBidQuantity(maxBidOrder, transactionQuantity);
            double remainingAskQuantity = bidAskOrders.ComputeRemainingAskQuantity(minAskOrder, transactionQuantity);

            RenewPhysicalAssetBidOrder(ref bidAskOrders, maxBidOrder, remainingBidQuantity);
            RenewPhysicalAssetAskOrder(ref bidAskOrders, minAskOrder, remainingAskQuantity);

            EntityDouble pair = GetEntityDouble(maxBidOrder, minAskOrder);
            WealthEntityDouble wealthPair = new WealthEntityDouble(pair.entityFrom, pair.entityTo);
            PhysicalAssetMarketTransaction transaction = new PhysicalAssetMarketTransaction(Time.Now(), wealthPair, minAskOrder.assetOwnership.assetLocation, transactionQuantity, transactionPrice);
            AddTransaction(transaction);
        }

        private void ExecuteClearing(BidAskOrders bidAskOrders)
        {
            IPriceOrder maxBidOrder = bidAskOrders.GetMaxBidOrder();
            IPriceOrder minAskOrder = bidAskOrders.GetMinAskOrder();
            while (bidAskOrders.HasPriceGap(maxBidOrder, minAskOrder)) {
                MatchAndClearOrderPairAndCreateTransaction(ref bidAskOrders);
            }
        }

        private void ExecuteAllAssetsClearing()
        {
            foreach (var item in orders) {
                ExecuteClearing(item.Value);
            }
        }

    }
}
