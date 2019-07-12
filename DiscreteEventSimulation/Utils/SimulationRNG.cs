using Accord.Statistics.Distributions.Univariate;
using System;
namespace DiscreteEventSimulation.Utils
{
    public class SimulationRNG
    {
        private static SimulationRNG _instance;
        private static Random _random;

        public static SimulationRNG CreateInstance(int seed)
        {
            if (_instance == null)
            {
                _instance = new SimulationRNG();
                _random = new Random(seed);
            }

            return _instance;
        }

        public static long GetRandomNormal(long expected, double deviation)
        {
            NormalDistribution normal = new NormalDistribution(expected, deviation);

            return (long)normal.InverseDistributionFunction(_random.NextDouble());
        }

        public static long GetRandomPoissonDistribution(double lambda)
        {
            return new PoissonDistribution(lambda).InverseDistributionFunction(_random.NextDouble());
        }

        public static long GetRandomExponentional(double lambda)
        {
            return (long)new ExponentialDistribution(lambda).InverseDistributionFunction(_random.NextDouble());
        }

        public static long GetRandomGamma(double theta, double k)
        {
            return (long)new GammaDistribution(theta, k).InverseDistributionFunction(_random.NextDouble());
        }
    }
}