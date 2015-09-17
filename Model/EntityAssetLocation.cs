using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alicization.Model
{
    public class EntityAssetLocation
    {
        internal IEntity owner { get; private set; }
        internal AssetLocation assetLocation { get; private set; }

        public EntityAssetLocation(IEntity entity, AssetLocation aLoc)
        {
            owner = entity;
            assetLocation = aLoc;
        }

        public override bool Equals(System.Object obj)
        {
            if (obj == null) return false;
            EntityAssetLocation p = obj as EntityAssetLocation;
            if ((System.Object) p == null) return false;
            return (owner.Equals(p.owner) && assetLocation.Equals(p.assetLocation));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                hash = (hash * 7) + owner.GetHashCode();
                hash = (hash * 7) + assetLocation.GetHashCode();
                return hash;
            }
        }

        public bool Equals(EntityAssetLocation p)
        {
            return ((object) p == null) ? false : (owner.Equals(p.owner) && assetLocation.Equals(p.assetLocation));
        }
    }
}
