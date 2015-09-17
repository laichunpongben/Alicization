using Alicization.Model.Wealth.Assets.Commodities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alicization.Model.Wealth.Assets
{
    class CommodityDouble
    {
        internal ICommodity commodityHighOrder { get; private set; }
        internal ICommodity commodityLowOrder { get; private set; }

        internal CommodityDouble(ICommodity commodity1, ICommodity commodity2)
        {
            commodityHighOrder = commodity1;
            commodityLowOrder = commodity2;
        }

        public override bool Equals(System.Object obj)
        {
            if (obj == null) return false;
            CommodityDouble p = obj as CommodityDouble;
            if ((System.Object) p == null) return false;
            return (commodityHighOrder.Equals(p.commodityHighOrder) && commodityLowOrder.Equals(p.commodityLowOrder));
        }

        public bool Equals(CommodityDouble p)
        {
            return ((object) p == null) ? false : (commodityHighOrder.Equals(p.commodityHighOrder) && commodityLowOrder.Equals(p.commodityLowOrder));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                hash = (hash * 7) + commodityHighOrder.GetHashCode();
                hash = (hash * 7) + commodityLowOrder.GetHashCode();
                return hash;
            }
        }
    }
}
