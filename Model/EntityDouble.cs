using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alicization.Model
{
    public class EntityDouble
    {
        internal IEntity entityFrom { get; private set; }
        internal IEntity entityTo { get; private set; }

        internal EntityDouble(IEntity entity1, IEntity entity2)
        {
            entityFrom = entity1;
            entityTo = entity2;
        }

        public override bool Equals(System.Object obj)
        {
            if (obj == null) return false;
            EntityDouble p = obj as EntityDouble;
            if ((System.Object) p == null) return false;
            return (entityFrom.Equals(p.entityFrom) && entityTo.Equals(p.entityTo));
        }

        public bool Equals(EntityDouble p)
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
