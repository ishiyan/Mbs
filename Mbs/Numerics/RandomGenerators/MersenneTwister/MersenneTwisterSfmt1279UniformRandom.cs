using System;

namespace Mbs.Numerics.Random
{
    /// <summary>
    /// The SIMD-oriented Fast Mersenne Twister (SFMT) uniform sfmt1279 pseudo-random number generator with the period of 2¹²⁷⁹-1
    /// is based upon information and the implementation presented on
    /// <para />
    /// http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/SFMT/index.html.
    /// </summary>
    public sealed class MersenneTwisterSfmt1279UniformRandom : MersenneTwisterSfmtEngine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwisterSfmt1279UniformRandom"/> class.
        /// </summary>
        public MersenneTwisterSfmt1279UniformRandom()
            : this(Environment.TickCount)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwisterSfmt1279UniformRandom"/> class.
        /// </summary>
        /// <param name="seed">A number used to calculate a starting value for the pseudo-random number sequence.</param>
        public MersenneTwisterSfmt1279UniformRandom(int seed)
            : base(
                  1279,
                  7,
                  14,
                  3,
                  5,
                  1,
                  0xf7fefffdU,
                  0x7fefcfffU,
                  0xaff3ef3fU,
                  0xb5ffff7fU,
                  0x00000001U,
                  0x00000000U,
                  0x00000000U,
                  0x20000000U,
                  seed)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwisterSfmt1279UniformRandom"/> class, using the specified seed array.
        /// </summary>
        /// <param name="seedArray">An array of numbers used to calculate a starting values for the pseudo-random number sequence.</param>
        public MersenneTwisterSfmt1279UniformRandom(int[] seedArray)
            : base(
                  1279,
                  7,
                  14,
                  3,
                  5,
                  1,
                  0xf7fefffdU,
                  0x7fefcfffU,
                  0xaff3ef3fU,
                  0xb5ffff7fU,
                  0x00000001U,
                  0x00000000U,
                  0x00000000U,
                  0x20000000U,
                  seedArray)
        {
        }
    }
}
