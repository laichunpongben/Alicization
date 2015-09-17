using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alicization.Model.Demographics.Players
{
    class CultureWallet : IWallet
    {
        internal double balance { get; private set; }

        internal CultureWallet()
        {
            balance = 0.0;
        }

        internal CultureWallet(double startingBalance)
        {
            balance = startingBalance;
        }

        internal void Earn(double amount)
        {
            balance += amount;
        }
    }
}
