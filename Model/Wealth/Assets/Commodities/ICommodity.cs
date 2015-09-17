using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alicization.Model.Wealth.Assets.Commodities
{
    interface ICommodity : IPhysicalAsset
    {
        CommodityType commodityType { get; }
        double mass { get; }
        double volume { get; }
        double satiationBonus { get; }
        double healthBonus { get; }
        double happinessBonus { get; }
        double wealthBonus { get; }
        double scienceBonus { get; }
        double cultureBonus { get; }
        double decayRate { get; }

        void JoinCommodities();
        int SampleConsumptionDecay();
        int SampleConsumptionDecay(int quantity);
        double SampleConsumptionDecay(double quantity);
    }

    
}
