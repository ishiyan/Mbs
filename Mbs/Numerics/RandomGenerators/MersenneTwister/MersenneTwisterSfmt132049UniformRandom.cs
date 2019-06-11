using System;

namespace Mbs.Numerics.Random
{
    /// <summary>
    /// The SIMD-oriented Fast Mersenne Twister (SFMT) uniform sfmt132049 pseudo-random number generator with the period of 2¹³²⁰⁴⁹-1
    /// is based upon information and the implementation presented on
    /// <para />
    /// http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/SFMT/index.html.
    /// </summary>
    public sealed class MersenneTwisterSfmt132049UniformRandom : MersenneTwisterSfmtEngine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwisterSfmt132049UniformRandom"/> class.
        /// </summary>
        public MersenneTwisterSfmt132049UniformRandom()
            : this(Environment.TickCount)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwisterSfmt132049UniformRandom"/> class.
        /// </summary>
        /// <param name="seed">A number used to calculate a starting value for the pseudo-random number sequence.</param>
        public MersenneTwisterSfmt132049UniformRandom(int seed)
            : base(
                  132049,
                  110,
                  19,
                  1,
                  21,
                  1,
                  0xffffbb5fU,
                  0xfb6ebf95U,
                  0xfffefffaU,
                  0xcff77fffU,
                  0x00000001U,
                  0x00000000U,
                  0xcb520000U,
                  0xc7e91c7dU,
                  seed)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwisterSfmt132049UniformRandom"/> class, using the specified seed array.
        /// </summary>
        /// <param name="seedArray">An array of numbers used to calculate a starting values for the pseudo-random number sequence.</param>
        public MersenneTwisterSfmt132049UniformRandom(int[] seedArray)
            : base(
                  132049,
                  110,
                  19,
                  1,
                  21,
                  1,
                  0xffffbb5fU,
                  0xfb6ebf95U,
                  0xfffefffaU,
                  0xcff77fffU,
                  0x00000001U,
                  0x00000000U,
                  0xcb520000U,
                  0xc7e91c7dU,
                  seedArray)
        {
        }
    }
}
