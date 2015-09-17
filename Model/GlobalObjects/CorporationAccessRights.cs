using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicization.Util.Extensions;
using Alicization.Model.Demographics.Corporations;
using System.Diagnostics;
using Alicization.Model.Demographics.Players;
using System.Collections.Concurrent;

namespace Alicization.Model.GlobalObjects
{
    public class CorporationAccessRights : IGlobalObject
    {
        private static CorporationAccessRights instance = null;

        private ConcurrentDictionary<CorporationEntity, CorporationAccessRightType> dict { get; set; }

        private CorporationAccessRights()
        {
            ; //singleton
        }

        internal static CorporationAccessRights GetInstance()
        {
            return (instance == null) ? instance = new CorporationAccessRights() : instance; 
        }

        internal void Initialize()
        {
            dict = new ConcurrentDictionary<CorporationEntity, CorporationAccessRightType>();
        }

        public int Count()
        {
            return dict.Count();
        }

        internal int CountCorporations(ICorporation corp)
        {
            return dict.Count(i => i.Key.corporationAccessible == corp);
        }

        internal void Add(CorporationEntity corporationEntityPair)
        {
            dict.TryAdd(corporationEntityPair, CorporationAccessRightType.ReadOnly); //minimum access right
        }

        internal void Add(CorporationEntity corporationEntityPair, CorporationAccessRightType type)
        {
            dict.TryAdd(corporationEntityPair, type);
        }

        internal void Remove(CorporationEntity corporationEntityPair)
        {
            dict.Remove(corporationEntityPair);
        }

        private IEnumerable<KeyValuePair<CorporationEntity, CorporationAccessRightType>> SelectEntityAccessRights(IEntity entity)
        {
            return dict.Where(i => i.Key.entityCanAccess == entity);
        }

        private IEnumerable<KeyValuePair<CorporationEntity, CorporationAccessRightType>> SelectExceptEntityAccessRights(IEntity entity)
        {
            return dict.Where(i => i.Key.entityCanAccess != entity);
        }

        internal void RemoveEntityAccessRights(IEntity entity)
        {
            dict = SelectExceptEntityAccessRights(entity).ToConcurrentDictionary();
        }

        private IEnumerable<KeyValuePair<CorporationEntity, CorporationAccessRightType>> SelectCorporationAccessRights(ICorporation corp)
        {
            return dict.Where(i => i.Key.corporationAccessible == corp);
        }

        private IEnumerable<KeyValuePair<CorporationEntity, CorporationAccessRightType>> SelectExceptCorporationAccessRights(ICorporation corp)
        {
            return dict.Where(i => i.Key.corporationAccessible != corp);
        }

        internal void RemoveCorporationAccessRights(ICorporation corp)
        {
            dict = SelectExceptCorporationAccessRights(corp).ToConcurrentDictionary();
        }

        private IEnumerable<KeyValuePair<CorporationEntity, CorporationAccessRightType>> SelectPlayerFamily(Player player)
        {
            return dict.Where(i => i.Key.entityCanAccess == player && i.Key.corporationAccessible.entityType == EntityType.Family);
        }

        private IEnumerable<KeyValuePair<CorporationEntity, CorporationAccessRightType>> SelectExceptPlayerFamily(Player player)
        {
            return dict.Where(i => i.Key.entityCanAccess != player || i.Key.corporationAccessible.entityType != EntityType.Family);
        }

        internal void RemovePlayerFamily(Player player)
        {
            dict = SelectExceptPlayerFamily(player).ToConcurrentDictionary();
        }

        internal Family GetPlayerFamily(Player player)
        {
            var selection = SelectPlayerFamily(player).FirstOrDefault();
            if (selection.Key != null) {
                return (Family) selection.Key.corporationAccessible;
            } else {
                return null;
            }
        }

        internal bool IsAccessible(IEntity entity, ICorporation corp)
        {
            CorporationAccessRightType type;
            CorporationEntity corporationEntityPair = new CorporationEntity(corp, entity);
            return (dict.TryGetValue(corporationEntityPair, out type) && (int) type > 1);
        }

        public void ReportAccessRights()
        {
            foreach (var item in dict) {
                Debug.WriteLine(item.Key.corporationAccessible.entityId + "," + item.Key.entityCanAccess.entityId + "," + item.Value);
            }
        }
    }
}
