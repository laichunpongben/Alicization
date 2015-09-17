using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicization.Model.Geographics.Locations;
using Alicization.Model.Wealth.Assets.Commodities;
using Alicization.Model.Wealth.Assets;
using Alicization.Util.Numerics;
using Alicization.Model.Wealth;
using Alicization.Model.Demographics.Corporations;
using System.Diagnostics;

namespace Alicization.Model.GlobalObjects
{
    public class Nature : IVirtualEntity, IGlobalObject
    {
        private static Nature instance = null;

        public int entityId { get; private set; }
        public string entityName { get; private set; }
        public EntityType entityType { get; private set; }
        public int dateOfBirth { get; private set; }

        private Nature()
        {
            ; //singleton
        }

        internal static Nature GetInstance()
        {
            if (instance == null) {
                instance = new Nature();
            }
            return instance;
        }

        internal void Initialize()
        {
            entityId = 0;
            entityName = "Nature";
            entityType = EntityType.Nature;
            dateOfBirth = 0;
        }

        public int Count()
        {
            return 1;
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

        public void AccessPhysicalAsset(ICorporation target, IPhysicalAsset asset, double quantity)
        {
            if (World.corporationAccessRights.IsAccessible(this, target)) {
                target.TransferPhysicalAsset(this, asset, quantity);
            }
        }

        public void AccessPhysicalAsset(ICorporation target, ILocation location, IPhysicalAsset asset, double quantity)
        {
            if (World.corporationAccessRights.IsAccessible(this, target)) {
                target.TransferPhysicalAsset(this, location, asset, quantity);
            }
        }

        public void AccessVirtualAsset(ICorporation target, IVirtualAsset asset, double quantity)
        {
            if (World.corporationAccessRights.IsAccessible(this, target)) {
                target.TransferVirtualAsset(this, asset, quantity);
            }
        }

        public bool HasSufficientLocalInventories(ILocation location, IPhysicalAsset asset, double quantity)
        {
            return World.physicalAssetOwnerships.IsSufficientLocalInventories(this, location, asset, quantity);
        }

        public bool HasSufficientVirtualAsset(IVirtualAsset asset, double quantity)
        {
            return World.virtualAssetOwnerships.IsSufficient(this, asset, quantity);
        }

        internal void CreateNaturalResources()
        {
            foreach (var i in World.locations.dict) {
                foreach (var j in World.assets.commodities) {
                    ILocation location = i.Value;
                    IPhysicalAsset commodity = (IPhysicalAsset) j.Value;
                    AssetLocation aLoc = new AssetLocation(commodity, location);
                    EntityAssetLocation entityAssetLocation = new EntityAssetLocation(this, aLoc);
                    double qty = Randomness.SampleInt(1000000); //to supply global population
                    World.physicalAssetOwnerships.Add(entityAssetLocation, qty);
                }
            }
        }

        private IPhysicalAsset SampleNaturalResource()
        {
            int assetId = Randomness.SampleInt(1, 14);
            return World.assets.GetCommodity(assetId);
        }
    }
}
