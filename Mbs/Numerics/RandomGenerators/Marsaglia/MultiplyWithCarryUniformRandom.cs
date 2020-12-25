using System;
using System.Runtime.CompilerServices;

namespace Mbs.Numerics.RandomGenerators.Marsaglia
{
    /// <summary>
    /// Based on George Marsaglia's MWC (multiply with carry) uniform pseudo-random number generator from:
    /// <para />
    /// http://www.bobwheeler.com/statistics/Password/MarsagliaPost.txt.
    /// <para />
    /// The  MWC generator concatenates two 16-bit multiply-with-carry generators,
    /// x(n)=36969x(n-1)+carry, y(n)=18000y(n-1)+carry mod 216, has period about
    /// 260 and seems to pass all tests of randomness.
    /// </summary>
    public sealed class MultiplyWithCarryUniformRandom : RandomGenerator
    {
#pragma warning disable SA1139 // Use literal suffix notation instead of casting
        /// <summary>
        /// The default value of <c>W</c>used by Marsaglia.
        /// </summary>
        private const int DefaultW = (int)521288629U;

        /// <summary>
        /// The default value of <c>Z</c>used by Marsaglia.
        /// </summary>
        private const int DefaultZ = (int)362436069U;
#pragma warning restore SA1139

        // These values are not magical, just the default values Marsaglia used.
        // Any pair of unsigned integers should be fine.
        private readonly uint seedW = DefaultW;
        private readonly uint seedZ = DefaultZ;

        private uint w;
        private uint z;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiplyWithCarryUniformRandom"/> class,
        /// using the current system tick count as a seed value.
        /// </summary>
        public MultiplyWithCarryUniformRandom()
        {
            long l = DateTime.Now.ToFileTime();
            seedW = (uint)(l >> 16);
            seedZ = (uint)(l % 4294967296);
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiplyWithCarryUniformRandom"/> class,
        /// using the specified seed value.
        /// </summary>
        /// <param name="seedW">A non-zero seed value of <c>W</c> for the pseudo-random number sequence. If a negative number is specified, the absolute value of the number is used.</param>
        /// <param name="seedZ">A non-zero seed value of <c>Z</c> for the pseudo-random number sequence. If a negative number is specified, the absolute value of the number is used.</param>
        public MultiplyWithCarryUniformRandom(int seedW, int seedZ = DefaultZ)
        {
            w = (uint)Math.Abs(seedW);
            z = (uint)Math.Abs(seedZ);
            Init();
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="MultiplyWithCarryUniformRandom"/> can be reset,
        /// so that it produces the same pseudo-random number sequence again.
        /// </summary>
        public override bool CanReset => true;

        /// <summary>
        /// Resets the <see cref="MultiplyWithCarryUniformRandom"/>,
        /// so that it produces the same pseudo-random number sequence again.
        /// </summary>
        public override void Reset()
        {
            Init();
        }

        /// <summary>
        /// A next random 32-bit unsigned integer ∊[<see cref="uint.MinValue"/>, <see cref="uint.MaxValue"/>).
        /// <para />
        /// See http://www.bobwheeler.com/statistics/Password/MarsagliaPost.txt.
        /// </summary>
        /// <returns>A next random 32-bit unsigned integer.</returns>
        public override uint NextUInt()
        {
            z = 36969 * (z & 65535) + (z >> 16);
            w = 18000 * (w & 65535) + (w >> 16);
            return (z << 16) + w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Init()
        {
            w = seedW;
            z = seedZ;

            // Reset helper variables used for generation of random booleans.
            BitBuffer = 0;
            BitCount = 0;
        }
    }
}
