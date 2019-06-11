using System;
using System.Runtime.CompilerServices;

namespace Mbs.Numerics.Random
{
    /// <summary>
    /// Based on George Marsaglia's XORshift random generator from
    /// <para />
    /// http://www.jstatsoft.org/v08/i14/.
    /// <para />
    /// This algorithm has a period of 2¹²⁸ − 1 and it passes the Diehard tests very well.
    /// </summary>
    public sealed class XorShiftUniformRandom : RandomGenerator
    {
        /// <summary>
        /// The default value of <c>Y</c> used by Marsaglia.
        /// </summary>
        private const uint DefaultY = 362436069U;

        /// <summary>
        /// The default value of <c>W</c> used by Marsaglia.
        /// </summary>
        private const uint DefaultW = 88675123U;

        /// <summary>
        /// The default value of <c>Z</c> used by Marsaglia.
        /// </summary>
        private const uint DefaultZ = 521288629U;

        private readonly uint seedX;

        private uint x;
        private uint y;
        private uint w;
        private uint z;

        /// <summary>
        /// Initializes a new instance of the <see cref="XorShiftUniformRandom"/> class, using the current system tick count as a seed value.
        /// </summary>
        public XorShiftUniformRandom()
            : this(Environment.TickCount)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XorShiftUniformRandom"/> class, using the specified seed value.
        /// </summary>
        /// <param name="seed">A non-zero seed value of <c>X</c> for the pseudo-random number sequence. If a negative number is specified, the absolute value of the number is used.</param>
        public XorShiftUniformRandom(int seed)
        {
            seedX = (uint)Math.Abs(seed);
            Init();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Init()
        {
            x = seedX;
            y = DefaultY;
            w = DefaultW;
            z = DefaultZ;

            // Reset helper variables used for generation of random bools.
            BitBuffer = 0;
            BitCount = 0;
        }

        /// <summary>
        /// Resets the <see cref="XorShiftUniformRandom"/>, so that it produces the same pseudo-random number sequence again.
        /// </summary>
        public override void Reset()
        {
            Init();
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="XorShiftUniformRandom"/> can be reset, so that it produces the same pseudo-random number sequence again.
        /// </summary>
        public override bool CanReset => true;

        /// <summary>
        /// A next random 32-bit unsigned integer ∊[<see cref="uint.MinValue"/>, <see cref="uint.MaxValue"/>].
        /// </summary>
        /// <returns>A next random 32-bit unsigned integer.</returns>
        public override uint NextUInt()
        {
            uint t = x ^ (x << 11);
            x = y;
            y = z;
            z = w;
            return w = (w ^ (w >> 19)) ^ (t ^ (t >> 8));
        }
    }
}
