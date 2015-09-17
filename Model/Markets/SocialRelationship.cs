using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alicization.Model.Markets
{
    class SocialRelationship
    {
        internal SocialRelationshipType socialRelationshipType { get; private set; }
        internal double socialUtility { get; private set; }

        internal SocialRelationship(SocialRelationshipType type, double utility)
        {
            socialRelationshipType = type;
            socialUtility = utility;
        }
    }
}
