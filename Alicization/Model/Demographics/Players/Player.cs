using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alicization.Util.Numerics;
using Alicization.Model.Demographics.Corporations;
using Alicization.Model.Geographics.Locations;
using Alicization.Model.GlobalObjects;
using Alicization.Model.Wealth.Assets;
using Alicization.Model.Markets;
using Alicization.Model.Wealth;
using Alicization.Model.Wealth.Assets.Commodities;

namespace Alicization.Model.Demographics.Players
{
    public class Player : IPhysicalEntity, IWealthEntity
    {
        public int entityId { get; private set; }
        public string entityName { get; private set; }
        public EntityType entityType { get; private set; }
        public int dateOfBirth { get; private set; }
        private int dateOfDeath { get; set; }
        private DecrementType causeOfDeath { get; set; }
        internal Gender gender { get; private set; }
        internal ILocation birthPlace { get; private set; }
        internal MartialStatus martialStatus { get; set; }
        internal bool isDead { get; private set; }
        public bool isEmpty { get; set; }

        private double salary { get; set; }

        internal Gene gene { get; set; }
        internal Hygiene hygiene { get; private set; }
        internal WealthWallet wealth { get; private set; }
        internal ScienceWallet science { get; private set; }
        internal CultureWallet culture { get; private set; }
        internal CredibilityWallet credibility { get; private set; }
        internal Score score { get; private set; }

        internal Skills skill { get; set; }
        private PlayerActivityPlan activityPlan { get; set; }

        internal enum Gender { Male, Female };
        internal enum MartialStatus { Single, Married };

        internal Player()
        {
            gender = SampleGender();
            birthPlace = World.locations.Sample();
            gene = new Gene();

            StartLifeCommon();
            UpdatePlayersAtStartLifeCommon();
        }

        internal Player(Gender gen)
        {
            gender = gen;
            birthPlace = World.locations.Sample();
            gene = new Gene();

            StartLifeCommon();
            UpdatePlayersAtStartLifeCommon();
        }

        internal Player(Family family)
        {
            gender = SampleGender();
            birthPlace = family.GetResidence(); //follow family
            gene = new Gene(family.maleOwner.gene, family.femaleOwner.gene, 0.05); //m + f

            StartLifeCommon();
            AddFamily(family);
            UpdatePlayersAtStartLifeCommon();
        }

        private void StartLifeCommon()
        {
            entityId = World.players.MaxId() + 1;
            entityName = "p" + entityId.ToString();
            entityType = EntityType.Player;
            dateOfBirth = Time.Now();
            martialStatus = MartialStatus.Single;
            //salary = SampleSalary(10000);
            JoinPlayers();
            AddResidence(birthPlace);
            
            hygiene = new Hygiene();
            wealth = new WealthWallet();
            science = new ScienceWallet();
            culture = new CultureWallet();
            credibility = new CredibilityWallet();
            score = new Score();
            skill = new Skills();
            activityPlan = new PlayerActivityPlan();
            AddPublicDomain();
            
        }

        private void UpdatePlayersAtStartLifeCommon()
        {
            switch (gender) {
                case Gender.Male:
                    World.players.IncrementMaleCount();
                    break;
                case Gender.Female:
                    World.players.IncrementFemaleCount();
                    break;
                default:
                    break;
            }

            World.players.IncrementSingleCount();
        }

        internal void EndLife(DecrementType cause)
        {
            dateOfDeath = Time.Now();
            causeOfDeath = cause;
            isDead = true;
            JoinDeadPlayers();
            RemoveAccessRights();
            RemoveCorporationOwnerships();
            UpdatePlayersAtEndLifeCommon();
            UpdateFinalScore();
        }

        private void UpdatePlayersAtEndLifeCommon()
        {
            switch (gender) {
                case Gender.Male:
                    World.players.DecrementMaleCount();
                    break;
                case Gender.Female:
                    World.players.DecrementFemaleCount();
                    break;
                default:
                    break;
            }

            switch (martialStatus) {
                case MartialStatus.Single:
                    World.players.DecrementSingleCount();
                    break;
                case MartialStatus.Married:
                    World.players.DecrementMarriedCount();
                    break;
                default:
                    break;
            }
        }

        private void UpdateFinalScore()
        {
            score.UpdateFinishedScore(this);
        }

        internal void PlayTurn()
        {
            Upkeep();
            Act();
            Cleanup();
        }

        private void Upkeep()
        {
            hygiene.PassiveDeteriorate();
            this.CheckHygiene();
        }

        private void Act()
        {
            //act until action points are used up
            //LearnHarvesting();

            ////int typeCount = (int) Math.Floor(skill.harvestingSkillLevel);
            //int typeCount = 2;
            //int[] assetIds = Randomness.Draw(typeCount, 1, 10);
            //foreach (var i in assetIds) {
            //    ICommodity commodity = (ICommodity) World.assets.GetCommodity(i);
            //    Harvest(commodity, 1);
            //    //Consume(commodity, 1);
            //}

            foreach (var i in activityPlan.dict) {
                if (i.Value > 0) {
                    for (int j =0; j < i.Value; j++) {
                        this.DoActivity(i.Key);
                    }
                }
            }
            
        }

        private void Cleanup()
        {
            score.UpdateRunningScore(this);
        }

        internal Family GetFamily()
        {
            return World.corporationAccessRights.GetPlayerFamily(this);
        }

        internal void SetMarried()
        {
            martialStatus = Player.MartialStatus.Married;
            World.players.DecrementSingleCount();
            World.players.IncrementMarriedCount();
        }

        private Gender SampleGender()
        {
            Bernoulli bernoulli = new Bernoulli(0.5);
            return (bernoulli.Sample() == 0) ? Gender.Male : Gender.Female;
        }

        private double SampleSalary(double meanSalary)
        {
            Poisson poisson = new Poisson(meanSalary);
            return poisson.Sample();
        }

        private void JoinPlayers()
        {
            World.players.Add(this);
        }

        private void LeavePlayers() // iteration bug. Do not use
        {
            World.players.Remove(this);
        }

        private void JoinDeadPlayers()
        {
            World.players.AddDead(this);
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
            return (!isDead) ? Time.Now() - dateOfBirth : dateOfDeath - dateOfBirth;
        }

        public double ComputeAgeInYear()
        {
            return (double) ComputeAge() / Time.turnCountInOneYear ;
        }

        private void AddFamily(Family family)
        {
            family.AddMember(this);
        }

        private void AddPhysicalAsset(IPhysicalAsset asset, double qty)
        {
            ILocation residence = GetResidence();
            AssetLocation pair = new AssetLocation(asset, residence);
            EntityAssetLocation assetOwnership = new EntityAssetLocation(this, pair);
            World.physicalAssetOwnerships.Add(assetOwnership, qty);
        }

        public WealthWallet GetWealthWallet()
        {
            return wealth;
        }

        public double GetWealthWalletBalance()
        {
            return wealth.balance;
        }

        private void CreateSkills()
        {

        }

        private void CreateHistory()
        {

        }

        private void AddPublicDomain()
        {
            World.publicDomain.AddMember(this);
        }

        internal void RemoveAccessRights()
        {
            World.corporationAccessRights.RemoveEntityAccessRights(this);
        }

        private void RemoveCorporationOwnerships()
        {
            World.corporationOwnerships.RemoveEntity(this);
        }

        public ILocation GetResidence()
        {
            return World.residences.GetLocation(this);
        }

        

        

        internal void CheckDecay(ICommodity commodity, double quantity, AssetLocation aLoc)
        {
            double decayQuantity = commodity.SampleConsumptionDecay(quantity);
            int truncatedQuantity = (int) Math.Floor(decayQuantity);
            if (truncatedQuantity > 0) World.physicalAssetOwnerships.RemoveQuantity(this, aLoc, truncatedQuantity);
        }

        private void Move(ILocation destination)
        {
            ChangeResidence(destination);
        }

        

        private void Educate(IEntity target)
        {

        }

        

        private void Harvest(ICommodity naturalResource, double quantity) //deterministic?
        {
            if (World.nature.HasSufficientLocalInventories(GetResidence(), naturalResource, quantity)) {
                double harvestSize = quantity;
                World.nature.TransferPhysicalAsset(this, GetResidence(), naturalResource, harvestSize);
            }
        }

        private void Produce(IAsset asset, double qty)
        {
            //var bom = 
            //bool isMaterialReady = World.inventories.CheckIfSufficientMaterials(this, 
            //AddAsset(asset, qty);
        }

        private void Farm()
        {

        }

        private void Work()
        {
            wealth.Earn(Math.Max(Math.Log(skill.harvestingSkillLevel), 0));
        }

        private void BidPhysicalAsset(IPhysicalAsset asset, double quantity, double price)
        {
            double amount = quantity * price;
            if (HasSufficientCash(amount)) {
                AssetLocation aLoc = new AssetLocation(asset, GetResidence());
                EntityAssetLocation entityAssetLocation = new EntityAssetLocation(this, aLoc);
                PhysicalAssetMarketOrder bidOrder = new PhysicalAssetMarketOrder(Time.Now(), entityAssetLocation, quantity, price, true);
                World.physicalAssetMarket.AddAssetBidOrder(bidOrder);
            }
        }

        private void AskPhysicalAsset(IPhysicalAsset asset, double quantity, double price)
        {
            if (HasSufficientLocalInventories(asset, quantity)) {
                AssetLocation aLoc = new AssetLocation(asset, GetResidence());
                EntityAssetLocation entityAssetLocation = new EntityAssetLocation(this, aLoc);
                PhysicalAssetMarketOrder askOrder = new PhysicalAssetMarketOrder(Time.Now(), entityAssetLocation, quantity, price, true);
                World.physicalAssetMarket.AddAssetAskOrder(askOrder);
            }
        }

        public void TransferPhysicalAsset(IEntity target, IPhysicalAsset asset, double quantity)
        {
            if (HasSufficientLocalInventories(asset, quantity)) {
                EntityDouble entityDouble = new EntityDouble(this, target);
                AssetLocation aLoc = new AssetLocation(asset, GetResidence());
                World.physicalAssetOwnerships.TransferOwnership(entityDouble, aLoc, quantity); 
            }
        }

        public void TransferPhysicalAsset(IEntity target, ILocation location, IPhysicalAsset asset, double quantity)
        {
            if (HasSufficientLocalInventories(location, asset, quantity)) {
                EntityDouble entityDouble = new EntityDouble(this, target);
                AssetLocation aLoc = new AssetLocation(asset, location);
                World.physicalAssetOwnerships.TransferOwnership(entityDouble, aLoc, quantity); 
            }
        }

        public void TransferVirtualAsset(IEntity target, IVirtualAsset asset, double quantity)
        {
            if (HasSufficientVirtualAsset(asset, quantity)) {
                EntityDouble entityDouble = new EntityDouble(this, target);
                World.virtualAssetOwnerships.TransferOwnership(entityDouble, asset, quantity);
            }
        }

        public void RemovePhysicalAsset()
        {

        }

        public void AccessPhysicalAsset(ICorporation target, IPhysicalAsset asset, double quantity)
        {
            if (IsAccessible(target)) target.TransferPhysicalAsset(this, asset, quantity);
        }

        public void AccessPhysicalAsset(ICorporation target, ILocation location, IPhysicalAsset asset, double quantity)
        {
            if (IsAccessible(target)) target.TransferPhysicalAsset(this, location, asset, quantity);
        }

        public void AccessVirtualAsset(ICorporation target, IVirtualAsset asset, double quantity)
        {
            if (IsAccessible(target)) target.TransferVirtualAsset(this, asset, quantity);
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

        private void AccessInformation(ICorporation target, IVirtualAsset asset, double quantity)
        {

        }

        private void Trade()
        {

        }
        
        private void Invest()
        {
            //check if rational to buy one asset
        }

        internal void Donate(IWealthEntity target, double isk)
        {
            if (HasSufficientCash(isk) && isk >= 0) {
                WealthEntityDouble pair = new WealthEntityDouble(this, target);
                Donation donation = new Donation(pair, -1 * isk); // reverse money direction
            } 
        }

        internal void BidSocialRelationship(SocialRelationshipType socialRelationshipType, int qty)
        {
            switch (socialRelationshipType) {
                case SocialRelationshipType.Marriage:
                    BidMarriage();
                    break;
                case SocialRelationshipType.Friend:
                    CreateSocialMarketOrder(socialRelationshipType, qty);
                    break;
                case SocialRelationshipType.Acquaintance:
                    CreateSocialMarketOrder(socialRelationshipType, qty);
                    break;
                default:
                    CreateSocialMarketOrder(socialRelationshipType, qty);
                    break;
            }
        }

        private void BidMarriage()
        {
            if (martialStatus == MartialStatus.Single) {
                switch (gender) {
                    case Gender.Male:
                        SocialMarketOrder orderMarriageMale = new SocialMarketOrder(this, SocialRelationshipType.Marriage, 1);
                        World.socialMarket.AddOrderMarriageMale(orderMarriageMale);
                        break;
                    case Gender.Female:
                        SocialMarketOrder orderMarriageFemale = new SocialMarketOrder(this, SocialRelationshipType.Marriage, 1);
                        World.socialMarket.AddOrderMarriageFemale(orderMarriageFemale);
                        break;
                    default:
                        break;
                }
            }
        }

        private void CreateSocialMarketOrder(SocialRelationshipType socialRelationshipType, int qty)
        {
            SocialMarketOrder order = new SocialMarketOrder(this, socialRelationshipType, qty);
            World.socialMarket.AddOrder(order);
        }

        internal void UpdateSocialRelationshipType(IEntity target, SocialRelationshipType type)
        {
            EntityDouble pair = new EntityDouble(this, target);
            World.socialRelationships.AddOrUpdateRelationshipType(pair, type);
        }

        internal void SayHello()
        {
            Debug.WriteLine("Hello! My name is " + entityName);
        }

        internal void UpdateEntityUtility(IEntity target, SocialRelationshipType relationshipType)
        {
            EntityDouble pair = new EntityDouble(this, target);
            World.socialRelationships.AddOrUpdateRelationshipType(pair, relationshipType);
        }

        public string ReportStatus()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Name: " + this.entityName);
            sb.AppendLine("Gender: " + this.gender);
            sb.AppendLine("Life: " + ReportLifeStatus());
            sb.AppendLine("Age: " + ReportAge());
            sb.AppendLine("Cause of death: " + ReportCauseOfDeath());
            sb.AppendLine("Martial status: " + this.martialStatus);
            sb.AppendLine("Family: " + ReportFamily());
            sb.AppendLine("Wallet: " + GetWealthWalletBalance().ToString("#,##0.00"));
            sb.AppendLine("Residence: " + GetResidence().locationName);
            sb.AppendLine("Skill: " + skill.harvestingSkillLevel);
            sb.AppendLine("Score: " + ReportScore());
            return sb.ToString();
        }

        private string ReportLifeStatus()
        {
            return (isDead) ? "Dead" : "Alive";
        }

        private string ReportAge()
        {
            return ComputeAge().ToString() + " / " + ComputeAgeInYear().ToString("#,##0.00") + " yrs";
        }

        private string ReportCauseOfDeath()
        {
            return causeOfDeath.ToString();
        }

        private string ReportFamily()
        {
            if (GetFamily() != null) {
                return GetFamily().entityName;
            } else {
                return "N/A";
            }
        }

        private string ReportScore()
        {
            return score.ComputeScore(this).ToString("#,##0.00");
        }
    }
}
