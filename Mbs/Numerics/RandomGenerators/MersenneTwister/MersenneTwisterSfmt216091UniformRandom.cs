using System;

namespace Mbs.Numerics.RandomGenerators.MersenneTwister
{
    /// <summary>
    /// The SIMD-oriented Fast Mersenne Twister (SFMT) uniform sfmt216091 pseudo-random number generator with the period of 2²¹⁶⁰⁹¹-1
    /// is based upon information and the implementation presented on:
    /// <para />
    /// http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/SFMT/index.html.
    /// </summary>
    public sealed class MersenneTwisterSfmt216091UniformRandom : MersenneTwisterSfmtEngine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwisterSfmt216091UniformRandom"/> class, using the current system tick count as a seed value.
        /// </summary>
        public MersenneTwisterSfmt216091UniformRandom()
            : this(Environment.TickCount)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwisterSfmt216091UniformRandom"/> class,
        /// using the specified seed value.
        /// </summary>
        /// <param name="seed">A number used to calculate a starting value for the pseudo-random number sequence.</param>
        public MersenneTwisterSfmt216091UniformRandom(int seed)
            : base(
                  216091,
                  627,
                  11,
                  3,
                  10,
                  1,
                  0xbff7bff7U,
                  0xbfffffffU,
                  0xbffffa7fU,
                  0xffddfbfbU,
                  0xf8000001U,
                  0x89e80709U,
                  0x3bd2b64bU,
                  0x0c64b1e4U,
                  seed)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwisterSfmt216091UniformRandom"/> class,
        /// using the specified seed array.
        /// </summary>
        /// <param name="seedArray">An array of numbers used to calculate a starting values for the pseudo-random number sequence.</param>
        public MersenneTwisterSfmt216091UniformRandom(int[] seedArray)
            : base(
                  216091,
                  627,
                  11,
                  3,
                  10,
                  1,
                  0xbff7bff7U,
                  0xbfffffffU,
                  0xbffffa7fU,
                  0xffddfbfbU,
                  0xf8000001U,
                  0x89e80709U,
                  0x3bd2b64bU,
                  0x0c64b1e4U,
                  seedArray)
        {
        }
    }
}
