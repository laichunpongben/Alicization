using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alicization.Model.Markets
{
    class VirtualAssetMarketOrder : IPriceOrder
    {
        public int turn { get; private set; }
        internal EntityAsset assetOwnership { get; private set; } //same for bidder/ asker?
        public double orderQuantity { get; private set; }
        public double orderPrice { get; private set; }
        public double monetaryAmount { get; private set; }
        internal bool isBuy { get; private set; }

        internal VirtualAssetMarketOrder(int t, EntityAsset entityAsset, double quantity, double price, bool isBid)
        {
            turn = t;
            assetOwnership = entityAsset;
            orderQuantity = quantity;
            orderPrice = price;
            monetaryAmount = orderQuantity * orderPrice;
            isBuy = isBid;
        }

        internal VirtualAssetMarketOrder(VirtualAssetMarketOrder oldOrder, double newQuantity)
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
