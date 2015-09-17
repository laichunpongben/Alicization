using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alicization.Model.Demographics.Players
{
    class CredibilityWallet : IWallet
    {
        internal double balance { get; private set; }

        internal CredibilityWallet()
        {
            balance = 0.0;
        }
    }
}
