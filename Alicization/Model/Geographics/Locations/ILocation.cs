using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alicization.Model.Geographics.Locations
{
    public interface ILocation
    {
        int locationId { get; }
        string locationName { get; }
    }
}
