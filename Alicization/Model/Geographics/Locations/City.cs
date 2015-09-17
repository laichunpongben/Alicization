using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alicization.Util.Numerics;

namespace Alicization.Model.Geographics.Locations
{
    class City : ILocation
    {
        public int locationId { get; private set; }
        public string locationName { get; private set; }

        internal City()
        {
            locationId = World.locations.MaxId() + 1;
            locationName = "City" + Randomness.SampleInt().ToString();
        }
    }
}
