namespace Mbs.Numerics.Random
{
    /// <summary>
    /// The Well1024a pseudo-random number generator as described in a paper by François Panneton, Pierre L'Ecuyer and Makoto Matsumoto.
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
    public sealed class Well1024AUniformRandom : WellEngine
    {
        /// <summary>
        /// Number of bits in the pool.
        /// </summary>
        private const int K = 1024;

        /// <summary>
        /// The first parameter of the algorithm.
        /// </summary>
        private const int M1 = 3;

        /// <summary>
        /// The second parameter of the algorithm.
        /// </summary>
        private const int M2 = 24;

        /// <summary>
        /// The third parameter of the algorithm.
        /// </summary>
        private const int M3 = 10;

        /// <summary>
        /// Initializes a new instance of the <see cref="Well1024AUniformRandom"/> class.
        /// </summary>
        public Well1024AUniformRandom()
            : base(K, M1, M2, M3)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Well1024AUniformRandom"/> class.
        /// </summary>
        /// <param name="seed">The initial seed value.</param>
        public Well1024AUniformRandom(int seed)
            : base(K, M1, M2, M3, seed)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Well1024AUniformRandom"/> class.
        /// </summary>
        /// <param name="seed">The initial seed value.</param>
        public Well1024AUniformRandom(long seed)
            : base(K, M1, M2, M3, seed)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Well1024AUniformRandom"/> class.
        /// </summary>
        /// <param name="seedArray">The initial seed (32 bits integers array), if null the seed of the generator will be related to the current time.</param>
        public Well1024AUniformRandom(int[] seedArray)
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

            uint v0 = V[Index];
            uint vM1 = V[I1[Index]];
            uint vM2 = V[I2[Index]];
            uint vM3 = V[I3[Index]];

            var z0 = V[indexRm1];
            uint z1 = v0 ^ (vM1 ^ (vM1 >> 8));
            uint z2 = (vM2 ^ (vM2 << 19)) ^ (vM3 ^ (vM3 << 14));
            uint z3 = z1 ^ z2;
            uint z4 = (z0 ^ (z0 << 11)) ^ (z1 ^ (z1 << 7)) ^ (z2 ^ (z2 << 13));

            V[Index] = z3;
            V[indexRm1] = z4;
            Index = indexRm1;

            return z4;
        }
    }
}
