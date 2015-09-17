using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alicization.Model.Demographics.Players;
using Alicization.Model.Geographics.Locations;
using Alicization.Model.GlobalObjects;
using Alicization.Model.Wealth;
using Alicization.Model.Wealth.Assets;

namespace Alicization.Model.Demographics.Corporations
{
    class Government : ICorporation
    {
        public int entityId { get; private set; }
        public string entityName { get; private set; }
        public EntityType entityType { get; private set; }
        public int dateOfBirth { get; private set; }
        public int memberCount { get; private set; }

        internal WealthWallet wealth { get; private set; }

        public WealthWallet GetWealthWallet()
        {
            return wealth;
        }

        public double GetWealthWalletBalance()
        {
            return wealth.balance;
        }

        public void AddMember(IEntity entity)
        {
            AddAccessRight(entity);
            memberCount++;
        }

        private void AddAccessRight(IEntity entity)
        {
            CorporationEntity accessRight = new CorporationEntity(this, entity);
            World.corporationAccessRights.Add(accessRight);
        }

        public void AddResidence(ILocation location)
        {
            World.residences.Add(this, location);
        }

        public void RemoveResidence()
        {
            World.residences.RemoveEntity(this);
        }

        public void ChangeResidence(ILocation location)
        {
            RemoveResidence();
            AddResidence(location);
        }

        public int ComputeAge()
        {
            return Time.Now() - dateOfBirth;
        }

        public ILocation GetResidence()
        {
            return World.residences.GetLocation(this);
        }

        public void TransferPhysicalAsset(IEntity target, IPhysicalAsset asset, double quantity)
        {
            if (World.physicalAssetOwnerships.IsSufficientLocalInventories(this, asset, quantity)) {
                EntityDouble entityDouble = new EntityDouble(this, target);
                AssetLocation aLoc = new AssetLocation(asset, GetResidence());
                World.physicalAssetOwnerships.TransferOwnership(entityDouble, aLoc, quantity); 
            }
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

        public bool HasSufficientCash(double amount)
        {
            return wealth.IsSufficientCash(amount);
        }

        public bool HasSufficientLocalInventories(IPhysicalAsset asset, double quantity)
        {
            return World.physicalAssetOwnerships.IsSufficientLocalInventories(this, asset, quantity);
        }

        public bool HasSufficientLocalInventories(ILocation location, IPhysicalAsset asset, double quantity)
        {
            return World.physicalAssetOwnerships.IsSufficientLocalInventories(this, location, asset, quantity);
        }

        public bool HasSufficientVirtualAsset(IVirtualAsset asset, double quantity)
        {
            return World.virtualAssetOwnerships.IsSufficient(this, asset, quantity);
        }

        public bool IsAccessible(ICorporation target)
        {
            return World.corporationAccessRights.IsAccessible(this, target);
        }
    }
}
