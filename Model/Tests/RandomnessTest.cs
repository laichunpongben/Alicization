using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.Distributions;
using System.Diagnostics;
using Alicization.Util.Numerics;

namespace Alicization.Model.Tests
{
    class RandomnessTest
    {
        public static void TestSampleBinomialPoisson(double p, int maxN)
        {
            int sample1 = 0;
            int sample2 = 0;

            for (int n = 1; n <= maxN; n++) {

                Stopwatch sw1 = new Stopwatch();
                sw1.Start();

                Binomial binomial = new Binomial(p, n);
                sample1 = binomial.Sample();

                sw1.Stop();

                Stopwatch sw2 = new Stopwatch();
                sw2.Start();

                Poisson poisson = new Poisson(p * n);
                sample2 = poisson.Sample();

                sw2.Stop();

                Debug.WriteLine(sample1 + ", " + sample2 + ", " + sw1.ElapsedTicks + ", " + sw2.ElapsedTicks);
            }
        }

        public static void TestSampleGamma(double shape, double rate, int maxN)
        {
            double sample = 0.0;
            for (int i = 0; i < maxN; i++) {
                sample = Randomness.SampleGamma(shape, rate);
                Debug.WriteLine(sample);
            }
        }

        public static void TestSampleLifeConstantHazardRateBernoulli(double constHazardRate, int maxN)
        {
            int age = 0;
            for (int i = 0; i < maxN; i++) {
                age = Randomness.SampleLifeConstantHazardRateBernoulli(constHazardRate);
                Debug.WriteLine(age);
            }
        }

        public static void TestSampleLifeConstantHazardRateExponential(double constHazardRate, int maxN)
        {
            double age = 0;
            for (int i = 0; i < maxN; i++) {
                age = Randomness.SampleExponential(constHazardRate);
                Debug.WriteLine(age);
            }
        }

        public static void TestSampleLifeLinearHazardRate(int ultimateAge, int maxN)
        {
            int age = 0;
            for (int i = 0; i < maxN; i++) {
                age = Randomness.SampleLifeLinearHazardRate(ultimateAge);
                Debug.WriteLine(age);
            }
        }
    }
}
