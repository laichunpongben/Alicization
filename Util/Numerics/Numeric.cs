using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics;

namespace Alicization.Util.Numerics
{
    static class Numeric
    {
        public static double Product(IEnumerable<int> enumerable)
        {
            IEnumerable<double> doubles = enumerable.Select(i => (double) i).ToList();
            return doubles.Aggregate(1.0, (accumulator, current) => accumulator * current);
        }

        public static double Product(IEnumerable<double> enumerable)
        {
            return enumerable.Aggregate(1.0, (accumulator, current) => accumulator * current);
        }

        public static double Factorial(int i)
        {
            return (i <= 1) ? 1 :  i * Factorial(i - 1);
        }

        public static double BinomialCoefficient(int n, int k)
        {
            //Multiplicative formula
            return
                (k < 0 || k > n) ? 0 :
                    (k == 0 || k == n) ? 1 :
                        Product(Enumerable.Range(0, Math.Min(k, n - k)).ToList().ConvertAll(i => (double) (n - i) / (i + 1)).ToList());
        }

        public static double BinomialCoefficient(double alpha, double k)
        {
            //Gamma function
            return 
                (alpha < k) ? 0 :
                    RoundToNearestIntIfAlmostEqual
                    (Math.Exp(SpecialFunctions.GammaLn(alpha + 1) - SpecialFunctions.GammaLn(k + 1) - SpecialFunctions.GammaLn(alpha - k + 1)));
        }

        public static double RoundToNearestIntIfAlmostEqual(double x)
        {
            int thresholdDecimalPlaces = 13;
            return (Precision.AlmostEqual(x, Math.Round(x), thresholdDecimalPlaces)) ? Math.Round(x) : x;
        }

        public static bool IsAlmostEqual(double x)
        {
             int thresholdDecimalPlaces = 13;
             return (Precision.AlmostEqual(x, Math.Round(x), thresholdDecimalPlaces));
        }

        public static bool IsAlmostEqual(double x, double y)
        {
            int thresholdDecimalPlaces = 13;
            return (Precision.AlmostEqual(x, y, thresholdDecimalPlaces));
        }

        public static double TaylorExp(double x)
        {
            //Approx. Exp(x)
            return Enumerable.Range(0, 170).ToList().Sum(i => Math.Pow(x, i) / SpecialFunctions.Factorial(i));
        }
        
        public static double TaylorLogOnePlusX(double x)
        {
            // Approx. log (1+x) for small x
            double threshold = 0.0001;
            return 
                (x == 0) ? 1 :
                    (Math.Abs(x) < threshold) ? Enumerable.Range(1, 10).ToList().Sum(i => Math.Pow(-1, i + 1) * Math.Pow(x, i) / i) :
                        Math.Log(1 + x);
        }

        public static double TaylorOnePlusXPowerAlpha(double x, int alpha)
        {
            // Approx. (1+x)^k for small x
            double threshold = 0.0001;
            return 
                (alpha == 1) ? 1 + x : 
                    (Math.Abs(x) < threshold) ? Enumerable.Range(0, 10).ToList().Sum(i => BinomialCoefficient(alpha, i) * Math.Pow(x, i)) : 
                        Math.Pow(1 + x, alpha);
        }

        public static double TaylorOnePlusXPowerAlpha(double x, double alpha)
        {
            // Approx. (1+x)^k for small x. Overload for real alpha in Binomial Coefficient
            double threshold = 0.0001;
            return
                (alpha == 1) ? 1 + x :
                    (Math.Abs(x) < threshold) ? Enumerable.Range(0, 10).ToList().Sum(i => BinomialCoefficient(alpha, i) * Math.Pow(x, i)) :
                        Math.Pow(1 + x, alpha);
        }

        public static double Average(IList<double> list1, IList<double> list2)
        {
            return (double) (list1.Sum() + list2.Sum()) / (list1.Count + list2.Count);
        }

        public static double WeightedAverage(IList<double> list, IList<double> weight)
        {
            double v = 0;
            for (int i =0; i < list.Count; i++) {
                v += list[i] * weight[i];
            }
            return v;
        }

        public static double Max(params IList<double>[] lists)
        {
            int listCount = lists.Count();
            double maxOfLists = 0;
            switch (listCount) {
                case 0:
                    maxOfLists = 0;
                    break;
                case 1:
                    maxOfLists = (lists[0].Count > 0) ? lists[0].Max() : 0.0;
                    break;
                default:
                    maxOfLists = (lists[0].Count > 0) ? lists[0].Max() : 0.0;
                    for (int i = 1; i < listCount; i++) {
                        maxOfLists = Math.Max(maxOfLists, (lists[i].Count > 0) ? lists[i].Max() : 0.0);
                    }
                    break;
            }
            return maxOfLists;
        }

        public static double MinPositive(params IList<double>[] lists)
        {
            int listCount = lists.Count();
            double minOfLists = 0;
            switch (listCount) {
                case 0:
                    minOfLists = 0;
                    break;
                case 1:
                    minOfLists = (lists[0].Count > 0) ? lists[0].Min() : 0.0;
                    break;
                default:
                    minOfLists = (lists[0].Count > 0) ? lists[0].Min() : 0.0;
                    for (int i = 1; i < listCount; i++) {
                        minOfLists = Math.Min(minOfLists, (lists[i].Count > 0) ? lists[i].Min() : 0.0);
                    }
                    break;
            }
            return minOfLists;
        }

        public static int GetMaxId<T>(params IDictionary<int, T>[] dicts)
        {
            int dictCount = dicts.Count();
            int maxIdOfDicts = 0;
            switch (dictCount) {
                case 0:
                    maxIdOfDicts = 0;
                    break;
                case 1:
                    maxIdOfDicts = (dicts[0].Count > 0) ? dicts[0].Keys.Max() : 0;
                    break;
                default:
                    maxIdOfDicts = (dicts[0].Count > 0) ? dicts[0].Keys.Max() : 0;
                    for (int i = 1; i < dictCount; i++) {
                        maxIdOfDicts = Math.Max(maxIdOfDicts, (dicts[i].Count > 0) ? dicts[i].Keys.Max() : 0);
                    }
                    break;
            }
            return maxIdOfDicts;
        }
    }
}
