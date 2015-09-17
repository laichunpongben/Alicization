using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.Distributions;
using Alicization.Util.Numerics;
using Alicization.Model.Demographics.Players;

namespace Alicization.Model.Demographics
{
    internal class Survivorship
    {
        private static Survivorship instance = null;

        private static double constDeathRate { get; set; }
        private static double logOneMinusConstDeathRate { get; set; }

        private Survivorship()
        {
            ; //singleton
        }

        internal static Survivorship GetInstance()
        {
            return (instance == null) ? instance = new Survivorship() : instance;
        }

        internal void Initialize()
        {
            constDeathRate = 0.000027;
            logOneMinusConstDeathRate = Numeric.TaylorLogOnePlusX(-1 * constDeathRate);
        }

        private static int SampleDeath()
        {
            Bernoulli bernoulli = new Bernoulli(constDeathRate);
            return bernoulli.Sample();
        }

        private static int SampleDeath(double deathRate)
        {
            Bernoulli bernoulli = new Bernoulli(deathRate);
            return bernoulli.Sample();
        }

        private static int SampleDeath(double deathRate, int groupSize)
        {
            return Randomness.SampleBinomialPoisson(deathRate, groupSize);
        }

        private static IEnumerable<int> SampleDeathSequence(double deathRate, int groupSize)
        {
            return Randomness.SampleBinomialPoissonSequence(deathRate, groupSize);
        }

        private static bool IsDeadThisTurn()
        {
            return Convert.ToBoolean(SampleDeath());
        }

        private static bool IsDeadThisTurn(double deathRate)
        {
            return Convert.ToBoolean(SampleDeath(deathRate));
        }

        private static int SampleDeathNormalized(int remainingGroupSize)
        {
            double deathRate = ComputeNormalizedDeathRate(remainingGroupSize);
            Bernoulli bernoulli = new Bernoulli(deathRate);
            return bernoulli.Sample();
        }

        private static int SampleDeathNormalized(double mortalityRate, double groupDeathRate)
        {
            double deathRate = ComputeNormalizedDeathRate(mortalityRate, groupDeathRate);
            Bernoulli bernoulli = new Bernoulli(deathRate);
            return bernoulli.Sample();
        }

        private static bool IsDeadThisTurnNormalized(int remainingGroupSize)
        {
            return Convert.ToBoolean(SampleDeathNormalized(remainingGroupSize));
        }

        private static bool IsDeadThisTurnNormalized(double mortalityRate, double groupDeathRate)
        {
            return Convert.ToBoolean(SampleDeathNormalized(mortalityRate, groupDeathRate));
        }

        private static double ComputeDeathRateThreshold()
        {
            return Math.Sqrt(constDeathRate);
        }

        private static int ComputeDeathGroupSize()
        {
            return (int) Math.Floor(ComputeDeathRateThreshold() / constDeathRate);
        }

        private static double ComputeGroupDeathRate(int remainingGroupSize)
        {
            return 1 - Math.Exp(remainingGroupSize * logOneMinusConstDeathRate);
        }

        private static double ComputeNormalizedDeathRate(int remainingGroupSize)
        {
            return Math.Min(constDeathRate / (1 - Math.Exp(remainingGroupSize * logOneMinusConstDeathRate)), 1);
        }

        private static double ComputeNormalizedDeathRate(double mortalityRate, double groupDeathRate)
        {
            return Math.Min(mortalityRate / groupDeathRate, 1);
        }

        internal void Invoke()
        {
            //DrawPlayersDeathConstant();
            //CheckPlayersDeathLifeTable();
            //CheckPlayersDeathByGroupConstant(); 
            CheckPlayersDeathByGroupLifeTable();
            int deathCount = World.players.CountDead();
            RemoveDeadPlayers();
            UpdateDemographicSurvivorship(deathCount);
        }

        private void CheckPlayersDeathConstant() // Poor performance. Not used
        {
            foreach (Player player in World.players.active.Values) {
                if (IsDeadThisTurn()) player.EndLife(DecrementType.Natural);
            }
        }

        private void CheckPlayersDeathLifeTable() // Poor performance. Not used
        {
            foreach (Player player in World.players.active.Values) {
                double mortalityRate = LifeTable.GetMortalityRate(player.ComputeAge());
                if (IsDeadThisTurn(mortalityRate)) player.EndLife(DecrementType.Natural);
            }
        }

        private void CheckPlayersDeathByGroupConstant()
        {
            int playerCount = World.players.Count();
            int groupSize = ComputeDeathGroupSize();
            int groupCount = (int) Math.Ceiling((double) playerCount / groupSize);
            int lastGroupSize = playerCount - groupSize * (groupCount - 1);
            int groupDeathSize = 0;
            bool isDeath = false;
            bool isGroupHasDeath = false;
            int index;

            for (int i = 0; i < groupCount; i++) {
                isGroupHasDeath = false;

                if (groupSize < playerCount) {
                    groupDeathSize = SampleDeath(constDeathRate, groupSize);
                } else {
                    groupDeathSize = SampleDeath(constDeathRate, playerCount);
                }

                if (groupDeathSize > 0) {
                    if (i == groupCount - 1) {
                        groupSize = lastGroupSize;
                    }

                    for (int j = 0; j < groupSize; j++) {
                        index = i * groupSize + j;
                        if (!isGroupHasDeath) {
                            isDeath = IsDeadThisTurnNormalized(groupSize - j);
                        } else {
                            isDeath = IsDeadThisTurn();
                        }

                        if (isDeath) {
                            isGroupHasDeath = true;
                            World.players.active.ElementAtOrDefault(index).Value.EndLife(DecrementType.Natural);
                        }
                    }
                }
            }
        }

        private void CheckPlayersDeathByGroupLifeTable()
        {
            int playerCount = 0;
            double groupDeathRateThreshold = Math.Pow(LifeTable.averageDeathRate, 0.5);
            double logGroupSurvivalRate = 0.0;
            double mortalityRate = 0.0;
            double logOneMinusMortalityRates = 0.0;
            List<double> remainingLogOneMinusMortalityRates = new List<double>();
            double groupDeathRate = 0.0;
            double remainingGroupDeathRate = 0.0;
            double remainingGroupLogOneMinusMortalityRate = 0.0;
            int groupDeathSize = 0;
            bool isGroupHasDeath = false;
            bool isDeath = false;
            int groupStartIndex = 0;
            int i = 0;

            List<Player> selectedPlayers = World.players.ToList();
            playerCount = selectedPlayers.Count();

            while (i < playerCount) {
                while (logGroupSurvivalRate < groupDeathRateThreshold && i < playerCount) {
                    logOneMinusMortalityRates = LifeTable.GetLogOneMinusMortalityRate(selectedPlayers[i].ComputeAge());
                    remainingLogOneMinusMortalityRates.Add(logOneMinusMortalityRates);
                    logGroupSurvivalRate += logOneMinusMortalityRates;
                    i++;
                }
                groupDeathRate = 1 - Math.Exp(logGroupSurvivalRate);
                remainingGroupDeathRate = groupDeathRate;
                remainingGroupLogOneMinusMortalityRate = remainingLogOneMinusMortalityRates.Sum();

                groupDeathSize = SampleDeath(groupDeathRate);
                if (groupDeathSize > 0) { //draw individual death;
                    for (int j = groupStartIndex; j < i; j++) {
                        mortalityRate = LifeTable.GetMortalityRate(selectedPlayers[j].ComputeAge());
                        if (! isGroupHasDeath) {
                            remainingGroupDeathRate = 1 - Math.Exp(remainingGroupLogOneMinusMortalityRate);
                            isDeath = IsDeadThisTurnNormalized(mortalityRate, remainingGroupDeathRate);
                        } else {
                            isDeath = IsDeadThisTurn(mortalityRate);
                        }

                        if (isDeath) {
                            isGroupHasDeath = true;
                            World.players.active.ElementAtOrDefault(j).Value.EndLife(DecrementType.Natural);
                        }

                        remainingGroupLogOneMinusMortalityRate += -1 * remainingLogOneMinusMortalityRates[j - groupStartIndex];
                    }
                }

                isDeath = false;
                isGroupHasDeath = false;
                groupStartIndex = i;
                remainingLogOneMinusMortalityRates.Clear();
                groupDeathRate = 0.0;
                remainingGroupDeathRate = 0.0;
                remainingGroupLogOneMinusMortalityRate = 0.0;
            }
        }

        private int SamplePopulationDeath()
        {
            int playerCount = World.players.Count();
            int deathCount = SampleDeath(constDeathRate, playerCount);
            return deathCount;
        }

        private void DrawPlayersDeathConstant()
        {
            int playerCount = World.players.Count();
            int deathSize = SamplePopulationDeath();
            int[] selection = Randomness.Draw(deathSize, playerCount - 1);
            
            foreach (int index in selection) {
                World.players.active.ElementAtOrDefault(index).Value.EndLife(DecrementType.Natural);
            }
        }

        private void RemoveDeadPlayers()
        {
            World.players.RemoveDead();
        }

        private void UpdateDemographicSurvivorship(int deathCount)
        {
            Demographic.deathCount = deathCount;
        }
    }
}
