using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicization.Util.Numerics;
using Alicization.Model.Demographics.Players;

namespace Alicization.Model
{
    internal class Score
    {
        private double weightWealth { get; set; }
        private double weightScience { get; set; }
        private double weightCulture { get; set; }
        private double weightCredibility { get; set; }

        private double score { get; set; }

        internal Score()
        {
            weightWealth = 0.25;
            weightScience = 0.25;
            weightCulture = 0.25;
            weightCredibility = 0.25;
        }

        internal Score(double wWealth, double wScience, double wCulture, double wCredibility)
        {
            double totalWeight = wWealth + wScience + wCulture + wCredibility;
            if (Numeric.IsAlmostEqual(totalWeight, 1.0)) {
                weightWealth = wWealth;
                weightScience = wScience;
                weightCulture = wCulture;
                weightCredibility = wCredibility;
            } else {
                weightWealth = 0.25;
                weightScience = 0.25;
                weightCulture = 0.25;
                weightCredibility = 0.25;
            }
        }

        internal double ComputeScore(Player player)
        {
            double wealthBalance = player.wealth.balance;
            double scienceBalance = player.science.balance;
            double cultureBalance = player.culture.balance;
            double credibilityBalance = player.credibility.balance;
            return weightWealth * wealthBalance + weightScience * scienceBalance + weightCulture * cultureBalance + weightCredibility * credibilityBalance;
        }

        internal void UpdateRunningScore(Player player)
        {
            score = ComputeScore(player);
            World.scores.AddRunningScore(score);
        }

        internal void UpdateFinishedScore(Player player)
        {
            score = ComputeScore(player);
            World.scores.AddFinishedScore(score);
        }
    }
}
