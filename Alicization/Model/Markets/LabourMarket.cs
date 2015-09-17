using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alicization.Model.Markets
{
    public class LabourMarket : IMarket
    {
        private static LabourMarket instance = null;

        internal static List<LabourMarketOrder> marketOrders { get; private set; }
        internal static List<LabourMarketTransaction> marketTransactions { get; private set; }

        private LabourMarket()
        {
            ; //singleton
        }

        internal static LabourMarket GetInstance()
        {
            return (instance == null) ? instance = new LabourMarket() : instance;
        }

        internal void Initialize()
        {
            marketOrders = new List<LabourMarketOrder>();
            marketTransactions = new List<LabourMarketTransaction>();
        }

        internal void AddOrder(LabourMarketOrder labourMarketOrder)
        {
            marketOrders.Add(labourMarketOrder);
        }

        internal void AddTransaction(LabourMarketTransaction labourMarketTransaction)
        {
            marketTransactions.Add(labourMarketTransaction);
        }
    }
}
