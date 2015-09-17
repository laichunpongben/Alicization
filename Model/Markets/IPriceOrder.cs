using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alicization.Model.Markets
{
    interface IPriceOrder
    {
        double orderQuantity { get; }
        double orderPrice { get; }
    }
}
