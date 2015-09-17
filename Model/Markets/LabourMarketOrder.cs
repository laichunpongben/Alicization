using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alicization.Model.Markets
{
    class LabourMarketOrder : IOrder
    {
        public int turn { get; private set; }
        private IEntity entityBy { get; set; }
        public double monetaryAmount { get; private set; }
        private bool isEmployee { get; set; }


    }
}
