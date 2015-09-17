using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicization.Util.Numerics;

namespace Alicization.Model.Wealth.Assets.Commodities
{
    internal class NaturalMaterial : ICommodity
    {
        public int assetId { get; private set; }
        public string assetName { get; private set; }
        public AssetType assetType { get; private set; }
        public CommodityType commodityType { get; private set; }
        public double mass { get; private set; }
        public double volume { get; private set; }
        public double satiationBonus { get; private set; }
        public double healthBonus { get; private set; }
        public double happinessBonus { get; private set; }
        public double wealthBonus { get; private set; }
        public double scienceBonus { get; private set; }
        public double cultureBonus { get; private set; }
        public double decayRate { get; private set; }

        internal NaturalMaterial()
        {
            assetId = World.assets.MaxId() + 1;
            assetName = assetId.ToString();
            assetType = AssetType.Commodity;
            commodityType = CommodityType.NaturalMaterial;
            mass = 0.0;
            volume = 0.0;
            satiationBonus = Randomness.SampleInt(10);
            healthBonus = Randomness.SampleInt(10);
            happinessBonus = Randomness.SampleInt(10);
            wealthBonus = Randomness.SampleInt(10);
            scienceBonus = Randomness.SampleInt(10);
            cultureBonus = Randomness.SampleInt(10);
            decayRate = 1.0;

            JoinCommodities();
        }

        internal NaturalMaterial(int id, string name, double satiation, double health, double happiness, double wealth, double science, double culture)
        {
            assetId = id;
            assetName = name;
            assetType = AssetType.Commodity;
            commodityType = CommodityType.NaturalMaterial;
            mass = 0.0;
            volume = 0.0;
            satiationBonus = satiation;
            healthBonus = health;
            happinessBonus = happiness;
            wealthBonus = wealth;
            scienceBonus = science;
            cultureBonus = culture;
            decayRate = 1.0;

            JoinCommodities();
        }

        public void JoinCommodities()
        {
            World.assets.AddCommodity(this);
        }

        public int SampleConsumptionDecay() 
        {
            return (decayRate < 1) ? Randomness.SampleBernoulli(decayRate) : 1;
        }

        public int SampleConsumptionDecay(int quantity) 
        {
            return (decayRate < 1) ? Randomness.SampleBinomialPoisson(decayRate, quantity) : quantity;
        }

        public double SampleConsumptionDecay(double quantity) 
        {
            return (decayRate < 1) ? Randomness.SampleGamma(quantity, 1 / decayRate) : quantity;
        }
    }
}
