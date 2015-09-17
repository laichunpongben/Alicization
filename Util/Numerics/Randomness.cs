using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;

namespace Alicization.Util.Numerics
{
    static class Randomness
    {
        public static Random GetRandom()
        {
            return new Random(Guid.NewGuid().GetHashCode());
        }

        public static double SampleUniform()
        {
            ContinuousUniform uniform = new ContinuousUniform();
            return uniform.Sample();
        }

        public static double SampleUniform(double lower, double upper)
        {
            ContinuousUniform uniform = new ContinuousUniform(lower, upper);
            return uniform.Sample();
        }

        public static IEnumerable<double> SampleUniformSequence()
        {
            ContinuousUniform uniform = new ContinuousUniform();
            return uniform.Samples();
        }

        public static IEnumerable<double> SampleUniformSequence(double lower, double upper)
        {
            ContinuousUniform uniform = new ContinuousUniform(lower, upper);
            return uniform.Samples();
        }

        public static int SampleInt()
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            return rnd.Next();
        }
        
        public static int SampleInt(int maxInt)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            return rnd.Next(maxInt);
        }
        
        public static int SampleInt(int minInt, int maxInt)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            return rnd.Next(minInt, maxInt);
        }

        public static double SampleDouble(double maxDouble)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            return (double) rnd.Next() / (Int32.MaxValue - 1) * maxDouble;
        }

        public static double SampleDouble(double minDouble, double maxDouble)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            return (double) rnd.Next() / (Int32.MaxValue - 1) * (maxDouble - minDouble) + minDouble;
        }

        public static bool SampleBoolean()
        {
            Bernoulli bernoulli = new Bernoulli(0.5);
            int sample = bernoulli.Sample();
            return (sample > 0);
        }

        public static bool SampleBoolean(double p)
        {
            Bernoulli bernoulli = new Bernoulli(p);
            int sample = bernoulli.Sample();
            return (sample > 0);
        }

        public static int SampleBernoulli(double p)
        {
            Bernoulli bernoulli = new Bernoulli(p);
            return bernoulli.Sample();
        }

        public static int SampleNegativeBinomial(double r, double p)
        {
            NegativeBinomial nb = new NegativeBinomial(r, p);
            return nb.Sample();
        }

        public static int SampleBinomialPoisson(double p, int n)
        {
            //Use Poisson as approximation

            int thresholdSize = 1;
            int sample = 0;

            if (n > 0) {
                if (n < thresholdSize) {
                    Binomial binomial = new Binomial(p, n);
                    sample = binomial.Sample();
                } else {
                    Poisson poisson = new Poisson(p * n);
                    sample = poisson.Sample();
                }
            }

            return sample;
        }

        public static IEnumerable<int> SampleBinomialPoissonSequence(double p, int n)
        {
            //Use Poisson as approximation

            int thresholdSize = 1;
            IEnumerable<int> samples = null;

            if (n > 0) {
                if (n < thresholdSize) {
                    Binomial binomial = new Binomial(p, n);
                    samples = binomial.Samples();
                } else {
                    Poisson poisson = new Poisson(p * n);
                    samples = poisson.Samples();
                }
            }

            return samples;
        }

        public static int SamplePoisson(double mean)
        {
            if (mean > 0) {
                Poisson poisson = new Poisson(mean);
                return poisson.Sample();
            } else {
                return 0;
            }
        }

        public static double SampleGamma(double shape, double rate)
        {
            //mean = shape / rate
            //variance = shape / rate ^ 2
            Gamma gamma = new Gamma(shape, rate);
            return gamma.Sample();
        }

        public static double SampleExponential(double lambda)
        {
            Exponential exponential = new Exponential(lambda);
            return exponential.Sample();
        }

        public static double SampleNormal(double mean, double stddev)
        {
            Normal normal = new Normal(mean, stddev);
            return normal.Sample();
        }

        public static double SampleNormalTruncatedPositive(double mean, double stddev)
        {
            double sample = SampleNormal(mean, stddev);
            return (sample > 0) ? sample : 0;
        }

        public static double SampleNormalTruncatedNegative(double mean, double stddev)
        {
            double sample = SampleNormal(mean, stddev);
            return (sample < 0) ? sample : 0;
        }

        public static double SampleStandardizedNormal()
        {
            Normal normal = new Normal();
            return normal.Sample();
        }
        
        public static int[] Draw(int k, int maxInt)
        {
            int[] selection = new int[k];
            int randInt = 0;
            bool isSelected = false;
            int index = 0;
            for (int i = maxInt - k + 1; i <= maxInt; i++) {
                randInt = SampleInt(i);
                isSelected = selection.Contains(randInt);
                if (!isSelected) {
                    selection[index] = randInt;
                } else {
                    selection[index] = i;
                }
                index++;
            }
            return selection;
        }
        
        public static int[] Draw(int k, int minInt, int maxInt)
        {
            int[] selection = new int[k];
            int randInt = 0;
            bool isSelected = false;
            int index = 0;
            for (int i = maxInt - k + 1; i <= maxInt; i++) {
                randInt = SampleInt(minInt, i);
                isSelected = selection.Contains(randInt);
                if (!isSelected) {
                    selection[index] = randInt;
                } else {
                    selection[index] = i;
                }
                index++;
            }
            return selection;
        }

        public static IList<T> Draw<T>(int k, IList<T> list)
        {
            int[] selection = Draw(k, list.Count - 1);
            List<T> newlist = new List<T>();

            foreach (var i in selection) {
                newlist.Add(list[i]);
            }
            return newlist;
        }

        public static IList<T> Draw<T>(int k, IEnumerable<T> list, int listCount)
        {
            int[] selection = Draw(k, listCount - 1);
            List<T> newlist = new List<T>();

            foreach (var i in selection) {
                newlist.Add(list.ElementAtOrDefault(i));
            }
            return newlist;
        }

        public static T Draw<T>(IList<T> list)
        {
            int index = SampleInt(list.Count - 1);
            return list[index];
        }

        public static T Draw<T>(ISet<T> set)
        {
            int index = SampleInt(set.Count - 1);
            return set.ElementAtOrDefault(index);
        }

        public static T Draw<T>(IDictionary<int,T> dict)
        {
            int index = SampleInt(dict.Count - 1);
            return dict.ElementAtOrDefault(index).Value;
        }

        public static T Draw<T>(ICollection<T> set)
        {
            int index = SampleInt(set.Count - 1);
            return set.ElementAtOrDefault(index);
        }

        public static T Draw<T>(IEnumerable<T> list, int listCount)
        {
            int index = SampleInt(listCount - 1);
            return list.ElementAtOrDefault(index);
        }

        public static T Draw<T>(IDictionary<T, double> distribution)
        {
            double u = SampleUniform();
            T key = default(T);
            double fx = 0;
            int i = 0;
            while (u > fx) {
                key = distribution.Keys.ElementAtOrDefault(i);
                fx += distribution[key];
                i++;
            }
            return key;
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            Random rnd = GetRandom();
            int n = list.Count;
            while (n > 1) {
                n--;
                int k = rnd.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static int SampleLifeConstantHazardRateBernoulli(double constHazardRate)
        {
            bool isAlive = true;
            int age = 0;
            int sample = 0;
            while (isAlive) {
                Bernoulli bernoulli = new Bernoulli(constHazardRate);
                sample = bernoulli.Sample();
                if (sample > 0) {
                    isAlive = false;
                } else {
                    age++;
                }
            }
            return age;
        }

        public static int SampleLifeLinearHazardRate(int ultimateAge)
        {
            bool isAlive = true;
            int age = 0;
            int sample = 0;
            double hazardRate = 0.0;
            while (isAlive) {
                hazardRate = (double) 1 / (ultimateAge - age);
                Bernoulli bernoulli = new Bernoulli(hazardRate);
                sample = bernoulli.Sample();
                if (sample > 0) {
                    isAlive = false;
                } else {
                    age++;
                }
            }
            return age;
        }

        
    }
}
