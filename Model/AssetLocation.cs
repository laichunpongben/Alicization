using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicization.Model.Wealth.Assets;
using Alicization.Model.Geographics.Locations;

namespace Alicization.Model
{
    public class AssetLocation
    {
        internal IPhysicalAsset physicalAsset { get; private set; }
        internal ILocation location { get; private set; }

        public AssetLocation(IPhysicalAsset asset, ILocation loc)
        {
            physicalAsset = asset;
            location = loc;
        }

        public override bool Equals(System.Object obj)
        {
            if (obj == null) return false;
            AssetLocation p = obj as AssetLocation;
            if ((System.Object) p == null) return false;
            return (physicalAsset.Equals(p.physicalAsset) && location.Equals(p.location));
        }

        public bool Equals(AssetLocation p)
        {
            return ((object) p == null) ? false : (physicalAsset.Equals(p.physicalAsset) && location.Equals(p.location));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                hash = (hash * 7) + physicalAsset.GetHashCode();
                hash = (hash * 7) + location.GetHashCode();
                return hash;
            }
        }
    }
}
