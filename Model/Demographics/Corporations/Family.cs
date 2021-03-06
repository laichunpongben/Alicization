﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alicization.Model.Demographics.Players;
using Alicization.Model.Geographics.Locations;
using Alicization.Model.GlobalObjects;
using Alicization.Model.Markets;
using Alicization.Model.Wealth;
using Alicization.Model.Wealth.Assets;

namespace Alicization.Model.Demographics.Corporations
{
    internal class Family : ICorporation
    {
        public int entityId { get; private set; }
        public string entityName { get; private set; }
        public EntityType entityType { get; private set; }
        public int dateOfBirth { get; private set; }
        public int memberCount { get; private set; }
        internal Player maleOwner { get; private set; }
        internal Player femaleOwner { get; private set; }

        internal WealthWallet wealth { get; private set; }

        internal Family(ILocation location)
        {
            entityId = World.families.MaxId() + 1;
            entityName = "f" + entityId.ToString();
            entityType = EntityType.Family; 
            dateOfBirth = Time.Now();
            memberCount = 0;
            JoinFamilies();
            UpdateFamilies();
            AddResidence(location);
        }

        internal static void CreateNewFamily(Player player1, Player player2)
        {
            ILocation location1 = player1.GetResidence();
            ILocation location2 = player2.GetResidence();

            ILocation location = null;
            if (location1 == location2) {
                location = location1;
            }

            Family family = new Family(location);
            family.memberCount = 2;
            family.AddAccessRight(player1);
            family.AddAccessRight(player2);
            family.AddCorporationOwnership(player1);
            family.AddCorporationOwnership(player2);

            if (player1.gender == Player.Gender.Male) {
                family.maleOwner = player1;
                family.femaleOwner = player2;
            } else {
                family.maleOwner = player2;
                family.femaleOwner = player1;
            }
        }

        internal static void MatchCouple(Player player1, Player player2)
        {
            player1.RemoveAccessRights();
            player2.RemoveAccessRights();
            CreateNewFamily(player1, player2);
            player1.SetMarried();
            player2.SetMarried();
            player1.UpdateEntityUtility(player2, SocialRelationshipType.Marriage);
            player2.UpdateEntityUtility(player1, SocialRelationshipType.Marriage);
        }

        internal static void MatchManyCouples(IList<Player> males, IList<Player> females)
        {
            if (males.Count == females.Count) {
                for (int i = 0; i < males.Count; i++) {
                    MatchCouple(males[i], females[i]);
                }
            }
        }

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
            World.corporationAccessRights.Add(accessRight, CorporationAccessRightType.ReadWrite);
        }

        private void AddCorporationOwnership(IEntity entity)
        {
            CorporationEntity corporationOwnership = new CorporationEntity(this, entity);
            World.corporationOwnerships.Add(corporationOwnership);
        }

        public void JoinFamilies()
        {
            World.families.Add(this);
        }

        public void UpdateFamilies()
        {
            World.families.IncrementFamilyCount();
        }

        public ILocation GetResidence()
        {
            return World.residences.GetLocation(this);
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
