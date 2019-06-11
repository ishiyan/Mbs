using System;
using System.Runtime.CompilerServices;

namespace Mbs.Numerics.Random
{
    /// <summary>
    /// An additive lagged Fibonacci uniform pseudo-random number generator.
    /// <para />
    /// Uses the modulus 2³² and by default the lags 418 and 1279, which can be adjusted through the
    /// associated <see cref="ShortLag"/> and <see cref="LongLag"/> properties. Some popular pairs are presented on
    /// <a href="http://en.wikipedia.org/wiki/Lagged_Fibonacci_generator">Wikipedia - Lagged Fibonacci generator</a>.
    /// </summary>
    public sealed class AdditiveLaggedFibonacciUniformRandom : RandomGenerator
    {
        /// <summary>
        /// Gets the short lag of the lagged Fibonacci pseudo-random number generator.
        /// </summary>
        public int ShortLag { get; private set; } = 418;

        /// <summary>
        /// Gets the long lag of the lagged Fibonacci pseudo-random number generator.
        /// </summary>
        public int LongLag { get; private set; } = 1279;

        /// <summary>
        /// Stores the used seed value.
        /// </summary>
        private readonly uint seedValue;

        /// <summary>
        /// An array of <see cref="LongLag"/> random numbers.
        /// </summary>
        private uint[] array;

        /// <summary>
        /// An index for the random number array element that will be accessed next.
        /// </summary>
        private int arrayNextIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdditiveLaggedFibonacciUniformRandom"/> class, using the current system tick count as a seed value.
        /// </summary>
        public AdditiveLaggedFibonacciUniformRandom()
            : this(Environment.TickCount)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdditiveLaggedFibonacciUniformRandom"/> class, using the specified seed value.
        /// </summary>
        /// <param name="seed">A number used to calculate a starting value for the pseudo-random number sequence. If a negative number is specified, the absolute value of the number is used.</param>
        public AdditiveLaggedFibonacciUniformRandom(int seed)
        {
            seedValue = (uint)Math.Abs(seed);
            Init();
        }

        /// <summary>
        /// Sets the lag values. The smaller numbers have short periods.
        /// <para />
        /// It is required that at least one of the values chosen to initialise the generator be odd.
        /// <para />
        /// It has been suggested that good ratios between the values are approximately the golden ratio.
        /// <para />
        /// The popular pairs are:
        /// <para />
        /// (7,10), (5,17), (24,55), (65,71), (128,159)
        /// <para />
        /// (6,31), (31,63), (97,127), (353,521), (168,521), (334,607), (273,607), (418,1279), (1252,2281), (2098,4423), (5502,9689), (9842,19937), (13470,23209), (21034,44497)
        /// <para />
        /// (24,55), (38,89), (37,100), (30,127), (83,258), (107,378), (273,607), (1029,2281), (576,3217), (4187,9689), (7083,19937), (9739,23209).
        /// </summary>
        /// <param name="shortLagValue">The short lag value, must be positive.</param>
        /// <param name="longLagValue">The long lag value, must be grater than the <paramref name="shortLagValue"/>.</param>
        /// <returns>True if values are valid and were assigned, otherwise false.</returns>
        public bool SetLags(int shortLagValue, int longLagValue)
        {
            if (shortLagValue > 0 && longLagValue > shortLagValue)
            {
                ShortLag = shortLagValue;
                LongLag = longLagValue;
                Init();
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Init()
        {
            var mt = new MersenneTwister19937UniformRandom((int)seedValue);
            array = new uint[LongLag];
            for (uint j = 0; j < LongLag; ++j)
                array[j] = mt.NextUInt();
            arrayNextIndex = LongLag;

            // Reset helper variables used for generation of random bools.
            BitBuffer = 0;
            BitCount = 0;
        }

        /// <summary>
        /// Resets the <see cref="AdditiveLaggedFibonacciUniformRandom"/>, so that it produces the same pseudo-random number sequence again.
        /// </summary>
        public override void Reset()
        {
            Init();
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="AdditiveLaggedFibonacciUniformRandom"/> can be reset, so that it produces the same pseudo-random number sequence again.
        /// </summary>
        public override bool CanReset => true;

        /// <summary>
        /// A next random 32-bit unsigned integer ∊[<see cref="uint.MinValue"/>, <see cref="uint.MaxValue"/>].
        /// </summary>
        /// <returns>A next random 32-bit unsigned integer.</returns>
        public override uint NextUInt()
        {
            if (arrayNextIndex >= LongLag)
            {
                // Two loops to avoid costly modulo operations.
                int i = LongLag - ShortLag;
                for (int j = 0; j < ShortLag; ++j)
                {
                    // array[j] = array[j] + array[j + i];
                    double d = array[j] + array[j + i];
                    if (d >= 1d)
                        --d;
                    array[j] = (uint)d;
                }

                for (int j = ShortLag; j < LongLag; ++j)
                {
                    // array[j] = array[j] + array[j - shortLag];
                    double d = array[j] + array[j - ShortLag];
                    if (d >= 1d)
                        --d;
                    array[j] = (uint)d;
                }

                arrayNextIndex = 0;
            }

            return array[arrayNextIndex++];
        }
    }
}
