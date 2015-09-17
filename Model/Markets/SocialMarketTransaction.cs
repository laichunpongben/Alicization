using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicization.Model.GlobalObjects;
using Alicization.Model.Wealth;

namespace Alicization.Model.Markets
{
    class SocialMarketTransaction : ITransaction
    {
        public int turn { get; private set;  }
        public WealthEntityDouble wealthEntityDouble { get; private set; }
        public double amount { get; private set; }
        public TransactionType transactionType { get; private set; }

        internal SocialMarketTransaction(WealthEntityDouble pair, double isk)
        {
            turn = Time.Now();
            wealthEntityDouble = pair;
            amount = isk;
            transactionType = TransactionType.SocialMarketTransaction;
        }

        internal SocialMarketTransaction(int t, WealthEntityDouble pair, double isk)
        {
            turn = t;
            wealthEntityDouble = pair;
            amount = isk;
            transactionType = TransactionType.SocialMarketTransaction;
        }

    }

}
