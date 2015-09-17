using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicization.Model.GlobalObjects;
using Alicization.Model.Wealth;

namespace Alicization.Model.Markets
{
    class MarketEscrow : ITransaction
    {
        public int turn { get; private set; }
        public WealthEntityDouble wealthEntityDouble { get; private set; }
        public double amount { get; private set; }
        public TransactionType transactionType { get; private set; }

        internal MarketEscrow(IWealthEntity entity, double isk)
        {
            turn = Time.Now();
            wealthEntityDouble = new WealthEntityDouble(entity, World.physicalAssetMarket);
            amount = isk;
            transactionType = TransactionType.MarketEscrow;
        }

        internal MarketEscrow(int t, IWealthEntity entity, double isk)
        {
            turn = t;
            wealthEntityDouble = new WealthEntityDouble(entity, World.physicalAssetMarket);
            amount = isk;
            transactionType = TransactionType.MarketEscrow;
        }

    }
}
