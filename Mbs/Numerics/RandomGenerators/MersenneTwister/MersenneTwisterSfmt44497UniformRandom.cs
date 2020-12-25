using System;

namespace Mbs.Numerics.RandomGenerators.MersenneTwister
{
    /// <summary>
    /// The SIMD-oriented Fast Mersenne Twister (SFMT) uniform sfmt44497 pseudo-random number generator with the period of 2⁴⁴⁴⁹⁷-1
    /// is based upon information and the implementation presented on:
    /// <para />
    /// http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/SFMT/index.html.
    /// </summary>
    public sealed class MersenneTwisterSfmt44497UniformRandom : MersenneTwisterSfmtEngine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwisterSfmt44497UniformRandom"/> class,
        /// using the current system tick count as a seed value.
        /// </summary>
        public MersenneTwisterSfmt44497UniformRandom()
            : this(Environment.TickCount)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwisterSfmt44497UniformRandom"/> class,
        /// using the specified seed value.
        /// </summary>
        /// <param name="seed">A number used to calculate a starting value for the pseudo-random number sequence.</param>
        public MersenneTwisterSfmt44497UniformRandom(int seed)
            : base(
                  44497,
                  330,
                  5,
                  3,
                  9,
                  3,
                  0xeffffffbU,
                  0xdfbebfffU,
                  0xbfbf7befU,
                  0x9ffd7bffU,
                  0x00000001U,
                  0x00000000U,
                  0xa3ac4000U,
                  0xecc1327aU,
                  seed)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwisterSfmt44497UniformRandom"/> class,
        /// using the specified seed array.
        /// </summary>
        /// <param name="seedArray">An array of numbers used to calculate a starting values for the pseudo-random number sequence.</param>
        public MersenneTwisterSfmt44497UniformRandom(int[] seedArray)
            : base(
                  44497,
                  330,
                  5,
                  3,
                  9,
                  3,
                  0xeffffffbU,
                  0xdfbebfffU,
                  0xbfbf7befU,
                  0x9ffd7bffU,
                  0x00000001U,
                  0x00000000U,
                  0xa3ac4000U,
                  0xecc1327aU,
                  seedArray)
        {
        }
    }
}
