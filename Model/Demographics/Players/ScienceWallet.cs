using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alicization.Model.Demographics.Players
{
    class ScienceWallet : IWallet
    {
        internal double balance { get; private set; }

        internal ScienceWallet()
        {
            balance = 0.0;
        }

        internal ScienceWallet(double startingBalance)
        {
            balance = startingBalance;
        }

        internal void Earn(double amount)
        {
            balance += amount;
        }
    }
}
