using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using Alicization.Model.Wealth.Assets.Commodities;
using Alicization.Util.Numerics;

namespace Alicization.Model.Demographics.Players
{
    static class PlayerActivities
    {
        private static Dictionary<int, PlayerActivity> dict { get; set; }

        static PlayerActivities()
        {   
            dict = new Dictionary<int, PlayerActivity>()
            {
                { 0, new PlayerActivity(0, player => player.Harvest(), 0.1)},
                { 1, new PlayerActivity(1, player => player.Consume(), 0.3)}, 
                { 2, new PlayerActivity(2, player => player.Learn(), 0.1)}
            };
        }

        internal static void DoActivity(this Player player, int activityId)
        {
            dict[activityId].activity.Invoke(player);            
        }

        private static void Harvest(this Player player) 
        {
            ICommodity naturalResource = (ICommodity) World.assets.DrawCommodity();
            player.Harvest(naturalResource);
        }

        private static void Harvest(this Player player, ICommodity naturalResource) 
        {
            double harvestSize = Randomness.SamplePoisson(player.skill.harvestingSkillLevel);
            if (World.nature.HasSufficientLocalInventories(player.GetResidence(), naturalResource, harvestSize)) {
                World.nature.TransferPhysicalAsset(player, player.GetResidence(), naturalResource, harvestSize);
            }
        }

        private static void Consume(this Player player)
        {
            ICommodity commodity = (ICommodity) World.assets.DrawCommodity();
            player.Consume(commodity, 1.0);
        }

        private static void Consume(this Player player, ICommodity commodity, double quantity)
        {
            if (player.HasSufficientLocalInventories(commodity, quantity)) {
                AssetLocation aLoc = new AssetLocation(commodity, player.GetResidence());
                player.hygiene.RestoreSatiation(commodity.satiationBonus * quantity);
                player.hygiene.RestoreHealth(commodity.healthBonus * quantity);
                player.hygiene.RestoreHappiness(commodity.healthBonus * quantity);
                player.wealth.Earn(commodity.wealthBonus * quantity);
                player.science.Earn(commodity.scienceBonus * quantity);
                player.culture.Earn(commodity.cultureBonus * quantity);

                player.CheckDecay(commodity, quantity, aLoc);
            }
        }

        private static void Learn(this Player player)
        {
            double u = Randomness.SampleUniform();
            if (u < 0.5) {
                player.LearnHarvesting();
            } else {
                player.LearnManufacturing();
            }
        }

        private static void LearnHarvesting(this Player player)
        {
            if (player.skill.harvestingSkillLevel < player.skill.maxLevel) {
                double pLearn = Math.Min(1.0, (player.gene.memory + player.gene.intelligence) / 200.0);
                if (Randomness.SampleBoolean(pLearn)) player.skill.HarvestingSkillUp();
            }
        }

        private static void LearnManufacturing(this Player player)
        {
            if (player.skill.manufacturingSkillLevel < player.skill.maxLevel) {
                double pLearn = Math.Min(1.0, (player.gene.intelligence + player.gene.memory) / 200.0);
                if (Randomness.SampleBoolean(pLearn)) player.skill.HarvestingSkillUp();
            }
        }

        private static void Manufacture(this Player player)
        {
        }
    }
}
