using System;

namespace Mbs.Numerics.Random
{
    /// <summary>
    /// The SIMD-oriented Fast Mersenne Twister (SFMT) uniform sfmt2281 pseudo-random number generator with the period of 2²²⁸¹-1
    /// is based upon information and the implementation presented on
    /// <para />
    /// http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/SFMT/index.html.
    /// </summary>
    public sealed class MersenneTwisterSfmt2281UniformRandom : MersenneTwisterSfmtEngine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwisterSfmt2281UniformRandom"/> class, using the current system tick count as a seed value.
        /// </summary>
        public MersenneTwisterSfmt2281UniformRandom()
            : this(Environment.TickCount)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwisterSfmt2281UniformRandom"/> class, using the specified seed value.
        /// </summary>
        /// <param name="seed">A number used to calculate a starting value for the pseudo-random number sequence.</param>
        public MersenneTwisterSfmt2281UniformRandom(int seed)
            : base(
                  2281,
                  12,
                  19,
                  1,
                  5,
                  1,
                  0xbff7ffbfU,
                  0xfdfffffeU,
                  0xf7ffef7fU,
                  0xf2f7cbbfU,
                  0x00000001U,
                  0x00000000U,
                  0x00000000U,
                  0x41dfa600U,
                  seed)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwisterSfmt2281UniformRandom"/> class, using the specified seed array.
        /// </summary>
        /// <param name="seedArray">An array of numbers used to calculate a starting values for the pseudo-random number sequence.</param>
        public MersenneTwisterSfmt2281UniformRandom(int[] seedArray)
            : base(
                  2281,
                  12,
                  19,
                  1,
                  5,
                  1,
                  0xbff7ffbfU,
                  0xfdfffffeU,
                  0xf7ffef7fU,
                  0xf2f7cbbfU,
                  0x00000001U,
                  0x00000000U,
                  0x00000000U,
                  0x41dfa600U,
                  seedArray)
        {
        }
    }
}
