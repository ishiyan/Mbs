using System;
using System.Runtime.CompilerServices;

namespace Mbs.Numerics.RandomGenerators.Well
{
    /// <summary>
    /// The WELL class of pseudo-random number generators engine as described in a paper by François Panneton, Pierre L'Ecuyer and Makoto Matsumoto:
    /// <para />
    /// http://www.iro.umontreal.ca/~lecuyer/myftp/papers/wellrng.pdf.
    /// <para />
    /// Improved Long-Period Generators Based on Linear Recurrences Modulo 2, ACM Transactions on Mathematical Software, 32, 1 (2006).
    /// <para />
    /// The errata for the paper are in:
    /// <para />
    /// http://www.iro.umontreal.ca/~lecuyer/myftp/papers/wellrng-errata.txt.
    /// <para />
    /// See also http://www.iro.umontreal.ca/~panneton/WELLRNG.html.
    /// </summary>
    public abstract class WellEngine : RandomGenerator
    {
        private readonly uint[] seedArray;

        /// <summary>
        /// Initializes a new instance of the <see cref="WellEngine"/> class.
        /// </summary>
        /// <param name="k">The number of bits in the pool (not necessarily a multiple of 32).</param>
        /// <param name="m1">The first parameter of the algorithm.</param>
        /// <param name="m2">The second parameter of the algorithm.</param>
        /// <param name="m3">The third parameter of the algorithm.</param>
        /// <param name="seed">The initial seed (32 bits integer).</param>
        protected WellEngine(int k, int m1, int m2, int m3, int seed)
            : this(k, m1, m2, m3, new[] { seed })
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WellEngine"/> class.
        /// </summary>
        /// <param name="k">The number of bits in the pool (not necessarily a multiple of 32).</param>
        /// <param name="m1">The first parameter of the algorithm.</param>
        /// <param name="m2">The second parameter of the algorithm.</param>
        /// <param name="m3">The third parameter of the algorithm.</param>
        /// <param name="seed">The initial seed (64 bits integer).</param>
        protected WellEngine(int k, int m1, int m2, int m3, long seed)
            : this(k, m1, m2, m3, new[] { (int)(seed >> 32), (int)(seed & 0xffffffffL) })
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WellEngine"/> class.
        /// </summary>
        /// <param name="k">The number of bits in the pool (not necessarily a multiple of 32).</param>
        /// <param name="m1">The first parameter of the algorithm.</param>
        /// <param name="m2">The second parameter of the algorithm.</param>
        /// <param name="m3">The third parameter of the algorithm.</param>
        /// <param name="seedArray">The initial seed (32 bits integers array), if null the seed of the generator will be related to the current time.</param>
        protected WellEngine(int k, int m1, int m2, int m3, int[] seedArray = null)
        {
            // The bits pool contains k bits, k = r w - p where r is the number
            // of w bits blocks, w is the block size (always 32 in the original paper)
            // and p is the number of unused bits in the last block.
            const int w = 32;
            int r = (k + w - 1) / w;
            V = new uint[r];

            // Precompute indirection index tables. These tables are used for optimizing access
            // they allow saving computations like "(j + r - 2) % r" with costly modulo operations.
            Irm1 = new int[r];
            Irm2 = new int[r];
            I1 = new int[r];
            I2 = new int[r];
            I3 = new int[r];
            for (int j = 0; j != r; ++j)
            {
                Irm1[j] = (j + r - 1) % r;
                Irm2[j] = (j + r - 2) % r;
                I1[j] = (j + m1) % r;
                I2[j] = (j + m2) % r;
                I3[j] = (j + m3) % r;
            }

            if (seedArray == null)
            {
                this.seedArray = new[] { (uint)Environment.TickCount };
            }
            else
            {
                int l = seedArray.Length;
                this.seedArray = new uint[l];

                for (int j = 0; j != l; ++j)
                {
                    this.seedArray[j] = (uint)seedArray[j];
                }
            }

            Init();
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="WellEngine"/> can be reset,
        /// so that it produces the same pseudo-random number sequence again.
        /// </summary>
        public override bool CanReset => true;

#pragma warning disable CA1819 // Properties should not return arrays
        /// <summary>
        /// Gets the bytes pool.
        /// </summary>
        protected uint[] V { get; }

        /// <summary>
        /// Gets index indirection table giving for each index its predecessor taking table size into account.
        /// </summary>
        protected int[] Irm1 { get; }

        /// <summary>
        /// Gets index indirection table giving for each index its second predecessor taking table size into account.
        /// </summary>
        protected int[] Irm2 { get; }

        /// <summary>
        /// Gets index indirection table giving for each index the value index + m1 taking table size into account.
        /// </summary>
        protected int[] I1 { get; }

        /// <summary>
        /// Gets index indirection table giving for each index the value index + m2 taking table size into account.
        /// </summary>
        protected int[] I2 { get; }

        /// <summary>
        /// Gets index indirection table giving for each index the value index + m3 taking table size into account.
        /// </summary>
        protected int[] I3 { get; }

        /// <summary>
        /// Gets or sets current index in the bytes pool.
        /// </summary>
        protected int Index { get; set; }
#pragma warning restore CA1819

        /// <summary>
        /// Resets the <see cref="WellEngine"/>,
        /// so that it produces the same pseudo-random number sequence again.
        /// </summary>
        public override void Reset()
        {
            Init();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Init()
        {
            // ReSharper disable IdentifierTypo
            uint slen = (uint)seedArray.Length, vlen = (uint)V.Length;

            // ReSharper restore IdentifierTypo
            Array.Copy(seedArray, 0, V, 0, Math.Min(slen, vlen));
            if (slen < vlen)
            {
                for (uint i = slen; i != vlen; ++i)
                {
                    ulong l = V[i - slen];
                    V[i] = (uint)((1812433253UL * (l ^ (l >> 30)) + i) & 0xffffffffUL);
                }
            }

            Index = 0;

            // Reset helper variables used for generation of random booleans.
            BitBuffer = 0;
            BitCount = 0;
        }
    }
}
