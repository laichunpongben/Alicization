using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alicization.Model.Demographics.Corporations;

namespace Alicization.Model
{
    public class CorporationEntity
    {
        internal ICorporation corporationAccessible { get; private set; }
        internal IEntity entityCanAccess { get; private set; }

        internal CorporationEntity(ICorporation corp, IEntity entity)
        {
            corporationAccessible = corp;
            entityCanAccess = entity;
        }

        public override bool Equals(System.Object obj)
        {
            if (obj == null) return false;
            CorporationEntity p = obj as CorporationEntity;
            if ((System.Object) p == null) return false;
            return (corporationAccessible.Equals(p.corporationAccessible) && entityCanAccess.Equals(p.entityCanAccess));
        }

        public bool Equals(CorporationEntity p)
        {
            return ((object) p == null) ? false : (corporationAccessible.Equals(p.corporationAccessible) && entityCanAccess.Equals(p.entityCanAccess));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                hash = (hash * 7) + corporationAccessible.GetHashCode();
                hash = (hash * 7) + entityCanAccess.GetHashCode();
                return hash;
            }
        }
    }
}
