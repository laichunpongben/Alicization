using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alicization.Model.Demographics.Players
{
    internal class PlayerActivityPlan
    {
        internal Dictionary<int, int> dict { get; private set; }

        internal PlayerActivityPlan()
        {
            dict = new Dictionary<int, int>()
            {
                { 0, 4 },
                { 1, 4 },
                { 2, 4 }
            };
        }
    }
}
