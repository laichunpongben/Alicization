using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alicization.Model.Geographics.Locations
{
    class Planet : ILocation
    {
        public int locationId { get; private set; }
        public string locationName { get; private set; }
    }
}
