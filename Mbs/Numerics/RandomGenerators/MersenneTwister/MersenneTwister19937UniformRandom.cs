using System;
using System.Runtime.CompilerServices;

namespace Mbs.Numerics.Random
{
    /// <summary>
    /// The Mersenne Twister uniform mt19937ar 32-bit pseudo-random number generator with the period of 2¹⁹⁹³⁷-1
    /// is based upon information and the implementation presented on the Mersenne Twister home page
    /// <para />
    /// http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/emt.html.
    /// <para />
    /// See http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/MT2002/CODES/mt19937ar.c.
    /// </summary>
    public sealed class MersenneTwister19937UniformRandom : RandomGenerator
    {
        /// <summary>
        /// The state size, N.
        /// </summary>
        private const int StateSize = 624;

        /// <summary>
        /// The shift size, M.
        /// </summary>
        private const int ShiftSize = 397;

        /// <summary>
        /// The word size, w.
        /// </summary>
        private const int WordSize = 32;

        // /// <summary>
        // /// The mask bits, r.
        // /// </summary>
        // private const int MaskBits = 31;

        /// <summary>
        /// Constant matrix a.
        /// </summary>
        private const uint MatrixA = 0x9908b0dfU;

        /// <summary>
        /// The most significant w-r bits.
        /// </summary>
        private const uint UpperMask = 0x80000000U;

        /// <summary>
        /// The least significant r bits.
        /// </summary>
        private const uint LowerMask = 0x7fffffffU;

        /// <summary>
        /// The tempering mask b.
        /// </summary>
        private const uint TemperingMaskB = 0x9d2c5680U;

        /// <summary>
        /// The tempering mask c.
        /// </summary>
        private const uint TemperingMaskC = 0xefc60000U;

        // /// <summary>
        // /// The tempering mask d.
        // /// </summary>
        // private const uint TemperingMaskD = 0xffffffffU;

        /// <summary>
        /// The initialization multiplier, f.
        /// </summary>
        private const uint InitializationMultiplier = 1812433253U;

        /// <summary>
        /// The initialization multiplier, f.
        /// </summary>
        private const uint DefaultSeedValue = 19650218U;

        /// <summary>
        /// The first array initialization multiplier.
        /// </summary>
        private const uint ArrayInitializationMultiplier1 = 1664525U;

        /// <summary>
        /// The second array initialization multiplier.
        /// </summary>
        private const uint ArrayInitializationMultiplier2 = 1566083941U;

        /// <summary>
        /// The word with MSB equal to 1.
        /// </summary>
        private const uint ArrayInitializationMsb1 = 0x80000000U;

        /// <summary>
        /// 9007199254740991.0 is the maximum double value which the 53 significand can hold when the exponent is 0.
        /// </summary>
        private const double FiftyThreeBitsOf1S = 9007199254740991.0;

        private const double Inverse53BitsOf1S = 1.0 / FiftyThreeBitsOf1S;
        private const double OnePlus53BitsOf1S = FiftyThreeBitsOf1S + 1.0;
        private const double InverseOnePlus53BitsOf1S = 1.0 / OnePlus53BitsOf1S;

        /// <summary>
        /// Stores the state vector array.
        /// </summary>
        private readonly uint[] mt = new uint[StateSize];

        /// <summary>
        /// An index for the state vector array element that will be accessed next.
        /// </summary>
        private uint mti;

        private static readonly uint[] Mag01 = { 0x0U, MatrixA };

        /// <summary>
        /// The used seed value.
        /// </summary>
        private readonly uint seedValue;

        /// <summary>
        /// The used seed array.
        /// </summary>
        private readonly uint[] seedArray;

        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwister19937UniformRandom"/> class, using the current system tick count as a seed value.
        /// </summary>
        public MersenneTwister19937UniformRandom()
            : this(Environment.TickCount)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwister19937UniformRandom"/> class, using the specified seed value.
        /// </summary>
        /// <param name="seed">A number used to calculate a starting value for the pseudo-random number sequence.</param>
        public MersenneTwister19937UniformRandom(int seed)
        {
            seedValue = (uint)seed;
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwister19937UniformRandom"/> class, using the specified seed array.
        /// </summary>
        /// <param name="seedArray">An array of numbers used to calculate a starting values for the pseudo-random number sequence.</param>
        public MersenneTwister19937UniformRandom(int[] seedArray)
        {
            seedValue = DefaultSeedValue;
            this.seedArray = new uint[seedArray.Length];
            for (int index = 0; index < seedArray.Length; ++index)
                this.seedArray[index] = (uint)seedArray[index];
            Init();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Init()
        {
            // New seeding algorithm from
            // http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/MT2002/emt19937ar.html.
            mt[0] = seedValue /*& TemperingMaskD*/; // & TemperingMaskD is for WordSize > 32 machines.
            var x = mt[0];
            for (uint i = 1; i != StateSize; ++i)
            {
                // See Knuth "The Art of Computer Programming" Vol. 2, 3rd ed., page 106 for multiplier.
                x = InitializationMultiplier * (x ^ (x >> (WordSize - 2))) + i /*& TemperingMaskD*/; // & TemperingMaskD is for WordSize > 32 machines.
                mt[i] = x;
            }

            if (seedArray != null)
            {
                uint i = 1, j = 0, l = (uint)seedArray.Length;
                x = mt[0];
                uint k = StateSize > l ? StateSize : l;
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

                    if (++j >= seedArray.Length)
                        j = 0;
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

                mt[0] = ArrayInitializationMsb1; // MSB is 1; assuring non-0 initial array.
            }

            mti = StateSize + 1;

            // Reset helper variables used for generation of random booleans.
            BitBuffer = 0;
            BitCount = 32;
        }

        /// <summary>
        /// Resets the <see cref="MersenneTwister19937UniformRandom"/>, so that it produces the same pseudo-random number sequence again.
        /// </summary>
        public override void Reset()
        {
            Init();
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="MersenneTwister19937UniformRandom"/> can be reset, so that it produces the same pseudo-random number sequence again.
        /// </summary>
        public override bool CanReset => true;

        /// <summary>
        /// A next random 32-bit unsigned integer ∊[<see cref="uint.MinValue"/>, <see cref="uint.MaxValue"/>].
        /// </summary>
        /// <returns>A next random 32-bit unsigned integer.</returns>
        public override uint NextUInt()
        {
            uint i;
            if (mti >= StateSize)
            {
                int j;
                for (j = 0; j != StateSize - ShiftSize; ++j)
                {
                    i = (mt[j] & UpperMask) | (mt[j + 1] & LowerMask);
                    mt[j] = mt[j + ShiftSize] ^ (i >> 1) ^ Mag01[i & 0x1U];
                }

                for (; j != StateSize - 1; ++j)
                {
                    i = (mt[j] & UpperMask) | (mt[j + 1] & LowerMask);
                    mt[j] = mt[j + (ShiftSize - StateSize)] ^ (i >> 1) ^ Mag01[i & 0x1U];
                }

                // Last iteration.
                i = (mt[StateSize - 1] & UpperMask) | (mt[0] & LowerMask);
                mt[StateSize - 1] = mt[ShiftSize - 1] ^ (i >> 1) ^ Mag01[i & 0x1U];
                mti = 0;
            }

            i = mt[mti];
            ++mti;

            // Tempering.
            i ^= i >> 11;
            i ^= (i << 7) & TemperingMaskB;
            i ^= (i << 15) & TemperingMaskC;
            return i ^ (i >> 18);
        }

        /// <summary>
        /// There are two common ways to create a double floating point using MT19937:
        /// using <see cref="NextUInt"/> and dividing by 0xFFFFFFFF + 1,  or else generating
        /// two double words and shifting the first by 26 bits and adding the second.
        /// <para />
        /// In a newer measurement of the randomness of MT19937 published in the
        /// journal "Monte Carlo Methods and Applications, Vol. 12, No. 5-6, pp. 385 - 393 (2006)"
        /// entitled "A Repetition Test for Pseudo-Random Number Generators",
        /// it was found that the 32-bit version of generating a double fails at the 95%
        /// confidence level when measuring for expected repetitions of a particular
        /// number in a sequence of numbers generated by the algorithm.
        /// <para />
        /// Due to this, the 53-bit method is implemented here and the 32-bit method
        /// of generating a double is not. If, for some reason,
        /// the 32-bit method is needed, it can be generated by the following:
        /// <code>
        /// (double)NextUInt() / ((UInt64)UInt32.MaxValue + 1);
        /// </code>
        /// </summary>
        /// <param name="translate">Translation factor.</param>
        /// <param name="scale">Scaling factor.</param>
        /// <returns>A double-precision floating-point number.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double Next53Bit(double translate, double scale)
        {
            // Get 27 pseudo-random bits.
            ulong a = (ulong)NextUInt() >> 5;

            // Get 26 pseudo-random bits.
            ulong b = (ulong)NextUInt() >> 6;

            // Shift the 27 pseudo-random bits (a) over by 26 bits (* 67108864.0) and
            // add another pseudo-random 26 bits (+ b).
            return ((a * 67108864.0 + b) + translate) * scale;

            // What about the following instead of the above? Is the multiply better?
            // return BitConverter.Int64BitsToDouble((a << 26) + b));
        }

        /// <summary>
        /// A double-precision floating point random number ∊[0.0, 1.0).
        /// </summary>
        /// <returns>A double-precision floating point random number.</returns>
        public override double NextDouble()
        {
            return Next53Bit(0, InverseOnePlus53BitsOf1S);
        }

        /// <summary>
        /// A double-precision floating point random number ∊[0.0, 1.0].
        /// </summary>
        /// <returns>A double-precision floating point random number.</returns>
        public double NextDoubleInclusiveOne()
        {
            return Next53Bit(0, Inverse53BitsOf1S);
        }

        /// <summary>
        /// A double-precision floating point random number ∊(0.0, 1.0).
        /// </summary>
        /// <returns>A double-precision floating point random number.</returns>
        public double NextDoublePositive()
        {
            return Next53Bit(0.5, Inverse53BitsOf1S);
        }

        /// <summary>
        /// A double-precision floating point random number ∊[0.0, <paramref name="maxValue"/>).
        /// </summary>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. The <paramref name="maxValue"/> must be greater than or equal to 0.0.</param>
        /// <returns>A double-precision floating point random number.</returns>
        public override double NextDouble(double maxValue)
        {
            return Next53Bit(0, InverseOnePlus53BitsOf1S) * maxValue;
        }

        /// <summary>
        /// A double-precision floating point random number ∊[<paramref name="minValue"/>, <paramref name="maxValue"/>).
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number to be generated. The range between <paramref name="minValue"/> and <paramref name="maxValue"/> must be less than or equal to <see cref="double.MaxValue"/>.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. The <paramref name="maxValue"/> must be greater than or equal to <paramref name="minValue"/>. The range between <paramref name="minValue"/> and <paramref name="maxValue"/> must be less than or equal to <see cref="double.MaxValue"/>.</param>
        /// <returns>A double-precision floating point random number.</returns>
        public override double NextDouble(double minValue, double maxValue)
        {
            double range = maxValue - minValue;
            return minValue + Next53Bit(0, InverseOnePlus53BitsOf1S) * range;
        }
    }
}
