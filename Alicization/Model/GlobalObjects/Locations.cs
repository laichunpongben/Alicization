using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicization.Util.Extensions;
using Alicization.Model.Geographics.Locations;
using Alicization.Util.Numerics;
using System.Collections.Concurrent;

namespace Alicization.Model.GlobalObjects
{
    public class Locations : IGlobalObject
    {
        private static Locations instance = null;

        internal ConcurrentDictionary<int, ILocation> dict { get; private set; }

        private Locations()
        {
            ; //singleton
        }

        internal static Locations GetInstance()
        {
            return (instance == null) ? instance = new Locations() : instance; 
        }

        internal void Initialize()
        {
            dict = new ConcurrentDictionary<int, ILocation>();
        }

        public int Count()
        {
            return dict.Count;
        }

        internal int MaxId()
        {
            return (dict.Count > 0) ? dict.Keys.Max() : 0;
        }

        internal void Add(ILocation location)
        {
            dict.TryAdd(location.locationId, location);
        }

        internal ILocation Sample()
        {
            return (dict.Count > 1) ? dict.ElementAtOrDefault(Randomness.SampleInt(dict.Count)).Value : dict.ElementAtOrDefault(0).Value;
        }

        internal IDictionary<ILocation, int> ReverseDictionary()
        {
            return dict.Select(i => new KeyValuePair<ILocation, int>(i.Value, i.Key)).ToConcurrentDictionary();
        }
    }
}
