﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alicization.Model.Demographics
{
    internal class DemographicMovement
    {
        internal int turn { get; private set; }
        internal int birthCount { get; private set; }
        internal int deathCount { get; private set; }
        internal int playerCount { get; private set; }

        internal DemographicMovement(int t, int b, int d, int p)
        {
            turn = t;
            birthCount = b;
            deathCount = d;
            playerCount = p;
        }
    }
}
