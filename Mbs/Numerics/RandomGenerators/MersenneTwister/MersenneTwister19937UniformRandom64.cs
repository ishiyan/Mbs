using System;
using System.Runtime.CompilerServices;

namespace Mbs.Numerics.RandomGenerators.MersenneTwister
{
    /// <summary>
    /// The Mersenne Twister uniform mt19937ar 64-bit pseudo-random number generator with the period of 2¹⁹⁹³⁷-1
    /// is based upon information and the implementation presented on the Mersenne Twister home page:
    /// <para />
    /// http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/emt.html.
    /// <para />
    /// See http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/VERSIONS/C-LANG/mt19937-64.c.
    /// </summary>
    public sealed class MersenneTwister19937UniformRandom64 : RandomGenerator
    {
        /// <summary>
        /// The state size, N.
        /// </summary>
        private const int StateSize = 312;

        /// <summary>
        /// The shift size, M.
        /// </summary>
        private const int ShiftSize = 156;

        /// <summary>
        /// The word size, w.
        /// </summary>
        private const int WordSize = 64;

        /// <summary>
        /// Constant matrix a.
        /// </summary>
        private const ulong MatrixA = 0xB5026F5AA96619E9UL;

        /// <summary>
        /// The most significant w-r (33) bits.
        /// </summary>
        private const ulong UpperMask = 0xFFFFFFFF80000000UL;

        /// <summary>
        /// The least significant r (31) bits.
        /// </summary>
        private const ulong LowerMask = 0x7FFFFFFFUL;

        /// <summary>
        /// The tempering mask a.
        /// </summary>
        private const ulong TemperingMaskA = 0x5555555555555555UL;

        /// <summary>
        /// The tempering mask b.
        /// </summary>
        private const ulong TemperingMaskB = 0x71d67fffeda60000UL;

        /// <summary>
        /// The tempering mask c.
        /// </summary>
        private const ulong TemperingMaskC = 0xFFF7EEE000000000UL;

        /// <summary>
        /// The initialization multiplier, f.
        /// </summary>
        private const ulong InitializationMultiplier = 6364136223846793005UL;

        /// <summary>
        /// The default seed value.
        /// </summary>
        private const ulong DefaultSeedValue = 19650218UL;

        /// <summary>
        /// The first array initialization multiplier.
        /// </summary>
        private const ulong ArrayInitializationMultiplier1 = 3935559000370003845UL;

        /// <summary>
        /// The second array initialization multiplier.
        /// </summary>
        private const ulong ArrayInitializationMultiplier2 = 2862933555777941757UL;

        /// <summary>
        /// The unsigned long with MSB equal to 1.
        /// </summary>
        private const ulong ArrayInitializationMsb1 = 1UL << 63;

        /// <summary>
        /// 9007199254740991.0 is the maximum double value which the 53 significand can hold when the exponent is 0.
        /// </summary>
        private const double FiftyThreeBitsOf1S = 9007199254740991.0;

        private const double Inverse53BitsOf1S = 1.0 / FiftyThreeBitsOf1S;
        private const double OnePlus53BitsOf1S = FiftyThreeBitsOf1S + 1.0;
        private const double InverseOnePlus53BitsOf1S = 1.0 / OnePlus53BitsOf1S;

        private static readonly ulong[] Mag01 = { 0x0UL, MatrixA };

        /// <summary>
        /// Stores the state vector array.
        /// </summary>
        private readonly ulong[] mt = new ulong[StateSize];

        /// <summary>
        /// The used seed value.
        /// </summary>
        private readonly ulong seedValue;

        /// <summary>
        /// The used seed array.
        /// </summary>
        private readonly ulong[] seedArray;

        /// <summary>
        /// An index for the state vector array element that will be accessed next.
        /// </summary>
        private uint mti;

        private ulong lastUlong;
        private bool loadedUlong;

        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwister19937UniformRandom64"/> class.
        /// </summary>
        public MersenneTwister19937UniformRandom64()
            : this(Environment.TickCount)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwister19937UniformRandom64"/> class,
        /// using the specified seed value.
        /// </summary>
        /// <param name="seed">A number used to calculate a starting value for the pseudo-random number sequence.</param>
        public MersenneTwister19937UniformRandom64(int seed)
        {
            seedValue = (ulong)seed;
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwister19937UniformRandom64"/> class,
        /// using the specified seed value.
        /// </summary>
        /// <param name="seed">A number used to calculate a starting value for the pseudo-random number sequence.</param>
        public MersenneTwister19937UniformRandom64(long seed)
        {
            seedValue = (ulong)seed;
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwister19937UniformRandom64"/> class,
        /// using the specified seed array.
        /// </summary>
        /// <param name="seedArray">An array of numbers used to calculate a starting values for the pseudo-random number sequence.</param>
        public MersenneTwister19937UniformRandom64(int[] seedArray)
        {
            seedValue = DefaultSeedValue;
            this.seedArray = new ulong[seedArray.Length];
            for (int index = 0; index != seedArray.Length; ++index)
            {
                this.seedArray[index] = (ulong)seedArray[index];
            }

            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwister19937UniformRandom64"/> class,
        /// using the specified seed array.
        /// </summary>
        /// <param name="seedArray">An array of numbers used to calculate a starting values for the pseudo-random number sequence.</param>
        public MersenneTwister19937UniformRandom64(long[] seedArray)
        {
            seedValue = DefaultSeedValue;
            this.seedArray = new ulong[seedArray.Length];
            for (int index = 0; index != seedArray.Length; ++index)
            {
                this.seedArray[index] = (ulong)seedArray[index];
            }

            Init();
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="MersenneTwister19937UniformRandom64"/> can be reset,
        /// so that it produces the same pseudo-random number sequence again.
        /// </summary>
        public override bool CanReset => true;

        /// <summary>
        /// Resets the <see cref="MersenneTwister19937UniformRandom64"/>,
        /// so that it produces the same pseudo-random number sequence again.
        /// </summary>
        public override void Reset()
        {
            Init();
        }

        /// <summary>
        /// A next random 64-bit unsigned integer ∊[<see cref="ulong.MinValue"/>, <see cref="ulong.MaxValue"/>].
        /// </summary>
        /// <returns>A next random 64-bit unsigned integer.</returns>
        public ulong NextULong()
        {
            ulong i;
            if (mti >= StateSize)
            {
                int j;
                for (j = 0; j != StateSize - ShiftSize; ++j)
                {
                    i = (mt[j] & UpperMask) | (mt[j + 1] & LowerMask);
                    mt[j] = mt[j + ShiftSize] ^ (i >> 1) ^ Mag01[i & 1UL];
                }

                for (; j != StateSize - 1; ++j)
                {
                    i = (mt[j] & UpperMask) | (mt[j + 1] & LowerMask);
                    mt[j] = mt[j + (ShiftSize - StateSize)] ^ (i >> 1) ^ Mag01[i & 1UL];
                }

                // Last iteration.
                i = (mt[StateSize - 1] & UpperMask) | (mt[0] & LowerMask);
                mt[StateSize - 1] = mt[ShiftSize - 1] ^ (i >> 1) ^ Mag01[i & 1UL];
                mti = 0;
            }

            i = mt[mti];
            ++mti;

            // Tempering.
            i ^= (i >> 29) & TemperingMaskA;
            i ^= (i << 17) & TemperingMaskB;
            i ^= (i << 37) & TemperingMaskC;
            i ^= i >> 43;
            return i;
        }

        /// <summary>
        /// A next random 32-bit unsigned integer ∊[<see cref="uint.MinValue"/>, <see cref="uint.MaxValue"/>].
        /// </summary>
        /// <returns>A next random 32-bit unsigned integer.</returns>
        public override uint NextUInt()
        {
            if (loadedUlong)
            {
                loadedUlong = false;
                return (uint)(lastUlong >> 32);
            }

            lastUlong = NextULong();
            loadedUlong = true;
            return (uint)lastUlong;
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
        /// A double-precision floating point random number ∊[0.0, 1.0).
        /// </summary>
        /// <returns>A double-precision floating point random number.</returns>
        public override double NextDouble()
        {
            return (NextULong() >> 11) * InverseOnePlus53BitsOf1S;
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Init()
        {
            // New seeding algorithm from
            // http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/MT2002/emt19937ar.html.
            mt[0] = seedValue;
            var x = seedValue;
            for (uint i = 1; i != StateSize; ++i)
            {
                // See Knuth "The Art of Computer Programming" Vol. 2, 3rd ed., page 106 for multiplier.
                x = InitializationMultiplier * (x ^ (x >> (WordSize - 2))) + i;
                mt[i] = x;
            }

            if (seedArray != null)
            {
                uint i = 1, j = 0, l = (uint)seedArray.Length;
                x = mt[0];
                uint k = l < StateSize ? StateSize : l;
                for (; k != 0; --k)
                {
                    x = (mt[i] ^ ((x ^ (x >> (WordSize - 2))) * ArrayInitializationMultiplier1)) + seedArray[j] + j; // Non-linear.
                    mt[i] = x;
                    if (++i >= StateSize)
                    {
                        x = mt[StateSize - 1];
                        mt[0] = x;
                        i = 1;
                    }

                    if (++j >= l)
                    {
                        j = 0;
                    }
                }

                for (k = StateSize - 1; k != 0; --k)
                {
                    x = (mt[i] ^ ((x ^ (x >> (WordSize - 2))) * ArrayInitializationMultiplier2)) - i; // Non-linear.
                    mt[i] = x;
                    if (++i >= StateSize)
                    {
                        x = mt[StateSize - 1];
                        mt[0] = x;
                        i = 1;
                    }
                }

                mt[0] = ArrayInitializationMsb1; // MSB is 1; assuring non-zero initial array.
            }

            mti = StateSize + 1;
            loadedUlong = false;

            // Reset helper variables used for generation of random booleans.
            BitBuffer = 0;
            BitCount = 32;
        }
    }
}
