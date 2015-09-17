using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicization.Model.Demographics.Players;
using Alicization.Model.Demographics.Corporations;
using Alicization.Util.Numerics;

namespace Alicization.Model.Demographics
{
    internal class Growth
    {
        private static Growth instance = null;

        private static double constBirthRate { get; set; }

        private Growth()
        {
            ; //singleton
        }

        internal static Growth GetInstance()
        {
            return (instance == null) ? instance = new Growth() : instance;
        }

        internal void Initialize()
        {
            constBirthRate = 0.000135; 
        }

        private static int SampleBirth(double meanBirth)
        {
            int birthCount = 0;
            if (meanBirth > 0) {
                Poisson poisson = new Poisson(meanBirth);
                birthCount = poisson.Sample();
            } else {
                birthCount = 0;
            }
            return birthCount;
        }

        internal void Invoke()
        {
            int birthCount = SamplePopulationBirth();
            AddNewPlayersToPopulation(birthCount);
            UpdateDemographicGrowth(birthCount);
        }

        private int SamplePopulationBirth()
        {
            int playerCount = World.players.CountMarriedPassively(); // only married players can give birth
            int birthCount = SampleBirth(constBirthRate * playerCount);
            return birthCount;
        }

        private void AddNewPlayersToPopulation(int birthCount)
        {
            for (int i = 0; i < birthCount; i++) {
                Player player = new Player((Family) World.families.Draw());
            }
        }

        private void UpdateDemographicGrowth(int birthCount)
        {
            Demographic.birthCount = birthCount;
        }
    }
}
