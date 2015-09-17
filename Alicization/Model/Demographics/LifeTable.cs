using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using Alicization.Model.GlobalObjects;
using Alicization.Util.Numerics;

namespace Alicization.Model.Demographics
{
    static class LifeTable
    {
        private static ConcurrentDictionary<int, double> mortalityRates { get; set; }
        private static ConcurrentDictionary<int, double> logOneMinusMortalityRates { get; set; }
        public static double averageDeathRate { get; private set; }

        static LifeTable()
        {
            mortalityRates = new ConcurrentDictionary<int, double>();
            logOneMinusMortalityRates = new ConcurrentDictionary<int, double>();

            int utimateAgeInYear = 120;
            int ultimateAge = Convert.ToInt32(utimateAgeInYear * Time.turnCountInOneYear);

            InitializeMortalities(ultimateAge);
            InitializeLogOneMinusMortalityRates(ultimateAge);
            averageDeathRate = ComputeAverageDeathRate(ultimateAge);
        }

        private static double ComputeDeMoivreLawMortality(int currentAge, int ultimateAge)
        {
            return (double) 1 / (ultimateAge - currentAge);
        }

        private static double ComputeLogOneMinusMortalityRate(double mortalityRate)
        {
            return Numeric.TaylorLogOnePlusX(-1 * mortalityRate);
        }

        private static void AddMortalityRate(int age, double mortalityRate)
        {
            mortalityRates.TryAdd(age, mortalityRate);
        }

        private static void AddLogOneMinusMortalityRate(int age, double logOneMinusMortalityRate)
        {
            logOneMinusMortalityRates.TryAdd(age, logOneMinusMortalityRate);
        }

        internal static double GetMortalityRate(int age)
        {
            return mortalityRates[age];
        }

        internal static double GetLogOneMinusMortalityRate(int age)
        {
            return logOneMinusMortalityRates[age];
        }

        private static void InitializeMortalities(int ultimateAge)
        {
            double mortalityRate = 0.0;
            for (int i = 0; i < ultimateAge; i++) {
                mortalityRate = ComputeDeMoivreLawMortality(i, ultimateAge);
                AddMortalityRate(i, mortalityRate);
            }
        }

        private static void InitializeLogOneMinusMortalityRates(int ultimateAge)
        {
            double mortalityRate = 0.0;
            double logOneMinusMortalityRate = 0.0;
            for (int i = 0; i < ultimateAge; i++) {
                mortalityRate = mortalityRates[i];
                logOneMinusMortalityRate = ComputeLogOneMinusMortalityRate(mortalityRate);
                AddLogOneMinusMortalityRate(i, logOneMinusMortalityRate);
            }
        }

        private static double ComputeAverageDeathRate(int ultimateAge)
        {
            return (double) 2 / ultimateAge;
        }
    }
}
