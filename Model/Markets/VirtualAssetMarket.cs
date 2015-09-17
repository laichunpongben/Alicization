using Alicization.Model.Wealth;
using Alicization.Model.Wealth.Assets;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicization.Model.GlobalObjects;
using Alicization.Model.Demographics.Players;
using Alicization.Model.Geographics.Locations;

namespace Alicization.Model.Markets
{
    public class VirtualAssetMarket : IMarket, IVirtualEntity, IWealthEntity
    {
        private static VirtualAssetMarket instance = null;

        public int entityId { get; private set; }
        public EntityType entityType { get; private set; }

        internal WealthWallet wealth { get; private set; }

        internal ConcurrentDictionary<IVirtualAsset, BidAskOrders> orders { get; private set; }

        internal List<VirtualAssetMarketTransaction> transactions { get; private set; }

        internal ConcurrentDictionary<IVirtualAsset, double> quotes { get; private set; }

        private VirtualAssetMarket()
        {
            ; //singleton
        }

        internal static VirtualAssetMarket GetInstance()
        {
            return (instance == null) ? instance = new VirtualAssetMarket() : instance;
        }

        internal void Initialize()
        {
            entityId = -2;
            entityType = EntityType.Market;

            orders = new ConcurrentDictionary<IVirtualAsset, BidAskOrders>();

            transactions = new List<VirtualAssetMarketTransaction>();

            quotes = new ConcurrentDictionary<IVirtualAsset, double>();
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

        internal void AddAssetBidOrder(VirtualAssetMarketOrder assetMarketOrder)
        {
            IVirtualAsset vAsset = assetMarketOrder.assetOwnership.virtualAsset;
            BidAskOrders bidAskOrders = orders[vAsset];
            bidAskOrders.AddBidOrder(assetMarketOrder);
        }

        internal void AddAssetAskOrder(VirtualAssetMarketOrder assetMarketOrder)
        {
            IVirtualAsset vAsset = assetMarketOrder.assetOwnership.virtualAsset;
            BidAskOrders bidAskOrders = orders[vAsset];
            bidAskOrders.AddAskOrder(assetMarketOrder);
        }

        internal void AddTransaction(VirtualAssetMarketTransaction assetMarketTransaction)
        {
            transactions.Add(assetMarketTransaction);
        }

        private void AddOrUpdateMarketPrice(VirtualAssetMarketTransaction assetMarketTransaction)
        {
            IVirtualAsset asset = assetMarketTransaction.virtualAsset;
            double price = assetMarketTransaction.transactionPrice;
            quotes.AddOrUpdate(asset, price, (k, v) => price);
        }

        //private IEnumerable<Tuple<int, double>> ConvertMarketPriceIndexDoubles()
        //{
        //    var reverseAssets = World.assets.ReverseDictionary();
        //    var reverseLocations = World.locations.ReverseDictionary();
        //    return quotes.Select(i => new Tuple<int, double>(reverseAssets[i.Key.virtualAsset] * reverseLocations[i.Key.location], i.Value));
        //}

        private void RenewVirtualAssetBidOrder(ref BidAskOrders bidAskOrders, VirtualAssetMarketOrder bidOrder, double remainingBidQuantity)
        {
            if (remainingBidQuantity > 0) {
                IPriceOrder newBidOrder = new VirtualAssetMarketOrder(bidOrder, remainingBidQuantity);
                bidAskOrders.AddBidOrder(newBidOrder);
            }
        }

        private void RenewVirtualAssetAskOrder(ref BidAskOrders bidAskOrders, VirtualAssetMarketOrder askOrder, double remainingAskQuantity)
        {
            if (remainingAskQuantity > 0) {
                IPriceOrder newAskOrder = new VirtualAssetMarketOrder(askOrder, remainingAskQuantity);
                bidAskOrders.AddAskOrder(newAskOrder);
            }
        }

        private EntityDouble GetEntityDouble(VirtualAssetMarketOrder bidOrder, VirtualAssetMarketOrder askOrder)
        {
            return new EntityDouble(askOrder.assetOwnership.owner, bidOrder.assetOwnership.owner);
        }

        private void MatchAndClearOrderPairAndCreateTransaction(ref BidAskOrders bidAskOrders)
        {
            VirtualAssetMarketOrder maxBidOrder = (VirtualAssetMarketOrder) bidAskOrders.RemoveAndGetMaxBidOrder();
            VirtualAssetMarketOrder minAskOrder = (VirtualAssetMarketOrder) bidAskOrders.RemoveAndGetMinAskOrder();
            double transactionQuantity = bidAskOrders.ComputeMatchingQuantity(maxBidOrder, minAskOrder);
            double transactionPrice = bidAskOrders.ComputeMidPointPrice(maxBidOrder, minAskOrder);
            double remainingBidQuantity = bidAskOrders.ComputeRemainingBidQuantity(maxBidOrder, transactionQuantity);
            double remainingAskQuantity = bidAskOrders.ComputeRemainingAskQuantity(minAskOrder, transactionQuantity);

            RenewVirtualAssetBidOrder(ref bidAskOrders, maxBidOrder, remainingBidQuantity);
            RenewVirtualAssetAskOrder(ref bidAskOrders, minAskOrder, remainingAskQuantity);

            EntityDouble pair = GetEntityDouble(maxBidOrder, minAskOrder);
            WealthEntityDouble wealthPair = new WealthEntityDouble(pair.entityFrom, pair.entityTo);
            VirtualAssetMarketTransaction transaction = new VirtualAssetMarketTransaction(Time.Now(), wealthPair, minAskOrder.assetOwnership.virtualAsset, transactionQuantity, transactionPrice);
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
