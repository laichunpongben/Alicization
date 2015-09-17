using Alicization.Model.Wealth.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicization.Model.Information;

namespace Alicization.Model
{
    public class EntityInformation
    {
        internal IEntity owner { get; private set; }
        internal IInformation information { get; private set; }

        internal EntityInformation(IEntity entity, IInformation info)
        {
            owner = entity;
            information = info;
        }

        public override bool Equals(System.Object obj)
        {
            if (obj == null) return false;
            EntityInformation p = obj as EntityInformation;
            if ((System.Object) p == null) return false;
            return (owner.Equals(p.owner) && information.Equals(p.information));
        }

        public bool Equals(EntityInformation p)
        {
            return ((object) p == null) ? false : (owner.Equals(p.owner) && information.Equals(p.information));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                hash = (hash * 7) + owner.GetHashCode();
                hash = (hash * 7) + information.GetHashCode();
                return hash;
            }
        }
    }
}
