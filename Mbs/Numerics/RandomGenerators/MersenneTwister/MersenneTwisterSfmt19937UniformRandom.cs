using System;

namespace Mbs.Numerics.Random
{
    /// <summary>
    /// The SIMD-oriented Fast Mersenne Twister(SFMT) uniform sfmt19937 pseudo-random number generator with the period of 2¹⁹⁹³⁷-1
    /// is based upon information and the implementation presented on
    /// <para />
    /// http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/SFMT/index.html.
    /// </summary>
    public sealed class MersenneTwisterSfmt19937UniformRandom : MersenneTwisterSfmtEngine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwisterSfmt19937UniformRandom"/> class.
        /// </summary>
        public MersenneTwisterSfmt19937UniformRandom()
            : this(Environment.TickCount)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwisterSfmt19937UniformRandom"/> class.
        /// </summary>
        /// <param name="seed">A number used to calculate a starting value for the pseudo-random number sequence.</param>
        public MersenneTwisterSfmt19937UniformRandom(int seed)
            : base(
                  19937,
                  122,
                  18,
                  1,
                  11,
                  1,
                  0xdfffffefU,
                  0xddfecb7fU,
                  0xbffaffffU,
                  0xbffffff6U,
                  0x00000001U,
                  0x00000000U,
                  0x00000000U,
                  0x13c9e684U,
                  seed)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwisterSfmt19937UniformRandom"/> class, using the specified seed array.
        /// </summary>
        /// <param name="seedArray">An array of numbers used to calculate a starting values for the pseudo-random number sequence.</param>
        public MersenneTwisterSfmt19937UniformRandom(int[] seedArray)
            : base(
                  19937,
                  122,
                  18,
                  1,
                  11,
                  1,
                  0xdfffffefU,
                  0xddfecb7fU,
                  0xbffaffffU,
                  0xbffffff6U,
                  0x00000001U,
                  0x00000000U,
                  0x00000000U,
                  0x13c9e684U,
                  seedArray)
        {
        }
    }
}
