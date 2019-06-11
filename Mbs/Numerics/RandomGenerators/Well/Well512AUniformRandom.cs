namespace Mbs.Numerics.Random
{
    /// <summary>
    /// The Well512a pseudo-random number generator as described in a paper by François Panneton, Pierre L'Ecuyer and Makoto Matsumoto.
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
    public sealed class Well512AUniformRandom : WellEngine
    {
        /// <summary>
        /// Number of bits in the pool.
        /// </summary>
        private const int K = 512;

        /// <summary>
        /// The first parameter of the algorithm.
        /// </summary>
        private const int M1 = 13;

        /// <summary>
        /// The second parameter of the algorithm.
        /// </summary>
        private const int M2 = 9;

        /// <summary>
        /// The third parameter of the algorithm.
        /// </summary>
        private const int M3 = 5;

        /// <summary>
        /// Initializes a new instance of the <see cref="Well512AUniformRandom"/> class.
        /// </summary>
        public Well512AUniformRandom()
            : base(K, M1, M2, M3)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Well512AUniformRandom"/> class.
        /// </summary>
        /// <param name="seed">The initial seed value.</param>
        public Well512AUniformRandom(int seed)
            : base(K, M1, M2, M3, seed)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Well512AUniformRandom"/> class.
        /// </summary>
        /// <param name="seed">The initial seed value.</param>
        public Well512AUniformRandom(long seed)
            : base(K, M1, M2, M3, seed)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Well512AUniformRandom"/> class.
        /// </summary>
        /// <param name="seedArray">The initial seed (32 bits integers array), if null the seed of the generator will be related to the current time.</param>
        public Well512AUniformRandom(int[] seedArray)
            : base(K, M1, M2, M3, seedArray)
        {
        }

        /// <summary>
        /// A next random 32-bit unsigned integer ∊[<see cref="uint.MinValue"/>, <see cref="uint.MaxValue"/>].
        /// </summary>
        /// <returns>A next random 32-bit unsigned integer.</returns>
        public override uint NextUInt()
        {
            int indexRm1 = Irm1[Index];

            uint vi = V[Index];
            uint vi1 = V[I1[Index]];
            uint vi2 = V[I2[Index]];
            uint z0 = V[indexRm1];

            // The values below include the errata of the original article.
            uint z1 = (vi ^ (vi << 16)) ^ (vi1 ^ (vi1 << 15));
            uint z2 = vi2 ^ (vi2 >> 11);
            uint z3 = z1 ^ z2;
            uint z4 = (z0 ^ (z0 << 2)) ^ (z1 ^ (z1 << 18)) ^ (z2 << 28) ^ (z3 ^ ((z3 << 5) & 0xda442d24));

            V[Index] = z3;
            V[indexRm1] = z4;
            Index = indexRm1;
            return z4;
        }
    }
}
