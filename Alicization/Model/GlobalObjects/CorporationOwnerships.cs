using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicization.Util.Extensions;
using Alicization.Model.Demographics.Corporations;

namespace Alicization.Model.GlobalObjects
{
    public class CorporationOwnerships : IGlobalObject
    {
        private static CorporationOwnerships instance = null;

        private ConcurrentDictionary<CorporationEntity, double> dict = null;

        private CorporationOwnerships()
        {
            ; //singleton
        }

        internal static CorporationOwnerships GetInstance()
        {
            return (instance == null) ? instance = new CorporationOwnerships() : instance; 
        }

        internal void Initialize()
        {
            dict = new ConcurrentDictionary<CorporationEntity, double>();
        }

        public int Count()
        {
            return dict.Count();
        }

        internal void Add(CorporationEntity corporationOwnership)
        {
            dict.TryAdd(corporationOwnership, 1);
        }

        internal void Add(CorporationEntity corporationOwnership, double share)
        {
            dict.TryAdd(corporationOwnership, share);
        }

        internal void Remove(CorporationEntity corporationEntityPair)
        {
            dict.Remove(corporationEntityPair);
        }

        internal void RemoveCorporation(ICorporation corp)
        {
            foreach (var item in dict.Where(i => i.Key.corporationAccessible == corp)) {
                dict.Remove(item.Key);
            }
        }

        internal void RemoveEntity(IEntity entity)
        {
            foreach (var item in dict.Where(i => i.Key.entityCanAccess == entity)) {
                dict.Remove(item.Key);
            }
        }
    }
}
