using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicization.Model.Wealth.Assets;

namespace Alicization.Model.Activities.Economic
{
    class ConsumptionRate
    {
        private IAsset asset { get; set; }
        private double consumptionRate { get; set; }
    }
}
