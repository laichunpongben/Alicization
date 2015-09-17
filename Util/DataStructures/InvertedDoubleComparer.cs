using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alicization.Util.DataStructures
{
    class InvertedDoubleComparer : IComparer<double>
    {
        public int Compare(double x, double y)
        {
            // "inverted" comparison
            // direct comparison of integers should return x - y
            return (int) ((x > y) ? -1 : (x < y) ? 1 : 0);
        }
    }
}
