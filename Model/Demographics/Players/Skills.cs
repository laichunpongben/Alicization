using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;

namespace Alicization.Model.Demographics.Players
{
    internal class Skills
    {
        internal double harvestingSkillLevel { get; private set; }
        internal double manufacturingSkillLevel { get; private set; }
        internal readonly int maxLevel = 5;

        internal Skills()
        {
            harvestingSkillLevel = 0.0;
            manufacturingSkillLevel = 0.0;
        }

        internal void HarvestingSkillUp()
        {
            harvestingSkillLevel++;
        }

        internal void ManufacturingSkillUp()
        {
            manufacturingSkillLevel++;
        }

    }
}
