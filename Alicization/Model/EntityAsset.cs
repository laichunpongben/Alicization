using Alicization.Model.Wealth.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alicization.Model
{
    public class EntityAsset
    {
        internal IEntity owner { get; private set; }
        internal IVirtualAsset virtualAsset { get; private set; }

        internal EntityAsset(IEntity entity, IVirtualAsset asset)
        {
            owner = entity;
            virtualAsset = asset;
        }

        public override bool Equals(System.Object obj)
        {
            if (obj == null) return false;
            EntityAsset p = obj as EntityAsset;
            if ((System.Object) p == null) return false;
            return (owner.Equals(p.owner) && virtualAsset.Equals(p.virtualAsset));
        }

        public bool Equals(EntityAsset p)
        {
            return ((object) p == null) ? false : (owner.Equals(p.owner) && virtualAsset.Equals(p.virtualAsset));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                hash = (hash * 7) + owner.GetHashCode();
                hash = (hash * 7) + virtualAsset.GetHashCode();
                return hash;
            }
        }
    }
}
