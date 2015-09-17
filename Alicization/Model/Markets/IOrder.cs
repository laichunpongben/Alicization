using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alicization.Model.Markets
{
    interface IOrder
    {
        int turn { get; }
        double monetaryAmount { get; }
    }
}
