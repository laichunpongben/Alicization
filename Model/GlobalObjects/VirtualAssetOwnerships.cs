using Alicization.Util.Extensions;
using Alicization.Model.Wealth.Assets;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alicization.Model.GlobalObjects
{
    public class VirtualAssetOwnerships : IGlobalObject
    {
        private static VirtualAssetOwnerships instance = null;

        private ConcurrentDictionary<EntityAsset, double> dict { get; set; }

        private VirtualAssetOwnerships()
        {
            ; //singleton
        }

        internal static VirtualAssetOwnerships GetInstance()
        {
            return (instance == null) ? instance = new VirtualAssetOwnerships() : instance;
        }

        internal void Initialize()
        {
            dict = new ConcurrentDictionary<EntityAsset, double>();
        }

        public int Count()
        {
            return dict.Count();
        }

        internal double GetQuantity(EntityAsset assetOwnership)
        {
            double v;
            return (dict.TryGetValue(assetOwnership, out v)) ? v : 0.0;
        }

        internal void Add(EntityAsset assetOwnership, double quantity)
        {
            dict.AddOrUpdate(assetOwnership, quantity, (k, v) => v + quantity);
        }

        internal void Remove(EntityAsset assetOwnership)
        {
            dict.Remove(assetOwnership);
        }

        private IEnumerable<KeyValuePair<EntityAsset, double>> SelectEntityAssets(IEntity entity)
        {
            return dict.Where(i => i.Key.owner == entity);
        }

        private IEnumerable<KeyValuePair<EntityAsset, double>> SelectExceptEntityAssets(IEntity entity)
        {
            return dict.Where(i => i.Key.owner != entity);
        }

        internal void RemoveEntityAssets(IEntity entity)
        {
            dict = SelectExceptEntityAssets(entity).ToConcurrentDictionary();
        }

        private IEnumerable<KeyValuePair<EntityAsset, double>> SelectAssets(IVirtualAsset asset)
        {
            return dict.Where(i => i.Key.virtualAsset == asset);
        }

        private IEnumerable<KeyValuePair<EntityAsset, double>> SelectExceptAssets(IVirtualAsset asset)
        {
            return dict.Where(i => i.Key.virtualAsset != asset);
        }

        internal void RemoveAsset(IVirtualAsset asset)
        {
            dict = SelectExceptAssets(asset).ToConcurrentDictionary();
        }

        private IDictionary<IVirtualAsset, double> ToAssets(IEnumerable<KeyValuePair<EntityAsset, double>> vAssets)
        {
            return vAssets.ToDictionary(i => i.Key.virtualAsset, i => i.Value);
        }

        internal void TransferOwnership(EntityDouble pair, IVirtualAsset asset, double qty)
        {
            EntityAsset fromOwnership = new EntityAsset(pair.entityFrom, asset);
            EntityAsset toOwnership = new EntityAsset(pair.entityTo, asset);

            Add(fromOwnership, -1 * qty);
            Add(toOwnership, qty);
        }

        internal bool IsSufficient(IEntity entity, IVirtualAsset asset, double requiredQuantity)
        {
            EntityAsset assetOwnership = new EntityAsset(entity, asset);
            double entityQuantity = GetQuantity(assetOwnership);
            return (requiredQuantity <= entityQuantity);
        }

    }
}
