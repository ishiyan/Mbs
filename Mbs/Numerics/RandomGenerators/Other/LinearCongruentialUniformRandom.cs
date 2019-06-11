using System;
using System.Runtime.CompilerServices;

namespace Mbs.Numerics.Random
{
    /// <summary>
    /// A linear congruential random generator is defined by the recurrence relation
    /// <para />
    /// xᵢ₊₁ ≡ (axᵢ + c) mod m.
    /// <para />
    /// where m > 0 is a modulus, m > a > 0 is a multiplier, m > c > 0 is an increment.
    /// <para />
    /// This implementation uses default values
    /// <para />
    /// a = 1103515245, c = 12345, m = 2³²,
    /// <para />
    /// which are used by GNU C/C++, Watcom C, Digital Mars, CodeWarrior, and IBM VisualAge C/C++.
    /// <para />
    /// Other popular pairs:
    /// <para />
    /// a = 134775813, c = 1, m = 2³², Borland Delphi
    /// <para />
    /// a = 22695477, c = 2531011, m = 2³², Borland C/C++
    /// <para />
    /// a = 214013, c = 1, m = 2³², Microsoft Visual/Quick C/C++
    /// <para />
    /// a = 16807, c = 0, m = 2³², Apple CarbonLib
    /// <para />
    /// a = 65539, c = 0, m = 2³¹, infamous IBM RANDU.
    /// </summary>
    public sealed class LinearCongruentialUniformRandom : RandomGenerator
    {
        /// <summary>
        /// The value of multiplier.
        /// </summary>
        private const uint A = 1103515245U;

        /// <summary>
        /// The value of increment.
        /// </summary>
        private const uint C = 12345U;

        private readonly uint seed;
        private uint x;

        /// <summary>
        /// Initializes a new instance of the <see cref="LinearCongruentialUniformRandom"/> class, using the current system tick count as a seed value.
        /// </summary>
        public LinearCongruentialUniformRandom()
            : this(Environment.TickCount)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LinearCongruentialUniformRandom"/> class, using the specified seed value.
        /// </summary>
        /// <param name="seed">A seed value of the pseudo-random number sequence. If a negative number is specified, the absolute value of the number is used.</param>
        public LinearCongruentialUniformRandom(int seed)
        {
            this.seed = (uint)Math.Abs(seed);
            Init();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Init()
        {
            x = seed;

            // Reset helper variables used for generation of random bools.
            BitBuffer = 0;
            BitCount = 0;
        }

        /// <summary>
        /// Resets the <see cref="LinearCongruentialUniformRandom"/>, so that it produces the same pseudo-random number sequence again.
        /// </summary>
        public override void Reset()
        {
            Init();
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="LinearCongruentialUniformRandom"/> can be reset, so that it produces the same pseudo-random number sequence again.
        /// </summary>
        public override bool CanReset => true;

        /// <summary>
        /// A next random 32-bit unsigned integer ∊[<see cref="uint.MinValue"/>, <see cref="uint.MaxValue"/>].
        /// </summary>
        /// <returns>A next random 32-bit unsigned integer.</returns>
        public override uint NextUInt()
        {
            x = x * A + C;
            return x;
        }
    }
}
