using System;
using System.Runtime.CompilerServices;

namespace Mbs.Numerics.Random
{
    /// <summary>
    /// A uniform pseudo-random number renerator based on the Park–Miller (sometimes called Lehmer) algorithm.
    /// See https://en.wikipedia.org/wiki/Lehmer_random_number_generator.
    /// </summary>
    public sealed class ParkMillerUniformRandom : RandomGenerator
    {
        /// <summary>
        /// 9007199254740991.0 is the maximum double value which the 53 significand can hold when the exponent is 0.
        /// </summary>
        private const double FiftyThreeBitsOf1S = 9007199254740991;

        private const double Inverse53BitsOf1S = 1.0 / FiftyThreeBitsOf1S;
        private const double OnePlus53BitsOf1S = FiftyThreeBitsOf1S + 1;
        private const double InverseOnePlus53BitsOf1S = 1 / OnePlus53BitsOf1S;

        /// <summary>
        /// 2³¹-1 = 2147483647.
        /// </summary>
        private const long LongMax = 2147483647;

        private const long Ndiv = 1 + (LongMax - 1) / 32;
        private const long Iq = 127773;
        private const long Ir = 2836;
        private const int TableSize = 32;

        private readonly long[] z = new long[TableSize];
        private readonly long seedValue;

        /// <summary>
        /// 7⁵ = 16807, a primitive root modulo M₃₁.
        /// </summary>
        private long g = 16807;

        private long x;
        private long y;
        private ulong lastUlong;
        private int countUlong;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParkMillerUniformRandom"/> class, using the current system tick count as a seed value.
        /// </summary>
        public ParkMillerUniformRandom()
            : this(Environment.TickCount)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParkMillerUniformRandom"/> class., using the specified seed value.
        /// </summary>
        /// <param name="seed">A number used to calculate a starting value for the pseudo-random number sequence.</param>
        public ParkMillerUniformRandom(int seed)
        {
            seedValue = seed;
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParkMillerUniformRandom"/> class, using the specified seed value.
        /// </summary>
        /// <param name="seed">A number used to calculate a starting value for the pseudo-random number sequence.</param>
        public ParkMillerUniformRandom(long seed)
        {
            seedValue = seed;
            Init();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Init()
        {
            // Initialize seed.
            y = seedValue;

            // Prevent zero value.
            if (y == 0)
                y = 1;

            // Load the shuffle table after 8 warm-ups.
            for (int j = TableSize + 7; j >= 0; --j)
            {
                // Implement multiplicative congruential generator with Schrage's algorithm.
                long k = y / Iq;
                y = g * (y - k * Iq) - Ir * k;
                if (y < 0)
                    y += LongMax;
                if (j < TableSize)
                    z[j] = y;
            }

            x = z[0];
            countUlong = 0;

            // Reset helper variables used for generation of random booleans.
            BitBuffer = 0;
            BitCount = 32;
        }

        /// <summary>
        /// Resets the <see cref="ParkMillerUniformRandom"/>, so that it produces the same pseudo-random number sequence again.
        /// </summary>
        public override void Reset()
        {
            Init();
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="ParkMillerUniformRandom"/> can be reset, so that it produces the same pseudo-random number sequence again.
        /// </summary>
        public override bool CanReset => true;

        /// <summary>
        /// A next random 64-bit unsigned integer ∊[<see cref="ulong.MinValue"/>, <see cref="ulong.MaxValue"/>].
        /// </summary>
        /// <returns>A next random 64-bit unsigned integer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ulong NextULong()
        {
            // Implement multiplicative congruential generator with Schrage's algorithm.
            long k = y / Iq;
            y = g * (y - k * Iq) - Ir * k;
            if (y < 0)
                y += LongMax;

            // Perform Bays-Durham shuffle to remove low-order serial correlations.
            long j = x / Ndiv;
            x = z[j];
            z[j] = y;
            return (ulong)x;
        }

        /// <summary>
        /// A next random 32-bit unsigned integer ∊[<see cref="uint.MinValue"/>, <see cref="uint.MaxValue"/>].
        /// </summary>
        /// <returns>A next random 32-bit unsigned integer.</returns>
        public override uint NextUInt()
        {
            if (countUlong == 0)
            {
                lastUlong = NextULong();
                countUlong = 1;
                return (uint)lastUlong;
            }

            countUlong = 0;
            return (uint)(lastUlong >> 32);
        }

        /// <summary>
        /// A double-precision floating point random number ∊[0.0, 1.0).
        /// </summary>
        /// <returns>A double-precision floating point random number.</returns>
        public override double NextDouble()
        {
            return (NextULong() >> 11) * InverseOnePlus53BitsOf1S;
        }

        /// <summary>
        /// A double-precision floating point random number ∊[0.0, 1.0].
        /// </summary>
        /// <returns>A double-precision floating point random number.</returns>
        public double NextDoubleInclusiveOne()
        {
            return (NextULong() >> 11) * Inverse53BitsOf1S;
        }

        /// <summary>
        /// A double-precision floating point random number ∊[0.0, <paramref name="maxValue"/>).
        /// </summary>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. The <paramref name="maxValue"/> must be greater than or equal to 0.0.</param>
        /// <returns>A double-precision floating point random number.</returns>
        public override double NextDouble(double maxValue)
        {
            return (NextULong() >> 11) * InverseOnePlus53BitsOf1S * maxValue;
        }

        /// <summary>
        /// A double-precision floating point random number ∊[<paramref name="minValue"/>, <paramref name="maxValue"/>).
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number to be generated. The range between <paramref name="minValue"/> and <paramref name="maxValue"/> must be less than or equal to <see cref="double.MaxValue"/>.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. The <paramref name="maxValue"/> must be greater than or equal to <paramref name="minValue"/>. The range between <paramref name="minValue"/> and <paramref name="maxValue"/> must be less than or equal to <see cref="double.MaxValue"/>.</param>
        /// <returns>A double-precision floating point random number.</returns>
        public override double NextDouble(double minValue, double maxValue)
        {
            return minValue + (NextULong() >> 11) * InverseOnePlus53BitsOf1S * (maxValue - minValue);
        }
    }
}
