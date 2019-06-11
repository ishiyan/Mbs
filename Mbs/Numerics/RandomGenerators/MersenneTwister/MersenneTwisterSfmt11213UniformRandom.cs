using System;

namespace Mbs.Numerics.Random
{
    /// <summary>
    /// The SIMD-oriented Fast Mersenne Twister (SFMT) uniform sfmt11213 pseudo-random number generator with the period of 2¹¹²¹³-1
    /// is based upon information and the implementation presented on
    /// <para />
    /// http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/SFMT/index.html.
    /// </summary>
    public sealed class MersenneTwisterSfmt11213UniformRandom : MersenneTwisterSfmtEngine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwisterSfmt11213UniformRandom"/> class.
        /// </summary>
        public MersenneTwisterSfmt11213UniformRandom()
            : this(Environment.TickCount)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwisterSfmt11213UniformRandom"/> class.
        /// </summary>
        /// <param name="seed">A number used to calculate a starting value for the pseudo-random number sequence.</param>
        public MersenneTwisterSfmt11213UniformRandom(int seed)
            : base(
                  11213,
                  68,
                  14,
                  3,
                  7,
                  3,
                  0xeffff7fbU,
                  0xffffffefU,
                  0xdfdfbfffU,
                  0x7fffdbfdU,
                  0x00000001U,
                  0x00000000U,
                  0xe8148000U,
                  0xd0c7afa3U,
                  seed)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwisterSfmt11213UniformRandom"/> class, using the specified seed array.
        /// </summary>
        /// <param name="seedArray">An array of numbers used to calculate a starting values for the pseudo-random number sequence.</param>
        public MersenneTwisterSfmt11213UniformRandom(int[] seedArray)
            : base(
                  11213,
                  68,
                  14,
                  3,
                  7,
                  3,
                  0xeffff7fbU,
                  0xffffffefU,
                  0xdfdfbfffU,
                  0x7fffdbfdU,
                  0x00000001U,
                  0x00000000U,
                  0xe8148000U,
                  0xd0c7afa3U,
                  seedArray)
        {
        }
    }
}
