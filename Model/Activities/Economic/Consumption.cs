using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alicization.Model.Wealth.Assets;

namespace Alicization.Model.Activities.Economic
{
    static class Consumption
    {
        private static List<ConsumptionRate> consumptionRates = null;

        static Consumption()
        {
            ConsumptionRate foodConsumption = new ConsumptionRate();
            consumptionRates.Add(foodConsumption);

            ConsumptionRate clothingConsumption = new ConsumptionRate();
            consumptionRates.Add(clothingConsumption);

        }
    }
}
