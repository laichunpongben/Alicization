using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alicization.Model.GlobalObjects
{
    public static class Time
    {
        private static int clock { get; set; }
        public static readonly double turnCountInOneYear = 365;
        
        static Time()
        {
            clock = 0;
        }
        
        internal static void Reset()
        {
            clock = 0; //invoke static constructor only
        }

        public static int Now()
        {
            return clock;
        }

        public static double NowInYear()
        {
            return (double) clock / turnCountInOneYear;
        }
        
        internal static void Next()
        {
            clock++;
        }
    }
}
