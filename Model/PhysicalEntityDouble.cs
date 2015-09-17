using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alicization.Model
{
    public class PhysicalEntityDouble
    {
        internal IPhysicalEntity entityFrom { get; private set; }
        internal IPhysicalEntity entityTo { get; private set; }

        internal PhysicalEntityDouble(IPhysicalEntity entity1, IPhysicalEntity entity2)
        {
            entityFrom = entity1;
            entityTo = entity2;
        }

        internal PhysicalEntityDouble(IEntity entity1, IEntity entity2)
        {
            entityFrom = (IPhysicalEntity) entity1;
            entityTo = (IPhysicalEntity) entity2;
        }

        public override bool Equals(System.Object obj)
        {
            if (obj == null) return false;
            PhysicalEntityDouble p = obj as PhysicalEntityDouble;
            if ((System.Object) p == null) return false;
            return (entityFrom.Equals(p.entityFrom) && entityTo.Equals(p.entityTo));
        }

        public bool Equals(PhysicalEntityDouble p)
        {
            return ((object) p == null) ? false : (entityFrom.Equals(p.entityFrom) && entityTo.Equals(p.entityTo));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                hash = (hash * 7) + entityFrom.GetHashCode();
                hash = (hash * 7) + entityTo.GetHashCode();
                return hash;
            }
        }
    }
}
