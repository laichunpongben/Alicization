using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicization.Model.Demographics.Players;

namespace Alicization.Model
{
    public interface IWealthEntity : IEntity
    {
        WealthWallet GetWealthWallet();
        double GetWealthWalletBalance();

        bool HasSufficientCash(double amount);
    }
}
