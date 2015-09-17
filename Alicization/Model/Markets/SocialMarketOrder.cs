using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicization.Model.GlobalObjects;

namespace Alicization.Model.Markets
{
    class SocialMarketOrder : IOrder
    {
        public int turn { get; private set; }
        internal IEntity entityBy { get; private set; }
        private SocialRelationshipType socialRelationshipType { get; set; }
        private int quantity { get; set; }
        public double monetaryAmount { get; private set; }

        internal SocialMarketOrder(IEntity entity, SocialRelationshipType type, int qty)
        {
            turn = Time.Now();
            entityBy = entity;
            socialRelationshipType = type;
            quantity = qty;
            monetaryAmount = 0.0;
        }
    }
}
