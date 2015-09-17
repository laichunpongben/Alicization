using Alicization.Model.GlobalObjects;
using Alicization.Model.Wealth.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alicization.Model.Markets
{
    class VirtualAssetMarketTransaction : ITransaction
    {
        public int turn { get; private set; }
        public WealthEntityDouble wealthEntityDouble { get; private set; }
        internal IVirtualAsset virtualAsset { get; private set; }
        internal double transactionPrice { get; private set; }
        internal double transactionQuantity { get; private set; }
        public double amount { get; private set; }
        public TransactionType transactionType { get; private set; }

        internal VirtualAssetMarketTransaction(WealthEntityDouble pair, IVirtualAsset asset, double quantity, double price)
        {
            turn = Time.Now();
            wealthEntityDouble = pair;
            virtualAsset = asset;
            transactionQuantity = quantity;
            transactionPrice = price;
            amount = price * quantity;
            transactionType = TransactionType.VirtualAssetMarketTransaction;
        }

        internal VirtualAssetMarketTransaction(int t, WealthEntityDouble pair, IVirtualAsset asset, double quantity, double price)
        {
            turn = t;
            wealthEntityDouble = pair;
            virtualAsset = asset;
            transactionQuantity = quantity;
            transactionPrice = price;
            amount = price * quantity;
            transactionType = TransactionType.VirtualAssetMarketTransaction;
        }

    }
}
