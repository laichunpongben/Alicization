using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;

namespace Alicization.Model.Wealth.Assets.Commodities
{
    class BillOfMaterials
    {
        private static BillOfMaterials instance = null;

        private ConcurrentDictionary<CommodityDouble, double> dict { get; set; }

        private BillOfMaterials()
        {
            ; //singleton
        }

        internal static BillOfMaterials GetInstance()
        {
            return (instance == null) ? instance = new BillOfMaterials() : instance; 
        }

        internal void Initialize()
        {
            dict = new ConcurrentDictionary<CommodityDouble, double>();
            InitializePlanetaryMaterialBOM();
        }

        internal IEnumerable<KeyValuePair<CommodityDouble, double>> GetPartLines(ICommodity product)
        {
            return dict.Where(i => i.Key.commodityHighOrder == product);
        }

        internal IEnumerable<KeyValuePair<CommodityDouble, double>> GetAssociatedProductLines(ICommodity part)
        {
            return dict.Where(i => i.Key.commodityLowOrder == part);
        }

        internal IDictionary<ICommodity, double> GetBOM(ICommodity product)
        {
            return GetPartLines(product).Select(i => new { i.Key.commodityLowOrder, i.Value }).ToDictionary(j => j.commodityLowOrder, j => j.Value);
        }

        private void InitializePlanetaryMaterialBOM()
        {

        }
        
    }
}
