using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicization.Model.Markets;

namespace Alicization.Model.Demographics.Players
{
    public class WealthWallet : IWallet
    {
        private List<ITransaction> list { get; set; }
        internal double balance { get; private set; }

        internal WealthWallet()
        {
            list = new List<ITransaction>();
            balance = 0.0;
        }

        internal WealthWallet(double startingBalance)
        {
            list = new List<ITransaction>();
            balance = startingBalance;
        }

        internal void Add(ITransaction transaction, IEntity entity)
        {
            list.Add(transaction);
            if (transaction.wealthEntityDouble.entityFrom == entity) {
                balance += transaction.amount;
            } else {
                balance += -1 * transaction.amount;
            }
        }

        internal bool IsSufficientCash(double amount)
        {
            return (amount <= balance);
        }

        internal void Earn(double amount)
        {
            balance += amount;
        }

    }
}
