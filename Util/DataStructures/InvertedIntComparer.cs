using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alicization.Util.DataStructures
{
    class InvertedIntComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            // "inverted" comparison
            // direct comparison of integers should return x - y
            return y - x;
        }
    }
}
