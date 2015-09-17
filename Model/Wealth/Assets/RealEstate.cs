using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alicization.Model.Wealth.Assets
{
    class RealEstate : IPhysicalAsset
    {
        public int assetId { get; private set; }
        public string assetName { get; private set; }
        public AssetType assetType { get; private set; }
    }
}
