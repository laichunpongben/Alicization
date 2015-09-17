using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alicization.Model.Wealth.Assets
{
    public interface IAsset
    {
        int assetId { get; }
        string assetName { get; }
        AssetType assetType { get; }
    }
}
