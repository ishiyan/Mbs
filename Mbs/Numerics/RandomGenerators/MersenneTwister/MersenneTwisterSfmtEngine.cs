using System.Runtime.CompilerServices;

namespace Mbs.Numerics.RandomGenerators.MersenneTwister
{
    /// <summary>
    /// The SIMD-oriented Fast Mersenne Twister (SFMT) uniform pseudo-random number generator engine
    /// is based upon information and the implementation presented on:
    /// <para />
    /// http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/SFMT/index.html.
    /// <para />
    /// Obviously, we can not use SSE2 _mm_* intrinsics here, we just emulate them.
    /// </summary>
    public abstract class MersenneTwisterSfmtEngine : RandomGenerator
    {
        /// <summary>
        /// 9007199254740991.0 is the maximum double value which the 53 significand can hold when the exponent is 0.
        /// </summary>
        private const double FiftyThreeBitsOf1S = 9007199254740991.0;

        private const double Inverse53BitsOf1S = 1.0 / FiftyThreeBitsOf1S;
        private const double OnePlus53BitsOf1S = FiftyThreeBitsOf1S + 1.0;
        private const double InverseOnePlus53BitsOf1S = 1.0 / OnePlus53BitsOf1S;

        /// <summary>
        /// The size, N32 = 4 * (MEXP / 128 + 1), of a state array when regarded as an array of 32-bit integers.
        /// </summary>
        private readonly int stateSize32;

        /// <summary>
        /// The pick up position of the array.
        /// </summary>
        private readonly int pos1;

        /// <summary>
        /// The parameter of shift left as four 32-bit registers.
        /// </summary>
        private readonly int sl1;

        /// <summary>
        /// The parameter of shift left as one 128-bit register. The 128-bit integer is shifted by (sl2 * 8) bits.
        /// </summary>
        private readonly int sl2;

        /// <summary>
        /// The parameter of shift right as four 32-bit registers.
        /// </summary>
        private readonly int sr1;

        /// <summary>
        /// The parameter of shift right as one 128-bit register. The 128-bit integer is shifted by (sr2 * 8) bits.
        /// </summary>
        private readonly int sr2;

        /// <summary>
        /// Stores the state array.
        /// </summary>
        private readonly uint[] sfmt;

        /// <summary>
        /// Temporary 128-bit word.
        /// </summary>
        private readonly uint[] tempX = new uint[4];

        /// <summary>
        /// Temporary 128-bit word.
        /// </summary>
        private readonly uint[] tempY = new uint[4];

        /// <summary>
        /// A 128-bit bitmask (MSK1, MSK2, MSK3, MSK4) used in the recursion. Is introduced to break symmetry of SIMD.
        /// </summary>
        private readonly uint[] msk;

        /// <summary>
        /// A 128-bit period certification vector.
        /// </summary>
        private readonly uint[] parity;

        /// <summary>
        /// The used seed value.
        /// </summary>
        private readonly uint seedValue;

        /// <summary>
        /// The used seed array.
        /// </summary>
        private readonly uint[] seedArray;

        /// <summary>
        /// An index to the 32-bit word within 128-bit structure.
        /// </summary>
        private int idx;

        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwisterSfmtEngine"/> class.
        /// </summary>
        /// <param name="mersenneExponent">The Mersenne Exponent, MEXP. The period of the sequence is a multiple of 2ᵐᶱˣᵖ-1.</param>
        /// <param name="pos1">The pick up position of the array.</param>
        /// <param name="sl1">The first parameter of shift left as four 32-bit registers. The 128-bit integer is shifted by (sl2 * 8) bits.</param>
        /// <param name="sl2">The second parameter of shift left as four 32-bit registers. The 128-bit integer is shifted by (sl2 * 8) bits.</param>
        /// <param name="sr1">The first parameter of shift right as one 128-bit register. The 128-bit integer is shifted by (sr2 * 8) bits.</param>
        /// <param name="sr2">The second parameter of shift right as one 128-bit register. The 128-bit integer is shifted by (sr2 * 8) bits.</param>
        /// <param name="msk1">A bitmask, MSK1, used in the recursion. Is introduced to break symmetry of SIMD.</param>
        /// <param name="msk2">A bitmask, MSK2, used in the recursion. Is introduced to break symmetry of SIMD.</param>
        /// <param name="msk3">A bitmask, MSK3, used in the recursion. Is introduced to break symmetry of SIMD.</param>
        /// <param name="msk4">A bitmask, MSK4, used in the recursion. Is introduced to break symmetry of SIMD.</param>
        /// <param name="parity1">This definition, PARITY1, is a part of a 128-bit period certification vector.</param>
        /// <param name="parity2">This definition, PARITY2, is a part of a 128-bit period certification vector.</param>
        /// <param name="parity3">This definition, PARITY3, is a part of a 128-bit period certification vector.</param>
        /// <param name="parity4">This definition, PARITY4, is a part of a 128-bit period certification vector.</param>
        /// <param name="seed">A number used to calculate a starting value for the pseudo-random number sequence. If a negative number is specified, the absolute value of the number is used.</param>
        protected MersenneTwisterSfmtEngine(
            int mersenneExponent,
            int pos1,
            int sl1,
            int sl2,
            int sr1,
            int sr2,
            uint msk1,
            uint msk2,
            uint msk3,
            uint msk4,
            uint parity1,
            uint parity2,
            uint parity3,
            uint parity4,
            int seed)
        {
            // The size, N, of an array of 128-bit integers. N = (MEXP / 128 + 1).
            int stateSize128 = mersenneExponent / 128 + 1;
            stateSize32 = stateSize128 * 4;
            sfmt = new uint[stateSize32];
            this.pos1 = pos1;
            this.sl1 = sl1;
            this.sl2 = sl2;
            this.sr1 = sr1;
            this.sr2 = sr2;
            msk = new[] { msk1, msk2, msk3, msk4 };
            parity = new[] { parity1, parity2, parity3, parity4 };
            seedValue = (uint)seed;
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwisterSfmtEngine"/> class.
        /// </summary>
        /// <param name="mersenneExponent">The Mersenne Exponent, MEXP. The period of the sequence is a multiple of 2ᵐᶱˣᵖ-1.</param>
        /// <param name="pos1">The pick up position of the array.</param>
        /// <param name="sl1">The first parameter of shift left as four 32-bit registers. The 128-bit integer is shifted by (sl2 * 8) bits.</param>
        /// <param name="sl2">The second parameter of shift left as four 32-bit registers. The 128-bit integer is shifted by (sl2 * 8) bits.</param>
        /// <param name="sr1">The first parameter of shift right as one 128-bit register. The 128-bit integer is shifted by (sr2 * 8) bits.</param>
        /// <param name="sr2">The second parameter of shift right as one 128-bit register. The 128-bit integer is shifted by (sr2 * 8) bits.</param>
        /// <param name="msk1">A bitmask, MSK1, used in the recursion. Is introduced to break symmetry of SIMD.</param>
        /// <param name="msk2">A bitmask, MSK2, used in the recursion. Is introduced to break symmetry of SIMD.</param>
        /// <param name="msk3">A bitmask, MSK3, used in the recursion. Is introduced to break symmetry of SIMD.</param>
        /// <param name="msk4">A bitmask, MSK4, used in the recursion. Is introduced to break symmetry of SIMD.</param>
        /// <param name="parity1">This definition, PARITY1, is a part of a 128-bit period certification vector.</param>
        /// <param name="parity2">This definition, PARITY2, is a part of a 128-bit period certification vector.</param>
        /// <param name="parity3">This definition, PARITY3, is a part of a 128-bit period certification vector.</param>
        /// <param name="parity4">This definition, PARITY4, is a part of a 128-bit period certification vector.</param>
        /// <param name="seedArray">An array of numbers used to calculate a starting values for the pseudo-random number sequence. If negative numbers are specified, the absolute values of them are used.</param>
        protected MersenneTwisterSfmtEngine(
            int mersenneExponent,
            int pos1,
            int sl1,
            int sl2,
            int sr1,
            int sr2,
            uint msk1,
            uint msk2,
            uint msk3,
            uint msk4,
            uint parity1,
            uint parity2,
            uint parity3,
            uint parity4,
            int[] seedArray)
        {
            // The size, N, of an array of 128-bit integers. N = (MEXP / 128 + 1).
            int stateSize128 = mersenneExponent / 128 + 1;
            stateSize32 = stateSize128 * 4;
            sfmt = new uint[stateSize32];
            this.pos1 = pos1;
            this.sl1 = sl1;
            this.sl2 = sl2;
            this.sr1 = sr1;
            this.sr2 = sr2;
            msk = new[] { msk1, msk2, msk3, msk4 };
            parity = new[] { parity1, parity2, parity3, parity4 };
            this.seedArray = new uint[seedArray.Length];
            for (int index = 0; index < seedArray.Length; ++index)
            {
                this.seedArray[index] = (uint)seedArray[index];
            }

            Init();
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="MersenneTwisterSfmtEngine"/> can be reset, so that it produces the same pseudo-random number sequence again.
        /// </summary>
        public override bool CanReset => true;

        /// <summary>
        /// Resets the <see cref="MersenneTwisterSfmtEngine"/>, so that it produces the same pseudo-random number sequence again.
        /// </summary>
        public override void Reset()
        {
            Init();
        }

        /// <summary>
        /// A next random 32-bit unsigned integer ∊[<see cref="uint.MinValue"/>, <see cref="uint.MaxValue"/>].
        /// </summary>
        /// <returns>A next random 32-bit unsigned integer.</returns>
        public override uint NextUInt()
        {
            if (idx >= stateSize32)
            {
                Regenerate();
                idx = 0;
            }

            return sfmt[idx++];
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
        /// A double-precision floating point random number ∊[0.0, 1.0).
        /// </summary>
        /// <returns>A double-precision floating point random number.</returns>
        public override double NextDouble()
        {
            return Next53Bit(0, InverseOnePlus53BitsOf1S);
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

#pragma warning disable SA1139 // Use literal suffix notation instead of casting
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint Func1(uint x)
        {
            return (x ^ (x >> 27)) * (uint)1664525UL;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint Func2(uint x)
        {
            return (x ^ (x >> 27)) * (uint)1566083941UL;
        }
#pragma warning restore SA1139

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Init()
        {
            if (seedArray == null)
            {
                uint u = seedValue;
                sfmt[0] = u;
                for (int i = 1; i != stateSize32; ++i)
                {
                    u = (uint)(1812433253UL * (u ^ (u >> 30)) + (uint)i);
                    sfmt[i] = u;
                }
            }
            else
            {
                int lag, count, i, j;
                if (stateSize32 >= 623)
                {
                    lag = 11;
                }
                else if (stateSize32 >= 68)
                {
                    lag = 7;
                }
                else if (stateSize32 >= 39)
                {
                    lag = 5;
                }
                else
                {
                    lag = 3;
                }

                int mid = (stateSize32 - lag) / 2;
                for (i = 0; i != stateSize32; ++i)
                {
                    sfmt[i] = 0x8b8b8b8bU;
                }

                int length = seedArray.Length;
                if (length + 1 > stateSize32)
                {
                    count = length;
                }
                else
                {
                    count = stateSize32 - 1;
                }

                uint r = Func1(sfmt[0] ^ sfmt[mid] ^ sfmt[stateSize32 - 1]);
                sfmt[mid] += r;
                r += (uint)length;
                sfmt[mid + lag] += r;
                sfmt[0] = r;
                for (i = 1, j = 0; j < count && j < length; ++j)
                {
                    r = Func1(sfmt[i] ^ sfmt[(i + mid) % stateSize32] ^ sfmt[(i + stateSize32 - 1) % stateSize32]);
                    sfmt[(i + mid) % stateSize32] += r;
                    r += seedArray[j] + (uint)i;
                    sfmt[(i + mid + lag) % stateSize32] += r;
                    sfmt[i] = r;
                    i = ++i % stateSize32;
                }

                for (; j != count; ++j)
                {
                    r = Func1(sfmt[i] ^ sfmt[(i + mid) % stateSize32] ^ sfmt[(i + stateSize32 - 1) % stateSize32]);
                    sfmt[(i + mid) % stateSize32] += r;
                    r += (uint)i;
                    sfmt[(i + mid + lag) % stateSize32] += r;
                    sfmt[i] = r;
                    i = ++i % stateSize32;
                }

                for (j = 0; j != stateSize32; ++j)
                {
                    r = Func2(sfmt[i] + sfmt[(i + mid) % stateSize32] + sfmt[(i + stateSize32 - 1) % stateSize32]);
                    sfmt[(i + mid) % stateSize32] ^= r;
                    r -= (uint)i;
                    sfmt[(i + mid + lag) % stateSize32] ^= r;
                    sfmt[i] = r;
                    i = ++i % stateSize32;
                }
            }

            idx = stateSize32;
            PeriodCertification();

            // Reset helper variables used for generation of random booleans.
            BitBuffer = 0;
            BitCount = 32;
        }

        /// <summary>
        /// Simulates the little endian SIMD 128-bit left shift.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void LeftShift128(uint[] target, int source, int shift)
        {
            ulong th = ((ulong)sfmt[source + 3] << 32) | sfmt[source + 2];
            ulong tl = ((ulong)sfmt[source + 1] << 32) | sfmt[source];
            int s = shift * 8;
            ulong oh = th << s;
            ulong ol = tl << s;
            oh |= tl >> (64 - s);
            target[1] = (uint)(ol >> 32);
            target[0] = (uint)ol;
            target[3] = (uint)(oh >> 32);
            target[2] = (uint)oh;
        }

        /// <summary>
        /// Simulates the little endian SIMD 128-bit right shift.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void RightShift128(uint[] target, int source, int shift)
        {
            ulong th = ((ulong)sfmt[source + 3] << 32) | sfmt[source + 2];
            ulong tl = ((ulong)sfmt[source + 1] << 32) | sfmt[source];
            int s = shift * 8;
            ulong oh = th >> s;
            ulong ol = tl >> s;
            ol |= th << (64 - s);
            target[1] = (uint)(ol >> 32);
            target[0] = (uint)ol;
            target[3] = (uint)(oh >> 32);
            target[2] = (uint)oh;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void DoRecursion(int a, int b, int c, int d)
        {
            LeftShift128(tempX, a, sl2);
            RightShift128(tempY, c, sr2);
            for (int i = 0; i != 4; ++i)
            {
                sfmt[a + i] = sfmt[a + i] ^ tempX[i] ^ ((sfmt[b + i] >> sr1) & msk[i]) ^ tempY[i] ^ (sfmt[d + i] << sl1);
            }
        }

        /// <summary>
        /// Fills the internal state array.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Regenerate()
        {
            int r1 = stateSize32 - 8;
            int r2 = stateSize32 - 4;
            int p = pos1 * 4, k = stateSize32 - p;
            for (int i = 0; i != k; i += 4)
            {
                DoRecursion(i, i + p, r1, r2);
                r1 = r2;
                r2 = i;
            }

            for (int i = k; i != stateSize32; i += 4)
            {
                DoRecursion(i, i - k, r1, r2);
                r1 = r2;
                r2 = i;
            }
        }

        /// <summary>
        /// Certificate the period of 2ᵐᶱˣᵖ.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void PeriodCertification()
        {
            int inner = 0;
            for (int i = 0; i != 4; ++i)
            {
                inner ^= (int)(sfmt[i] & parity[i]);
            }

            for (int i = 16; i > 0; i >>= 1)
            {
                inner ^= inner >> i;
            }

            inner &= 1;

            // Check OK.
            if (inner == 1)
            {
                return;
            }

            // Check NG, and modification.
            for (int i = 0; i != 4; ++i)
            {
                uint work = 1, p = parity[i];
                for (int j = 0; j != 32; ++j)
                {
                    if ((work & p) != 0)
                    {
                        sfmt[i] ^= work;
                        return;
                    }

                    work <<= 1;
                }
            }
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
        }
    }
}
