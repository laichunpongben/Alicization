using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicization.Model.Geographics.Locations;

namespace Alicization.Model.Demographics.Players
{
    class Residence //obsolete
    {
        internal IEntity resident { get; private set; }
        internal ILocation activeLocation { get; private set; }

        internal Residence(IEntity en, ILocation loc)
        {
            resident = en;
            activeLocation = loc;
        }
    }
}
