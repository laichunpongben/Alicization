using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicization.Util.DataStructures;
using Alicization.Model.GlobalObjects;

namespace Alicization.Model.Markets
{
    class BidAskOrders
    {
        internal PriorityQueue<double, IPriceOrder> bidOrders { get; private set; }
        internal PriorityQueue<double, IPriceOrder> askOrders { get; private set; }

        internal BidAskOrders()
        {
            bidOrders = new PriorityQueue<double, IPriceOrder>();
            askOrders = new PriorityQueue<double, IPriceOrder>(new InvertedDoubleComparer());
        }

        internal void AddAskOrder(IPriceOrder assetMarketOrder)
        {
            askOrders.Enqueue(assetMarketOrder.orderPrice, assetMarketOrder);
        }

        internal void AddBidOrder(IPriceOrder assetMarketOrder)
        {
            bidOrders.Enqueue(assetMarketOrder.orderPrice, assetMarketOrder);
        }

        internal IPriceOrder GetMinAskOrder()
        {
            return askOrders.PeekValue();
        }

        internal IPriceOrder GetMaxBidOrder()
        {
            return bidOrders.PeekValue();
        }

        internal IPriceOrder RemoveAndGetMinAskOrder()
        {
            return askOrders.DequeueValue();
        }

        internal IPriceOrder RemoveAndGetMaxBidOrder()
        {
            return bidOrders.DequeueValue();
        }

        internal bool HasPriceGap(IPriceOrder bidOrder, IPriceOrder askOrder)
        {
            return (bidOrder.orderPrice > askOrder.orderPrice);
        }

        internal double ComputeMidPointPrice(IPriceOrder bidOrder, IPriceOrder askOrder)
        {
            return bidOrder.orderPrice - askOrder.orderPrice;
        }

        internal double ComputeMatchingQuantity(IPriceOrder bidOrder, IPriceOrder askOrder)
        {
            return Math.Min(bidOrder.orderQuantity, askOrder.orderQuantity);
        }

        internal double ComputeRemainingBidQuantity(IPriceOrder bidOrder, double transactionQuantity)
        {
            return bidOrder.orderQuantity - transactionQuantity;
        }

        internal double ComputeRemainingAskQuantity(IPriceOrder askOrder, double transactionQuantity)
        {
            return askOrder.orderQuantity - transactionQuantity;
        }       

    }
}
