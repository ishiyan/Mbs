using System;

namespace Mbs.Numerics.RandomGenerators.MersenneTwister
{
    /// <summary>
    /// The SIMD-oriented Fast Mersenne Twister (SFMT) uniform sfmt4253 pseudo-random number generator with the period of 2⁴²⁵³-1
    /// is based upon information and the implementation presented on:
    /// <para />
    /// http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/SFMT/index.html.
    /// </summary>
    public sealed class MersenneTwisterSfmt4253UniformRandom : MersenneTwisterSfmtEngine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwisterSfmt4253UniformRandom"/> class,
        /// using the current system tick count as a seed value.
        /// </summary>
        public MersenneTwisterSfmt4253UniformRandom()
            : this(Environment.TickCount)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwisterSfmt4253UniformRandom"/> class,
        /// using the specified seed value.
        /// </summary>
        /// <param name="seed">A number used to calculate a starting value for the pseudo-random number sequence.</param>
        public MersenneTwisterSfmt4253UniformRandom(int seed)
            : base(
                  4253,
                  17,
                  20,
                  1,
                  7,
                  1,
                  0x9f7bffffU,
                  0x9fffff5fU,
                  0x3efffffbU,
                  0xfffff7bbU,
                  0xa8000001U,
                  0xaf5390a3U,
                  0xb740b3f8U,
                  0x6c11486dU,
                  seed)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwisterSfmt4253UniformRandom"/> class,
        /// using the specified seed array.
        /// </summary>
        /// <param name="seedArray">An array of numbers used to calculate a starting values for the pseudo-random number sequence.</param>
        public MersenneTwisterSfmt4253UniformRandom(int[] seedArray)
            : base(
                  4253,
                  17,
                  20,
                  1,
                  7,
                  1,
                  0x9f7bffffU,
                  0x9fffff5fU,
                  0x3efffffbU,
                  0xfffff7bbU,
                  0xa8000001U,
                  0xaf5390a3U,
                  0xb740b3f8U,
                  0x6c11486dU,
                  seedArray)
        {
        }
    }
}
