namespace Mbs.Numerics.Random
{
    /// <summary>
    /// The Well44497b pseudo-random number generator as described in a paper by François Panneton, Pierre L'Ecuyer and Makoto Matsumoto.
    /// <para />
    /// http://www.iro.umontreal.ca/~lecuyer/myftp/papers/wellrng.pdf
    /// <para />
    /// Improved Long-Period Generators Based on Linear Recurrences Modulo 2, ACM Transactions on Mathematical Software, 32, 1 (2006).
    /// <para />
    /// The errata for the paper are in
    /// <para />
    /// http://www.iro.umontreal.ca/~lecuyer/myftp/papers/wellrng-errata.txt
    /// <para />
    /// See also http://www.iro.umontreal.ca/~panneton/WELLRNG.html.
    /// </summary>
    public sealed class Well44497BUniformRandom : WellEngine
    {
        /// <summary>
        /// Number of bits in the pool.
        /// </summary>
        private const int K = 44497;

        /// <summary>
        /// The first parameter of the algorithm.
        /// </summary>
        private const int M1 = 23;

        /// <summary>
        /// The second parameter of the algorithm.
        /// </summary>
        private const int M2 = 481;

        /// <summary>
        /// The third parameter of the algorithm.
        /// </summary>
        private const int M3 = 229;

        /// <summary>
        /// Initializes a new instance of the <see cref="Well44497BUniformRandom"/> class.
        /// </summary>
        public Well44497BUniformRandom()
            : base(K, M1, M2, M3)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Well44497BUniformRandom"/> class.
        /// </summary>
        /// <param name="seed">The initial seed value.</param>
        public Well44497BUniformRandom(int seed)
            : base(K, M1, M2, M3, seed)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Well44497BUniformRandom"/> class.
        /// </summary>
        /// <param name="seed">The initial seed value.</param>
        public Well44497BUniformRandom(long seed)
            : base(K, M1, M2, M3, seed)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Well44497BUniformRandom"/> class.
        /// </summary>
        /// <param name="seedArray">The initial seed (32 bits integers array), if null the seed of the generator will be related to the current time.</param>
        public Well44497BUniformRandom(int[] seedArray)
            : base(K, M1, M2, M3, seedArray)
        {
        }

        /// <summary>
        /// A next random 32-bit unsigned integer ∊[<see cref="uint.MinValue"/>, <see cref="uint.MaxValue"/>].
        /// </summary>
        /// <returns>A next random 32-bit unsigned integer.</returns>
        public override uint NextUInt()
        {
            // Compute raw value given by WELL44497a generator which is NOT maximally-equidistributed.
            int indexRm1 = Irm1[Index];
            int indexRm2 = Irm2[Index];

            uint v0 = V[Index];
            uint vM1 = V[I1[Index]];
            uint vM2 = V[I2[Index]];
            uint vM3 = V[I3[Index]];

            // The values below include the errata of the original article.
            var z0 = (0xFFFF8000U & V[indexRm1]) ^ (0x00007FFFU & V[indexRm2]);
            uint z1 = (v0 ^ (v0 << 24)) ^ (vM1 ^ (vM1 >> 30));
            uint z2 = (vM2 ^ (vM2 << 10)) ^ (vM3 << 26);
            uint z3 = z1 ^ z2;
            var z2Prime = ((z2 << 9) ^ (z2 >> 23)) & 0xfbffffffU;
            uint z2Second = ((z2 & 0x00020000U) != 0L) ? (z2Prime ^ 0xb729fcecU) : z2Prime;
            uint z4 = z0 ^ (z1 ^ (z1 >> 20)) ^ z2Second ^ z3;

            V[Index] = z3;
            V[indexRm1] = z4;
            V[indexRm2] &= 0xFFFF8000U;
            Index = indexRm1;

            // Add Matsumoto-Kurita tempering to get a maximally-equidistributed generator.
            z4 = z4 ^ ((z4 << 7) & 0x93dd1400U);
            z4 = z4 ^ ((z4 << 15) & 0xfa118000U);

            return z4;
        }
    }
}
