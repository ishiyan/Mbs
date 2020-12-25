using System;

namespace Mbs.Numerics.RandomGenerators.MersenneTwister
{
    /// <summary>
    /// The SIMD-oriented Fast Mersenne Twister (SFMT) uniform sfmt607 pseudo-random number generator with the period of 2⁶⁰⁷-1
    /// is based upon information and the implementation presented on:
    /// <para />
    /// http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/SFMT/index.html.
    /// </summary>
    public sealed class MersenneTwisterSfmt607UniformRandom : MersenneTwisterSfmtEngine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwisterSfmt607UniformRandom"/> class,
        /// using the current system tick count as a seed value.
        /// </summary>
        public MersenneTwisterSfmt607UniformRandom()
            : this(Environment.TickCount)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwisterSfmt607UniformRandom"/> class,
        /// using the specified seed value.
        /// </summary>
        /// <param name="seed">A number used to calculate a starting value for the pseudo-random number sequence.</param>
        public MersenneTwisterSfmt607UniformRandom(int seed)
            : base(
                  607,
                  2,
                  15,
                  3,
                  13,
                  3,
                  0xfdff37ffU,
                  0xef7f3f7dU,
                  0xff777b7dU,
                  0x7ff7fb2fU,
                  0x00000001U,
                  0x00000000U,
                  0x00000000U,
                  0x5986f054U,
                  seed)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwisterSfmt607UniformRandom"/> class,
        /// using the specified seed array.
        /// </summary>
        /// <param name="seedArray">An array of numbers used to calculate a starting values for the pseudo-random number sequence.</param>
        public MersenneTwisterSfmt607UniformRandom(int[] seedArray)
            : base(
                  607,
                  2,
                  15,
                  3,
                  13,
                  3,
                  0xfdff37ffU,
                  0xef7f3f7dU,
                  0xff777b7dU,
                  0x7ff7fb2fU,
                  0x00000001U,
                  0x00000000U,
                  0x00000000U,
                  0x5986f054U,
                  seedArray)
        {
        }
    }
}
