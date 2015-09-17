using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicization.Util.Numerics;

namespace Alicization.Model.Wealth.Assets.Commodities
{
    class Ore : ICommodity
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

        internal Ore(int id, string name, double vol)
        {
            assetId = id;
            assetName = name;
            volume = vol;
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
