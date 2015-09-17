using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicization.Util.Extensions;
using Alicization.Model.Demographics.Corporations;
using Alicization.Util.Numerics;

namespace Alicization.Model.GlobalObjects
{
    public class Families : IGlobalObject
    {
        private static Families instance = null;

        private ConcurrentDictionary<int, ICorporation> dict { get; set; }

        internal int familyCount { get; private set; }

        private Families()
        {
            ; //singleton
        }

        internal static Families GetInstance()
        {
            return (instance == null) ? instance = new Families() : instance; 
        }

        internal void Initialize()
        {
            dict = new ConcurrentDictionary<int, ICorporation>();
            familyCount = 0;
        }

        public int Count()
        {
            return dict.Count;
        }

        internal int MaxId()
        {
            return (dict.Count > 0) ? dict.Keys.Max() : 0;
        }

        internal void IncrementFamilyCount()
        {
            familyCount++;
        }

        internal void DecrementFamilyCount()
        {
            familyCount--;
        }

        internal void Add(ICorporation family)
        {
            dict.TryAdd(family.entityId, family);
        }

        internal void Remove(ICorporation family)
        {
            dict.Remove(family.entityId);
        }

        internal ICorporation Draw()
        {
            return Randomness.Draw(dict);
        }

        internal void CleanUp()
        {
            RemoveEmpty();
            UpdateFamilyCount();
        }

        private void RemoveEmpty()
        {
            foreach (var item in dict.Where(i => i.Value.memberCount == 0)) {
                dict.Remove(item.Key);
            }
        }

        private void UpdateFamilyCount()
        {
            familyCount = dict.Count;
        }

        internal ICorporation GetFamily(int entityId)
        {
            ICorporation corp;
            return (dict.TryGetValue(entityId, out corp)) ? corp : null;
        }

        internal IDictionary<ICorporation, int> ReverseDictionary()
        {
            return dict.Select(i => new KeyValuePair<ICorporation, int>(i.Value, i.Key)).ToConcurrentDictionary();
        }

    }
}
