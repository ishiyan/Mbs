using System;
using System.Runtime.CompilerServices;

namespace Mbs.Numerics.RandomGenerators.BoxMuller
{
    /// <summary>
    /// A source of random values sample from a Gaussian distribution.
    /// Uses the polar form of the Box-Muller method.
    /// See http://en.wikipedia.org/wiki/Box_Muller_transform.
    /// </summary>
    public class BoxMullerNormalRandom : INormalRandomGenerator
    {
        private readonly IRandomGenerator rng;
        private readonly Func<double> sampleFn;
        private double? spareValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="BoxMullerNormalRandom"/> class.
        /// </summary>
        /// <param name="rng">The uniform random generator.</param>
        /// <param name="mean">The mean value of the normal distribution.</param>
        /// <param name="stdDev">The standard deviation of the normal distribution.</param>
        public BoxMullerNormalRandom(IRandomGenerator rng, double mean = 0, double stdDev = 1)
        {
            this.rng = rng;

            // We predetermine which of these four function variants to use at construction time,
            // thus avoiding the two condition tests on each invocation of Sample().
            if (Math.Abs(mean) <= double.Epsilon)
            {
                if (Math.Abs(stdDev - 1) <= double.Epsilon)
                {
                    sampleFn = NextDoubleStandard;
                }
                else
                {
                    sampleFn = () => NextDoubleStandard() * stdDev;
                }
            }
            else
            {
                if (Math.Abs(stdDev - 1) <= double.Epsilon)
                {
                    sampleFn = () => mean + NextDoubleStandard();
                }
                else
                {
                    sampleFn = () => mean + (NextDoubleStandard() * stdDev);
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="BoxMullerNormalRandom"/> can be reset,
        /// so that it produces the same pseudo-random number sequence again.
        /// </summary>
        public bool CanReset => rng.CanReset;

        /// <summary>
        /// Resets the <see cref="BoxMullerNormalRandom"/>,
        /// so that it produces the same random number sequence again.
        /// </summary>
        public void Reset()
        {
            rng.Reset();
            spareValue = null;
        }

        /// <summary>
        /// A double-precision floating point Gaussian random number.
        /// </summary>
        /// <returns>The next double-precision floating point Gaussian random number.</returns>
        public double NextDouble()
        {
            return sampleFn();
        }

        /// <summary>
        /// A double-precision floating point Gaussian random number given the distribution mean and the standard deviation.
        /// </summary>
        /// <param name="mean">Distribution mean.</param>
        /// <param name="stdDev">Distribution standard deviation.</param>
        /// <returns>The next double-precision floating point Gaussian random number.</returns>
        public double NextDouble(double mean, double stdDev)
        {
            return mean + (NextDoubleStandard() * stdDev);
        }

        /// <summary>
        /// Take a sample from the standard gaussian distribution, i.e. with mean of 0 and standard deviation of 1.
        /// </summary>
        /// <returns>The next double-precision floating point Gaussian random number.</returns>
        public double NextDoubleStandard()
        {
            if (spareValue.HasValue)
            {
                double tmp = spareValue.Value;
                spareValue = null;
                return tmp;
            }

            // Generate two new gaussian values.
            double x, y, sqr;

            // We need a non-zero random point inside the unit circle.
            do
            {
                x = 2 * NextDoubleNonZero() - 1;
                y = 2 * NextDoubleNonZero() - 1;
                sqr = x * x + y * y;
            }
            while (sqr > 1 || sqr < double.Epsilon);

            // Make the Box-Muller transformation.
            double fac = Math.Sqrt(-2 * Math.Log(sqr) / sqr);

            spareValue = x * fac;
            return y * fac;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double NextDoubleNonZero()
        {
            double nonZero;
            do
            {
                nonZero = rng.NextDouble();
            }
            while (Math.Abs(nonZero) < double.Epsilon);
            return nonZero;
        }
    }
}
