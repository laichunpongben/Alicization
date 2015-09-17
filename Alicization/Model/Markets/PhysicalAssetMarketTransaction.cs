using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicization.Model.Wealth.Assets;
using Alicization.Model.Geographics.Locations;
using Alicization.Model.Demographics;
using Alicization.Model.GlobalObjects;
using Alicization.Model.Wealth;

namespace Alicization.Model.Markets
{
    class PhysicalAssetMarketTransaction : ITransaction
    {
        public int turn { get; private set; }
        public WealthEntityDouble wealthEntityDouble { get; private set; }
        internal AssetLocation assetLocation { get; private set; }
        internal double transactionPrice { get; private set; }
        internal double transactionQuantity { get; private set; }
        public double amount { get; private set; }
        public TransactionType transactionType { get; private set; }

        internal PhysicalAssetMarketTransaction(WealthEntityDouble pair, AssetLocation aLoc, double quantity, double price)
        {
            turn = Time.Now();
            wealthEntityDouble = pair;
            assetLocation = aLoc;
            transactionQuantity = quantity;
            transactionPrice = price;
            amount = price * quantity;
            transactionType = TransactionType.PhysicalAssetMarketTransaction;
        }

        internal PhysicalAssetMarketTransaction(int t, WealthEntityDouble pair, AssetLocation aLoc, double quantity, double price)
        {
            turn = t;
            wealthEntityDouble = pair;
            assetLocation = aLoc;
            transactionQuantity = quantity;
            transactionPrice = price;
            amount = price * quantity;
            transactionType = TransactionType.PhysicalAssetMarketTransaction;
        }

    }
}
