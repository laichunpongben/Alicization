using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicization.Model.Geographics.Locations;
using System.Diagnostics;

namespace Alicization.Model.Geographics
{
    public class Geographic
    {
        private static Geographic instance = null;

        private Geographic()
        {
            ; //singleton
        }

        internal static Geographic GetInstance()
        {
            return (instance == null) ? instance = new Geographic() : instance; 
        }

        internal void Initialize()
        {
            InitializeCities();
        }

        private void InitializeCities()
        {
            City city1 = new City();
            World.locations.Add(city1);
        }

        internal void Invoke()
        {
            World.nature.CreateNaturalResources();
        }

    }
}
