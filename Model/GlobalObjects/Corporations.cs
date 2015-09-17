using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicization.Util.Extensions;
using Alicization.Model.Demographics.Corporations;
using Alicization.Model.Demographics.Players;
using Alicization.Util.Numerics;

namespace Alicization.Model.GlobalObjects
{
    public class Corporations : IGlobalObject
    {
        private static Corporations instance = null;

        private ConcurrentDictionary<int, ICorporation> dict { get; set; }

        internal int trueCorpCount { get; private set; }
        
        private Corporations()
        {
            ; //singleton
        }

        internal static Corporations GetInstance()
        {
            return (instance == null) ? instance = new Corporations() : instance; 
        }

        internal void Initialize()
        {
            dict = new ConcurrentDictionary<int, ICorporation>();
            trueCorpCount = 0;
        }

        public int Count()
        {
            return dict.Count;
        }

        internal void IncrementCorporationCount()
        {
            trueCorpCount++;
        }

        internal void DecrementCorporationCount()
        {
            trueCorpCount--;
        }

        internal void Add(ICorporation corporation)
        {
            dict.TryAdd(corporation.entityId, corporation);
        }

        internal void Remove(ICorporation corporation)
        {
            dict.Remove(corporation.entityId);
        }

        internal ICorporation Draw()
        {
            return Randomness.Draw(dict);
        }

        internal void CleanUp()
        {
            RemoveEmpty();
            UpdateTrueCorpCount();
        }

        private void RemoveEmpty()
        {
            foreach (var item in dict.Where(i => i.Value.memberCount == 0)) {
                dict.Remove(item.Key);
            }
        }

        private void UpdateTrueCorpCount()
        {
            trueCorpCount = dict.Count;
        }

        internal ICorporation GetCorporation(int entityId)
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
