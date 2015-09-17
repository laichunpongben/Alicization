using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicization.Util.Extensions;
using Alicization.Model.Demographics;
using Alicization.Model.Markets;

namespace Alicization.Model.GlobalObjects
{
    public class SocialRelationships : IGlobalObject
    {
        private static SocialRelationships instance = null;

        private static ConcurrentDictionary<EntityDouble, SocialRelationship> dict { get; set; }

        private SocialRelationships()
        {
            ; //singleton
        }

        internal static SocialRelationships GetInstance()
        {
            return (instance == null) ? instance = new SocialRelationships() : instance;
        }

        internal void Initialize()
        {
            dict = new ConcurrentDictionary<EntityDouble, SocialRelationship>();
        }

        public int Count()
        {
            return dict.Count;
        }

        internal SocialRelationship GetRelationship(EntityDouble entityentityPair)
        {
            return dict[entityentityPair];
        }

        internal SocialRelationshipType GetRelationshipType(EntityDouble entityentityPair)
        {
            return dict[entityentityPair].socialRelationshipType;
        }

        internal double GetUtility(EntityDouble entityentityPair)
        {
            return dict[entityentityPair].socialUtility;
        }

        internal void AddOrUpdateRelationshipType(EntityDouble entityentityPair, SocialRelationshipType relationshipType)
        {
            dict.AddOrUpdate(entityentityPair, new SocialRelationship(relationshipType, 0.0), (k, v) => new SocialRelationship(relationshipType, v.socialUtility));
        }

        internal void AddOrUpdateUtility(EntityDouble entityentityPair, double updateUtility)
        {
            dict.AddOrUpdate(entityentityPair, new SocialRelationship(SocialRelationshipType.None, updateUtility), 
                (k, v) => new SocialRelationship(v.socialRelationshipType, v.socialUtility + updateUtility));
        }

        internal void AddRelationshipTypeUtility(EntityDouble entityentityPair, SocialRelationshipType relationshipType, double updateUtility)
        {
            dict.AddOrUpdate(entityentityPair, new SocialRelationship(relationshipType, updateUtility), 
                (k, v) => new SocialRelationship(relationshipType, v.socialUtility + updateUtility));
        }

        internal void RemoveEntityPair(EntityDouble entityentityPair)
        {
            dict.Remove(entityentityPair);
        }

        internal void RemoveEntity(IEntity entity)
        {
            foreach (var item in dict.Where(i => i.Key.entityFrom == entity || i.Key.entityTo == entity)) {
                dict.Remove(item.Key);
            }
        }

        private IEnumerable<KeyValuePair<EntityDouble, SocialRelationship>> SelectAssociatedEntities(IEntity entity)
        {
            return dict.Where(i => i.Key.entityFrom == entity);
        }

        private IEnumerable<KeyValuePair<EntityDouble, SocialRelationship>> SelectEntityRelationshipType(IEntity entity, SocialRelationshipType type)
        {
            return dict.Where(i => i.Key.entityFrom == entity && i.Value.socialRelationshipType == type);
        }

        internal IEntity SelectHeir(IEntity entity)
        {
            if (dict.Count > 0) {
                var selectedAssociatedEntities = SelectAssociatedEntities(entity);
                SocialRelationshipType maxRelationshipType = selectedAssociatedEntities.Max(i => i.Value.socialRelationshipType);
                var selectedInitimateEntities = selectedAssociatedEntities.Where(i => i.Value.socialRelationshipType == maxRelationshipType).ToList();
                if (selectedInitimateEntities.Count > 1) {
                    return GetEntityOfHighestUtility(selectedInitimateEntities);
                } else {
                    return selectedAssociatedEntities.FirstOrDefault().Key.entityTo;
                }
            } else {
                return null;
            }
        }

        internal IEntity GetEntityOfHighestRelationshipType(IEntity entity)
        {
            var selectedAssociatedEntities = SelectAssociatedEntities(entity);
            SocialRelationshipType maxRelationshipType = selectedAssociatedEntities.Max(i => i.Value.socialRelationshipType);
            return selectedAssociatedEntities.Where(i => i.Value.socialRelationshipType == maxRelationshipType).FirstOrDefault().Key.entityTo;
        }

        internal IEntity GetEntityOfHighestUtility(IEntity entity)
        {
            var selectedAssociatedEntities = SelectAssociatedEntities(entity);
            double maxUtility = selectedAssociatedEntities.Max(i => i.Value.socialUtility);
            return selectedAssociatedEntities.Where(i => i.Value.socialUtility == maxUtility).FirstOrDefault().Key.entityTo;
        }

        private IEntity GetEntityOfHighestUtility(IEnumerable<KeyValuePair<EntityDouble, SocialRelationship>> selection)
        {
            double maxUtility = selection.Max(i => i.Value.socialUtility);
            return selection.Where(i => i.Value.socialUtility == maxUtility).FirstOrDefault().Key.entityTo;
        }
    }
}
