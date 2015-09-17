using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicization.Model.Wealth.Assets;

namespace Alicization.Model.Markets
{
    class PhysicalAssetMarketOrder : IPriceOrder
    {
        public int turn { get; private set; }
        internal EntityAssetLocation assetOwnership { get; private set; } //same for bidder/ asker?
        public double orderQuantity { get; private set; }
        public double orderPrice { get; private set; }
        public double monetaryAmount { get; private set; }
        internal bool isBuy { get; private set; }

        internal PhysicalAssetMarketOrder(int t, EntityAssetLocation entityAssetLocation, double quantity, double price, bool isBid)
        {
            turn = t;
            assetOwnership = entityAssetLocation;
            orderQuantity = quantity;
            orderPrice = price;
            monetaryAmount = orderQuantity * orderPrice;
            isBuy = isBid;
        }

        internal PhysicalAssetMarketOrder(PhysicalAssetMarketOrder oldOrder, double newQuantity)
        {
            turn = oldOrder.turn;
            assetOwnership = oldOrder.assetOwnership;
            orderQuantity = newQuantity;
            orderPrice = oldOrder.orderPrice;
            monetaryAmount = orderQuantity * orderPrice;
            isBuy = oldOrder.isBuy;
        }

    }
}
