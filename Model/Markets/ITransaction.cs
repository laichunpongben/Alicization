using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alicization.Model.Markets
{
    interface ITransaction
    {
        int turn { get; }
        WealthEntityDouble wealthEntityDouble { get; }
        double amount { get; }
        TransactionType transactionType { get; }
    }
}
