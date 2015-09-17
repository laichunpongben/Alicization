using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using MathNet.Numerics;
using Alicization.Util.Numerics;

namespace Alicization.Model.Tests
{
    class NumericTest
    {
        public static void TestProduct()
        {
            int repeat = 100;
            int maxInt = 170;

            Stopwatch sw = new Stopwatch();
            sw.Start();

            Enumerable.Range(0, repeat).ToList().ForEach
                (i => Enumerable.Range(1, maxInt).ToList().ForEach
                    (j => Debug.WriteLine(Numeric.Product(Enumerable.Range(1, j).ToList()).ToString()))
                );

            sw.Stop();
            long calcTime = sw.ElapsedTicks;
            Debug.WriteLine("Tick = " + calcTime.ToString());
        }

        public static void TestFactorial()
        {
            int repeat = 100;
            int maxInt = 170;

            Stopwatch sw = new Stopwatch();
            sw.Start();

            Enumerable.Range(0, repeat).ToList().ForEach
                (i => Enumerable.Range(1, maxInt).ToList().ForEach
                    (j => Debug.WriteLine(SpecialFunctions.Factorial(j).ToString()))
                );

            sw.Stop();
            long calcTime = sw.ElapsedTicks;
            Debug.WriteLine("Tick = " + calcTime.ToString());
        }

        public static void TestBinomialCoefficientInt()
        {
            int maxAlpha = 100;

            Enumerable.Range(1, maxAlpha).ToList().ForEach
                (i => Enumerable.Range(0, i + 1).ToList().ForEach
                    (j => Debug.WriteLine(i.ToString() + "," + j.ToString() + "," + Numeric.BinomialCoefficient(i, j).ToString()))
                );
        }

        public static void TestBinomialCoefficientDouble()
        {
            double x = 0.5;
            double alpha = 10.0;

            while (x < 1) {
                double y = Numeric.BinomialCoefficient(alpha, x);
                Debug.WriteLine(y.ToString());
                x += 0.1;
            }
        }

        public static void TestTaylorExp()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            double x = 0.01;
            for (int i = 0; i < 10000; i++)  {
                double y = Numeric.TaylorExp(x);
                Debug.WriteLine(y.ToString());
                x += 0.0001;
            }
            sw.Stop();
            long calcTime = sw.ElapsedTicks;
            Debug.WriteLine(calcTime.ToString());
        }

        public static void TestTaylorLogOnePluxX()
        {
            double q = 0.000027;
            double n = 21.0;

            double x = n * Math.Log(1 - q);
            double y = n * Numeric.TaylorLogOnePlusX(-q);
            double ex = Math.Exp(x);
            double ey = Math.Exp(y);
            double ex1 = 1 - ex;
            double ey1 = 1 - ey;
            double ex2 = Numeric.TaylorExp(x);
            double ey2 = Numeric.TaylorExp(y);
            double ex3 = 1 - ex2;
            double ey3 = 1 - ey2;

            Debug.WriteLine(x.ToString());
            Debug.WriteLine(y.ToString());
            Debug.WriteLine(ex.ToString());
            Debug.WriteLine(ey.ToString());
            Debug.WriteLine(ex1.ToString());
            Debug.WriteLine(ey1.ToString());
            Debug.WriteLine(ex2.ToString());
            Debug.WriteLine(ey2.ToString());
            Debug.WriteLine(ex3.ToString());
            Debug.WriteLine(ey3.ToString());
        }

        public static void TestTaylorOnePlusXPowerAlpha()
        {
            StringBuilder sb = new StringBuilder();

            double q = 0.000027;

            for (int i = 1; i < 200; i++)
            {
                double x = q / (1 - Math.Pow(1 - q, i));
                double y = q / (1 - Math.Exp(i * Numeric.TaylorLogOnePlusX(-q)));
                double z = q / (1 - Numeric.TaylorOnePlusXPowerAlpha(-q, i));

                sb.Append(x.ToString());
                sb.Append(",");
                sb.Append(y.ToString());
                sb.Append(",");
                sb.AppendLine(z.ToString());
            }
            Debug.WriteLine(sb.ToString());
        }

        public static void TestPower()
        {
            StringBuilder sb = new StringBuilder();

            double q = -0.000027;
            double x = 0.0;
            double y = 0.0;
            double z = 0.0;

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    x = Numeric.BinomialCoefficient(i, j);
                    y = Math.Pow(q, j);
                    z = x * y;
                    sb.Append(x.ToString());
                    sb.Append(",");
                    sb.Append(y.ToString());
                    sb.Append(",");
                    sb.AppendLine(z.ToString());
                }
            }

            Debug.WriteLine(sb.ToString());
        }
    }
}
