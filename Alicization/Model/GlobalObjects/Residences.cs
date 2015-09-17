using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicization.Model.Demographics.Players;
using Alicization.Model.Geographics.Locations;
using System.Collections.Concurrent;
using Alicization.Util.Extensions;

namespace Alicization.Model.GlobalObjects
{
    public class Residences : IGlobalObject 
    {
        private static Residences instance = null;

        private ConcurrentDictionary<IEntity, ILocation> dict { get; set; } //moved from hashSet

        private Residences()
        {
            ; //singleton
        }

        internal static Residences GetInstance()
        {
            return (instance == null) ? instance = new Residences() : instance; 
        }

        internal void Initialize()
        {
            dict = new ConcurrentDictionary<IEntity, ILocation>();
        }

        public int Count()
        {
            return dict.Count;
        }

        internal ILocation GetLocation(IEntity entity)
        {
            return dict[entity];
        }

        internal void Add(IEntity entity, ILocation location)
        {
            dict.TryAdd(entity, location);
        }

        internal void RemoveEntity(IEntity entity)
        {
            dict.Remove(entity);
        }

        internal void RemoveLocalEntities(ILocation location)
        {
            foreach (var item in dict.Where(i => i.Value == location)) {
                dict.Remove(item.Key);
            }
        }

        internal IEnumerable<IEntity> SelectLocalEntities(ILocation location)
        {
            return dict.Where(i => i.Value == location).Select(j => j.Key);
        }

        internal IEnumerable<Player> SelectLocalPlayers(ILocation location)
        {
            return dict.Where(i => i.Value == location && i.Key.entityType == EntityType.Player).Select(j => (Player) j.Key);
        }
        
    }
}
