using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicization.Util.Numerics;
using Alicization.Model.Demographics.Players;
using Alicization.Model.GlobalObjects;

namespace Alicization.Model.Demographics
{
    public class Demographic
    {
        private static Demographic instance = null;

        private static Marriage marriage = null;
        private static Growth growth = null;
        private static Survivorship survivorship = null;
        internal static DemographicReport demographicReport = null;

        private static readonly int startingPopulation = 10;
        internal static List<DemographicMovement> demographicHistory { get; private set; }

        internal static int birthCount { get; set; }
        internal static int deathCount { get; set; }

        private Demographic()
        {
            ; //singleton
        }

        internal static Demographic GetInstance()
        {
            return (instance == null) ? instance = new Demographic() : instance;
        }

        internal void Initialize()
        {
            InitializePopulation();
            ImplementMarriage();
            ImplementGrowth();
            ImplementSurvivorship();
            InitializeDemographicHistory();
        }

        private void InitializePopulation()
        {
            foreach (var i in Enumerable.Range(0, startingPopulation)) {
                if (i % 2 == 0) {
                    Player player = new Player(Player.Gender.Male);
                } else {
                    Player player = new Player(Player.Gender.Female);
                }
            }
        }

        private void ImplementMarriage()
        {
            marriage = Marriage.GetInstance();
            marriage.Initialize();
        }

        private void ImplementGrowth()
        {
            growth = Growth.GetInstance();
            growth.Initialize();
        }

        private void ImplementSurvivorship()
        {
            survivorship = Survivorship.GetInstance();
            survivorship.Initialize();
        }

        private void InitializeDemographicHistory()
        {
            demographicHistory = new List<DemographicMovement>();
            DemographicMovement survivalTimeZero = new DemographicMovement(0, 0, 0, startingPopulation);
            demographicHistory.Add(survivalTimeZero);
        }

        internal void Invoke()
        {
            int playerCount = World.players.Count();
            birthCount = 0;
            deathCount = 0;

            if (playerCount > 0) {
                if (marriage != null) marriage.Invoke();
                if (growth != null) growth.Invoke();
                if (survivorship != null) survivorship.Invoke();

                UpdateDemographicHistory(birthCount, deathCount);
            }
        }

        private void UpdateDemographicHistory(int birthCount, int deathCount)
        {
            int playerCount = World.players.Count();
            DemographicMovement demographicTransaction = new DemographicMovement(Time.Now(), birthCount, deathCount, playerCount);
            demographicHistory.Add(demographicTransaction);
        }
    }
}
