using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicization.Model.GlobalObjects;
using Alicization.Model.Wealth;

namespace Alicization.Model.Markets
{
    class Donation : ITransaction
    {
        public int turn { get; private set; }
        public WealthEntityDouble wealthEntityDouble { get; private set; }
        public double amount { get; private set; }
        public TransactionType transactionType { get; private set; }

        internal Donation(WealthEntityDouble pair, double isk)
        {
            turn = Time.Now();
            wealthEntityDouble = pair;
            amount = isk;
            transactionType = TransactionType.Donation;

            UpdateWalletPairs(pair, isk);
        }

        internal Donation(int t, WealthEntityDouble pair, double isk)
        {
            turn = t;
            wealthEntityDouble = pair;
            amount = isk;
            transactionType = TransactionType.Donation;

            UpdateWalletPairs(pair, isk);
        }

        private void UpdateWalletPairs(WealthEntityDouble pair, double isk)
        {
            IWealthEntity from = pair.entityFrom;
            IWealthEntity to = pair.entityTo;
            from.GetWealthWallet().Add(this, from);
            to.GetWealthWallet().Add(this, to);
        }
    }
}
