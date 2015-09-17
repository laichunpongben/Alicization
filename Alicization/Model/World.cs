/* Heterogenous agents model
 * Bounded rationality
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alicization.Model.Demographics;
using Alicization.Model.Geographics;
using Alicization.Model.GlobalObjects;
using Alicization.Model.Markets;
using Alicization.Model.Demographics.Corporations;
using Alicization.Model.Demographics.Players;

namespace Alicization.Model
{
    public class World
    {
        public static Locations locations = null;
        public static Nature nature = null;
        public static PublicDomain publicDomain = null;
        public static Players players = null;
        public static Corporations corporations = null;
        public static Families families = null;
        public static Assets assets = null;
        public static PhysicalAssetMarket physicalAssetMarket = null;
        public static VirtualAssetMarket virtualAssetMarket = null;
        public static LabourMarket labourMarket = null;
        public static SocialMarket socialMarket = null;

        public static Residences residences = null;
        public static CorporationAccessRights corporationAccessRights = null;
        public static CorporationOwnerships corporationOwnerships = null;
        public static InformationOwnerships informationOwnerships = null;
        public static PhysicalAssetOwnerships physicalAssetOwnerships = null;
        public static VirtualAssetOwnerships virtualAssetOwnerships = null;
        public static SocialRelationships socialRelationships = null;

        public static Scores scores = null;

        public static Geographic geographic = null;
        public static Demographic demographic = null;

        public World()
        {
            Initialize();
        }

        public void Load()
        {

        }

        public void Reset()
        {

        }

        public void Initialize()
        {
            InitializeGlobalObjects();
            InitializeMarkets();
            
            ImplementGeographic(); 
            ImplementDemographic(); 

            //InitializeNaturalResources();
        }

        private void InitializeGlobalObjects()
        {
            InitializeTime();
            InitializeLocations();
            InitializeNature();
            InitializePublicDomain();
            InitializePlayers();
            InitializeCorporations();
            InitializeFamilies();
            InitializeAssets();

            InitializeResidences();
            InitializeCorporationAccessRights();
            InitializeCorporationOwnerships();
            InitializeInformationDomainAccessRights();
            InitializePhysicalAssetOwnerships();
            InitializeVirtualAssetOwnerships();
            InitializeSocialUtilities();

            InitializeScores();
        }

        private void InitializeMarkets()
        {
            InitializePhysicalAssetMarket();
            InitializeVirtualAssetMarket();
            InitializeLabourMarket();
            InitializeSocialMarket();
        }

        private void InitializeTime()
        {
            Time.Reset();
        }

        private void InitializeLocations()
        {
            locations = Locations.GetInstance();
            locations.Initialize();
        }

        private void InitializeNature()
        {
            nature = Nature.GetInstance();
            nature.Initialize();
        }

        private void InitializePublicDomain()
        {
            publicDomain = new PublicDomain();
        }

        private void InitializePlayers()
        {
            players = Players.GetInstance();
            players.Initialize();
        }

        private void InitializeCorporations()
        {
            corporations = Corporations.GetInstance();
            corporations.Initialize();
        }

        private void InitializeFamilies()
        {
            families = Families.GetInstance();
            families.Initialize();
        }

        private void InitializeAssets()
        {
            assets = Assets.GetInstance();
            assets.Initialize();
        }

        private void InitializePhysicalAssetMarket()
        {
            physicalAssetMarket = PhysicalAssetMarket.GetInstance();
            physicalAssetMarket.Initialize();
        }

        private void InitializeVirtualAssetMarket()
        {
            virtualAssetMarket = VirtualAssetMarket.GetInstance();
            virtualAssetMarket.Initialize();
        }

        private void InitializeLabourMarket()
        {
            labourMarket = LabourMarket.GetInstance();
            labourMarket.Initialize();
        }

        private void InitializeSocialMarket()
        {
            socialMarket = SocialMarket.GetInstance();
            socialMarket.Initialize();
        }

        private void InitializeResidences()
        {
            residences = Residences.GetInstance();
            residences.Initialize();
        }

        private void InitializeCorporationAccessRights()
        {
            corporationAccessRights = CorporationAccessRights.GetInstance();
            corporationAccessRights.Initialize();
        }

        private void InitializeCorporationOwnerships()
        {
            corporationOwnerships = CorporationOwnerships.GetInstance();
            corporationOwnerships.Initialize();
        }

        private void InitializeInformationDomainAccessRights()
        {
            informationOwnerships = InformationOwnerships.GetInstance();
            informationOwnerships.Initialize();
        }

        private void InitializePhysicalAssetOwnerships()
        {
            physicalAssetOwnerships = PhysicalAssetOwnerships.GetInstance();
            physicalAssetOwnerships.Initialize();
        }

        private void InitializeVirtualAssetOwnerships()
        {
            virtualAssetOwnerships = VirtualAssetOwnerships.GetInstance();
            virtualAssetOwnerships.Initialize();
        }

        private void InitializeSocialUtilities()
        {
            socialRelationships = SocialRelationships.GetInstance();
            socialRelationships.Initialize();
        }

        private void InitializeScores()
        {
            scores = Scores.GetInstance();
            scores.Initialize();
        }

        private void ImplementGeographic()
        {
            geographic = Geographic.GetInstance();
            geographic.Initialize();
        }

        private void ImplementDemographic()
        {
            demographic = Demographic.GetInstance();
            demographic.Initialize();
        }

        private void InitializeNaturalResources()
        {

        }

        public void GreetingFromWorld()
        {
        }

        public int ReportTurn()
        {
            return Time.Now();
        }

        public int ReportPopulation()
        {
            return players.Count();
        }

        public double ReportMeanScore()
        {
            return scores.ComputeAverage();
        }

        public double ReportMaxScore()
        {
            return scores.ComputeMax();
        }

        public string ReportPlayerStatus(int playerId)
        {
            if (players.GetPlayer(playerId) != null) {
                return players.GetPlayer(playerId).ReportStatus();
            } else {
                return null;
            }
        }

        internal void PlayTurn()
        {
            //TODO: all players move simutaneously
            //assign a thread to each player

            Time.Next();

            if (demographic != null) geographic.Invoke();

            if (demographic != null) demographic.Invoke();
            if (corporations.Count() > 0) corporations.CleanUp();
            int reportTurnCount = 1;
            if (demographic != null && Time.Now() % reportTurnCount == 0) {
                DemographicReport report = new DemographicReport(DemographicReport.DemographicReportType.All);
                Debug.WriteLine(report.content);
            }

            scores.ClearRunning();
            foreach (Player player in players.active.Values) {
                player.PlayTurn();
            }

        }

        public void PlayManyTurns(int turnCount)
        {
            for (int i = 0; i < turnCount; i++) {
                PlayTurn();
            }

            if (demographic != null) {
                //end of test
            }
        }

        public void PlayUntilPopulationReach(int endingPlayerCount)
        {
            while (players.Count() < endingPlayerCount && players.Count() > 0) {
                PlayTurn();
            }

            if (demographic != null) {
                //end of test
            }
        }
    }
}
