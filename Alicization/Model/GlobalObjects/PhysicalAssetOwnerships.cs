using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicization.Util.Extensions;
using Alicization.Model.Wealth.Assets;
using Alicization.Model.Geographics.Locations;
using Alicization.Model.Markets;
using System.Diagnostics;

namespace Alicization.Model.GlobalObjects
{
    public class PhysicalAssetOwnerships : IGlobalObject
    {
        private static PhysicalAssetOwnerships instance = null;

        private ConcurrentDictionary<EntityAssetLocation, double> dict { get; set; }

        private PhysicalAssetOwnerships()
        {
            ; //singleton
        }

        internal static PhysicalAssetOwnerships GetInstance()
        {
            return (instance == null) ? instance = new PhysicalAssetOwnerships() : instance; 
        }

        internal void Initialize()
        {
            dict = new ConcurrentDictionary<EntityAssetLocation, double>();
        }

        public int Count()
        {
            return dict.Count();
        }

        internal double GetQuantity(EntityAssetLocation assetOwnership)
        {
            double v;
            return (dict.TryGetValue(assetOwnership, out v)) ? v : 0.0;
        }

        internal void Add(EntityAssetLocation assetOwnership, double quantity)
        {
            dict.AddOrUpdate(assetOwnership, quantity, (k, v) => v + quantity);
        }

        internal void Remove(EntityAssetLocation assetOwnership)
        {
            dict.Remove(assetOwnership);
        }

        private IEnumerable<KeyValuePair<EntityAssetLocation, double>> SelectEntityAssets(IEntity entity)
        {
            return dict.Where(i => i.Key.owner == entity);
        }

        private IEnumerable<KeyValuePair<EntityAssetLocation, double>> SelectExceptEntityAssets(IEntity entity)
        {
            return dict.Where(i => i.Key.owner != entity);
        }

        internal void RemoveEntityAssets(IEntity entity)
        {
            dict = SelectExceptEntityAssets(entity).ToConcurrentDictionary();
        }

        private IEnumerable<KeyValuePair<EntityAssetLocation, double>> SelectAssets(IPhysicalAsset asset)
        {
            return dict.Where(i => i.Key.assetLocation.physicalAsset == asset);
        }

        private IEnumerable<KeyValuePair<EntityAssetLocation, double>> SelectExceptAssets(IPhysicalAsset asset)
        {
            return dict.Where(i => i.Key.assetLocation.physicalAsset != asset);
        }

        internal void RemoveAsset(IPhysicalAsset asset)
        {
            dict = SelectExceptAssets(asset).ToConcurrentDictionary();
        }       

        private IEnumerable<KeyValuePair<EntityAssetLocation, double>> SelectLocalInventories(ILocation location)
        {
            return dict.Where(i => i.Key.assetLocation.location == location);
        }

        private IEnumerable<KeyValuePair<EntityAssetLocation, double>> SelectExceptLocalInventories(ILocation location)
        {
            return dict.Where(i => i.Key.assetLocation.location != location);
        }

        internal void RemoveLocation(ILocation location)
        {
            dict = SelectExceptLocalInventories(location).ToConcurrentDictionary();
        }

        private IEnumerable<KeyValuePair<EntityAssetLocation, double>> SelectEntityLocalInventories(IPhysicalEntity entity)
        {
            ILocation residence = entity.GetResidence();
            return dict.Where(i => i.Key.owner == entity && i.Key.assetLocation.location == residence);
        }

        private IEnumerable<KeyValuePair<EntityAssetLocation, double>> SelectEntityLocalInventories(IEntity entity, ILocation location)
        {
            return dict.Where(i => i.Key.owner == entity && i.Key.assetLocation.location == location);
        }

        private IDictionary<IPhysicalAsset, double> ToAssets(IEnumerable<KeyValuePair<EntityAssetLocation, double>> pAssets)
        {
            return pAssets.ToDictionary(i => i.Key.assetLocation.physicalAsset, i => i.Value);
        }

        internal bool IsSufficientLocalInventories(IPhysicalEntity entity, IPhysicalAsset asset, double requiredQuantity)
        {
            AssetLocation aLoc = new AssetLocation(asset, entity.GetResidence());
            EntityAssetLocation entityAssetLocation = new EntityAssetLocation(entity, aLoc);
            double entityQuantity = GetQuantity(entityAssetLocation);
            return (requiredQuantity <= entityQuantity);
        }

        internal bool IsSufficientLocalInventories(IEntity entity, ILocation location, IPhysicalAsset asset, double requiredQuantity)
        {
            AssetLocation aLoc = new AssetLocation(asset, location);
            EntityAssetLocation entityAssetLocation = new EntityAssetLocation(entity, aLoc);
            double entityQuantity = GetQuantity(entityAssetLocation);
            return (requiredQuantity <= entityQuantity);
        }

        internal bool IsSufficientLocalMultipleInventories(IPhysicalEntity entity, IDictionary<IPhysicalAsset, double> materials)
        {
            double entityQuantity = 0.0;
            double materialQuantity = 0.0;

            bool isAssetSufficient = false;
            bool isAllAssetsSufficient = false;

            IDictionary<IPhysicalAsset, double> entityAssets = ToAssets(SelectEntityLocalInventories(entity));

            foreach (var keyValuePair in materials) {
                entityQuantity = entityAssets[keyValuePair.Key];
                materialQuantity = materials[keyValuePair.Key];
                if (materialQuantity <= entityQuantity) {
                    isAssetSufficient = true;
                } else {
                    isAssetSufficient = false;
                }
                isAllAssetsSufficient = isAllAssetsSufficient && isAssetSufficient;
            }
            return isAllAssetsSufficient;
        }

        internal void RemoveQuantity(IEntity entity, AssetLocation aLoc, double qty)
        {
            EntityAssetLocation ownership = new EntityAssetLocation(entity, aLoc);
            Add(ownership, -1 * qty);
        }

        internal void TransferOwnership(EntityDouble pair, AssetLocation aLoc, double qty)
        {
            EntityAssetLocation fromOwnership = new EntityAssetLocation(pair.entityFrom, aLoc);
            EntityAssetLocation toOwnership = new EntityAssetLocation(pair.entityTo, aLoc);

            Add(fromOwnership, -1 * qty);
            Add(toOwnership, qty);
        }

        internal void TransferOwnershipFromMarketToBuyer(PhysicalAssetMarketTransaction marketTransaction)
        {
            EntityDouble entityDouble = new EntityDouble(World.physicalAssetMarket, marketTransaction.wealthEntityDouble.entityTo);
            TransferOwnership(entityDouble, marketTransaction.assetLocation, marketTransaction.transactionQuantity);
        }

        internal void TransferOwnershipFromSellerToMarket(PhysicalAssetMarketOrder marketOrder)
        {
            EntityDouble entityDouble = new EntityDouble(marketOrder.assetOwnership.owner, World.physicalAssetMarket);
            TransferOwnership(entityDouble, marketOrder.assetOwnership.assetLocation, marketOrder.orderQuantity);
        }

        internal void TransferOwnershipFromMarketToSeller(PhysicalAssetMarketOrder marketOrder)
        {
            // Cancel order
            EntityDouble entityDouble = new EntityDouble(World.physicalAssetMarket, marketOrder.assetOwnership.owner);
            TransferOwnership(entityDouble, marketOrder.assetOwnership.assetLocation, marketOrder.orderQuantity);
        }

    }
}
