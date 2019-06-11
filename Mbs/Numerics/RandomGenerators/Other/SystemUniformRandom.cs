using System;
using System.Runtime.CompilerServices;

namespace Mbs.Numerics.Random
{
    /// <summary>
    /// The standard random number generator of the .NET framework.
    /// </summary>
    public sealed class SystemUniformRandom : RandomGenerator
    {
        private readonly int seed;
        private System.Random random;

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemUniformRandom"/> class using the current system tick count as a seed value.
        /// </summary>
        public SystemUniformRandom()
            : this(Environment.TickCount)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemUniformRandom"/> class using the specified seed value.
        /// </summary>
        /// <param name="seed">A seed value of the pseudo-random number sequence.</param>
        public SystemUniformRandom(int seed)
        {
            this.seed = seed;
            Init();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Init()
        {
            random = new System.Random(seed);

            // Reset helper variables used for generation of random bools.
            BitBuffer = 0;
            BitCount = 0;
        }

        /// <summary>
        /// Resets the <see cref="SystemUniformRandom"/>, so that it produces the same pseudo-random number sequence again.
        /// </summary>
        public override void Reset()
        {
            Init();
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="SystemUniformRandom"/> can be reset, so that it produces the same pseudo-random number sequence again.
        /// </summary>
        public override bool CanReset => true;

        /// <summary>
        /// A 32-bit random signed integer ∊[0, <see cref="int.MaxValue"/>).
        /// </summary>
        /// <returns>A 32-bit random signed integer.</returns>
        public override int NextInt()
        {
            return random.Next();
        }

        /// <summary>
        /// A 32-bit random signed integer ∊[0, <paramref name="maxValue"/>).
        /// </summary>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. The <paramref name="maxValue"/> must be greater than or equal to zero.</param>
        /// <returns>A 32-bit random signed integer.</returns>
        public override int NextInt(int maxValue)
        {
            return random.Next(maxValue);
        }

        /// <summary>
        /// A 32-bit random signed integer ∊[<paramref name="minValue"/>, <paramref name="maxValue"/>).
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number to be generated.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. The <paramref name="maxValue"/> must be greater than or equal to <paramref name="minValue"/>.</param>
        /// <returns>A 32-bit random signed integer.</returns>
        public override int NextInt(int minValue, int maxValue)
        {
            return random.Next(minValue, maxValue);
        }

        /// <summary>
        /// A double-precision floating point random number ∊[0.0, 1.0).
        /// </summary>
        /// <returns>A double-precision floating point random number.</returns>
        public override double NextDouble()
        {
            return random.NextDouble();
        }

        /// <summary>
        /// A double-precision floating point random number ∊[0.0, <paramref name="maxValue"/>).
        /// </summary>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. The <paramref name="maxValue"/> must be greater than or equal to 0.0.</param>
        /// <returns>A double-precision floating point random number.</returns>
        public override double NextDouble(double maxValue)
        {
            return random.NextDouble() * maxValue;
        }

        /// <summary>
        /// A double-precision floating point random number ∊[<paramref name="minValue"/>, <paramref name="maxValue"/>).
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number to be generated. The range between <paramref name="minValue"/> and <paramref name="maxValue"/> must be less than or equal to <see cref="double.MaxValue"/>.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. The <paramref name="maxValue"/> must be greater than or equal to <paramref name="minValue"/>. The range between <paramref name="minValue"/> and <paramref name="maxValue"/> must be less than or equal to <see cref="double.MaxValue"/>.</param>
        /// <returns>A double-precision floating point random number.</returns>
        public override double NextDouble(double minValue, double maxValue)
        {
            return minValue + random.NextDouble() * (maxValue - minValue);
        }

        /// <summary>
        /// Fills the elements of a specified array of bytes with random numbers.
        /// Each element of the array of bytes is set to a random number ∊[0, <see cref="byte.MaxValue"/>].
        /// </summary>
        /// <param name="buffer">An array of bytes to contain random numbers.</param>
        public override void NextBytes(byte[] buffer)
        {
            random.NextBytes(buffer);
        }

        /// <summary>
        /// A next random 32-bit unsigned integer ∊[<see cref="uint.MinValue"/>, <see cref="uint.MaxValue"/>].
        /// </summary>
        /// <returns>A next random 32-bit unsigned integer.</returns>
        public override uint NextUInt()
        {
            return (uint)(random.NextDouble() * uint.MaxValue);
        }
    }
}
