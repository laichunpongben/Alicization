using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicization.Util.Numerics;

namespace Alicization.Model.Demographics.Players
{
    internal class Hygiene
    {
        internal double satiationCritical { get; private set; }
        internal double healthCritical { get; private set; }
        internal double happinessCritical { get; private set; }

        private double maxSatiationLevel { get; set; }
        private double maxHealthLevel { get; set; }
        private double maxHappinessLevel { get; set; }

        internal double satiationLevel { get; set; }
        internal double healthLevel { get; set; }
        internal double happinessLevel { get; set; }

        private double satiationDeteriorationRate { get; set; }
        private double healthDeteriorationRate { get; set; }
        private double happinessDeteriorationRate { get; set; }

        internal Hygiene()
        {
            InitializeHygieneCriticals();
            InitializeMaxHygieneLevels();
            InitializeHygieneLevels();
            InitializeDeteriorationRates();
        }

        internal void InitializeHygieneCriticals()
        {
            satiationCritical = 0.0;
            healthCritical = 0.0;
            happinessCritical = 0.0;
        }

        internal void InitializeMaxHygieneLevels()
        {
            maxSatiationLevel = 100.0;
            maxHealthLevel = 100.0;
            maxHappinessLevel = 100.0;
        }

        internal void InitializeHygieneLevels()
        {
            satiationLevel = 100.0;
            healthLevel = 100.0;
            happinessLevel = 100.0;
        }

        internal void InitializeDeteriorationRates()
        {
            satiationDeteriorationRate = -1.0 * Randomness.SampleGamma(100.0, 50.0);
            healthDeteriorationRate = -1.0 * Randomness.SampleGamma(100.0, 50.0);
            happinessDeteriorationRate = -1.0 * Randomness.SampleGamma(100.0, 50.0);
        }

        internal void PassiveDeteriorate()
        {
            satiationLevel += satiationDeteriorationRate;
            healthLevel += healthDeteriorationRate;
            happinessLevel += happinessDeteriorationRate;
        }

        internal void RestoreSatiation(double point)
        {
            satiationLevel += point;
            if (satiationLevel > maxSatiationLevel) satiationLevel = maxSatiationLevel;
        }

        internal void RestoreHealth(double point)
        {
            healthLevel += point;
            if (healthLevel > maxHealthLevel) healthLevel = maxHealthLevel;
        }

        internal void RestoreHappiness(double point)
        {
            happinessLevel += point;
            if (happinessLevel > maxHappinessLevel) happinessLevel = maxHappinessLevel;
        }
    }
}
