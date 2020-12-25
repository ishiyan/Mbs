using System;

namespace Mbs.Numerics.RandomGenerators.MersenneTwister
{
    /// <summary>
    /// The SIMD-oriented Fast Mersenne Twister (SFMT) uniform sfmt86243 pseudo-random number generator with the period of 2⁸⁶²⁴³-1
    /// is based upon information and the implementation presented on:
    /// <para />
    /// http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/SFMT/index.html.
    /// </summary>
    public sealed class MersenneTwisterSfmt86243UniformRandom : MersenneTwisterSfmtEngine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwisterSfmt86243UniformRandom"/> class, using the current system tick count as a seed value.
        /// </summary>
        public MersenneTwisterSfmt86243UniformRandom()
            : this(Environment.TickCount)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwisterSfmt86243UniformRandom"/> class,
        /// using the specified seed value.
        /// </summary>
        /// <param name="seed">A number used to calculate a starting value for the pseudo-random number sequence.</param>
        public MersenneTwisterSfmt86243UniformRandom(int seed)
            : base(
                  86243,
                  366,
                  6,
                  7,
                  19,
                  1,
                  0xfdbffbffU,
                  0xbff7ff3fU,
                  0xfd77efffU,
                  0xbf9ff3ffU,
                  0x00000001U,
                  0x00000000U,
                  0x00000000U,
                  0xe9528d85U,
                  seed)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwisterSfmt86243UniformRandom"/> class,
        /// using the specified seed array.
        /// </summary>
        /// <param name="seedArray">An array of numbers used to calculate a starting values for the pseudo-random number sequence.</param>
        public MersenneTwisterSfmt86243UniformRandom(int[] seedArray)
            : base(
                  86243,
                  366,
                  6,
                  7,
                  19,
                  1,
                  0xfdbffbffU,
                  0xbff7ff3fU,
                  0xfd77efffU,
                  0xbf9ff3ffU,
                  0x00000001U,
                  0x00000000U,
                  0x00000000U,
                  0xe9528d85U,
                  seedArray)
        {
        }
    }
}
