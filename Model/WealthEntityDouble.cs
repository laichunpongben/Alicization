using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alicization.Model
{
    public class WealthEntityDouble
    {
        internal IWealthEntity entityFrom { get; private set; }
        internal IWealthEntity entityTo { get; private set; }

        internal WealthEntityDouble(IWealthEntity entity1, IWealthEntity entity2)
        {
            entityFrom = entity1;
            entityTo = entity2;
        }

        internal WealthEntityDouble(IEntity entity1, IEntity entity2)
        {
            entityFrom = (IWealthEntity) entity1;
            entityTo = (IWealthEntity) entity2;
        }

        public override bool Equals(System.Object obj)
        {
            if (obj == null) return false;
            WealthEntityDouble p = obj as WealthEntityDouble;
            if ((System.Object) p == null) return false;
            return (entityFrom.Equals(p.entityFrom) && entityTo.Equals(p.entityTo));
        }

        public bool Equals(WealthEntityDouble p)
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
