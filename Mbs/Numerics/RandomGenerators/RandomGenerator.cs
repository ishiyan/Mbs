namespace Mbs.Numerics.Random
{
    /// <summary>
    /// A common functionality for all random number generators.
    /// </summary>
    public abstract class RandomGenerator : IRandomGenerator
    {
        /// <summary>
        /// Gets a value indicating whether the random number generator can be reset, so that it produces the same random number sequence again.
        /// </summary>
        public virtual bool CanReset => false;

        /// <summary>
        /// Gets or sets an <see cref="uint"/> used to generate up to 32 random <see cref="bool"/> values.
        /// </summary>
        protected uint BitBuffer { get; set; }

        /// <summary>
        /// Gets or sets specifies how many random <see cref="bool"/> values still can be generated from <see cref="BitBuffer"/>.
        /// </summary>
        protected int BitCount { get; set; }

        /// <summary>
        /// Represents the multiplier that computes a double-precision floating point number greater than or equal to 0.0
        /// and less than 1.0 when it gets applied to a nonnegative 32-bit signed integer.
        /// </summary>
        private const double IntToDoubleMultiplier = 1d / (int.MaxValue + 1d);

        /// <summary>
        /// Represents the multiplier that computes a double-precision floating point number greater than or equal to 0.0
        /// and less than 1.0  when it gets applied to a 32-bit unsigned integer.
        /// </summary>
        private const double UIntToDoubleMultiplier = 1d / (uint.MaxValue + 1d);

        /// <summary>
        /// A 32-bit random signed integer ∊[0, <see cref="int.MaxValue"/>).
        /// </summary>
        /// <returns>A 32-bit random signed integer.</returns>
        public virtual int NextInt()
        {
            var result = (int)(NextUInt() >> 1);

            // Exclude int.MaxValue from the range of return values.
            if (result == int.MaxValue)
                return NextInt();
            return result;
        }

        /// <summary>
        /// A 32-bit random signed integer ∊[0, <paramref name="maxValue"/>).
        /// </summary>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. The <paramref name="maxValue"/> must be greater than or equal to zero.</param>
        /// <returns>A 32-bit random signed integer.</returns>
        public virtual int NextInt(int maxValue)
        {
            // The shift operation and extra int cast before the first multiplication give better performance.
            // See comment in NextDouble().
            return (int)((int)(NextUInt() >> 1) * IntToDoubleMultiplier * maxValue);
        }

        /// <summary>
        /// A 32-bit random signed integer ∊[<paramref name="minValue"/>, <paramref name="maxValue"/>).
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number to be generated.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. The <paramref name="maxValue"/> must be greater than or equal to <paramref name="minValue"/>.</param>
        /// <returns>A 32-bit random signed integer.</returns>
        public virtual int NextInt(int minValue, int maxValue)
        {
            int range = maxValue - minValue;
            if (range < 0)
            {
                // The range is greater than Int32.MaxValue, so we have to use slower floating point arithmetic.
                // Also all 32 random bits (uint) have to be used which again is slower (See comment in NextDouble()).
                return minValue + (int)(NextUInt() * UIntToDoubleMultiplier * ((double)maxValue - minValue));
            }

            // 31 random bits (int) will suffice which allows us to shift and cast to an int before the first multiplication and gain better performance.
            // See comment in NextDouble().
            return minValue + (int)((int)(NextUInt() >> 1) * IntToDoubleMultiplier * range);
        }

        /// <summary>
        /// A double-precision floating point random number ∊[0.0, 1.0).
        /// </summary>
        /// <returns>A double-precision floating point random number.</returns>
        public virtual double NextDouble()
        {
            // Here a ~2x speed improvement is gained by computing a value that can be cast to an int
            // before casting to a double to perform the multiplication.
            // Casting a double from an int is a lot faster than from an uint and the extra shift operation
            // and cast to an int are very fast (the allocated bits remain the same), so overall there's
            // a significant performance improvement.
            return (int)(NextUInt() >> 1) * IntToDoubleMultiplier;
        }

        /// <summary>
        /// A double-precision floating point random number ∊[0.0, <paramref name="maxValue"/>).
        /// </summary>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. The <paramref name="maxValue"/> must be greater than or equal to 0.0.</param>
        /// <returns>A double-precision floating point random number.</returns>
        public virtual double NextDouble(double maxValue)
        {
            // The shift operation and extra int cast before the first multiplication give better performance.
            // See comment in NextDouble().
            return (int)(NextUInt() >> 1) * IntToDoubleMultiplier * maxValue;
        }

        /// <summary>
        /// A double-precision floating point random number ∊[<paramref name="minValue"/>, <paramref name="maxValue"/>).
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number to be generated. The range between <paramref name="minValue"/> and <paramref name="maxValue"/> must be less than or equal to <see cref="double.MaxValue"/>.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. The <paramref name="maxValue"/> must be greater than or equal to <paramref name="minValue"/>. The range between <paramref name="minValue"/> and <paramref name="maxValue"/> must be less than or equal to <see cref="double.MaxValue"/>.</param>
        /// <returns>A double-precision floating point random number.</returns>
        public virtual double NextDouble(double minValue, double maxValue)
        {
            double range = maxValue - minValue;

            // The shift operation and extra int cast before the first multiplication give better performance.
            // See comment in NextDouble().
            return minValue + (int)(NextUInt() >> 1) * IntToDoubleMultiplier * range;
        }

        /// <summary>
        /// Returns a random Boolean value.
        /// </summary>
        /// <returns>A <see cref="bool"/> value.</returns>
        public bool NextBoolean()
        {
            // Buffers 32 random bits for future calls, so the random number generator
            // is only invoked once in every 32 calls.
            if (BitCount == 32)
            {
                // Generate 32 more bits (1 uint) and store it for future calls.
                BitBuffer = NextUInt();

                // Reset the BitCount and use rightmost bit of buffer to generate random bool.
                BitCount = 1;
                return (BitBuffer & 0x1) == 1;
            }

            // Increase the BitCount and use rightmost bit of shifted buffer to generate random bool.
            ++BitCount;
            return ((BitBuffer >>= 1) & 0x1) == 1;
        }

        /// <summary>
        /// Fills the elements of a specified array of bytes with random numbers.
        /// Each element of the array of bytes is set to a random number ∊[0, <see cref="byte.MaxValue"/>].
        /// </summary>
        /// <param name="buffer">An array of bytes to contain random numbers.</param>
        public virtual void NextBytes(byte[] buffer)
        {
            // Fill the buffer with 4 bytes (1 uint) at a time.
            int i = 0, length = buffer.Length;
            uint u;
            while (i < length - 3)
            {
                u = NextUInt();
                buffer[i] = (byte)u;
                buffer[++i] = (byte)(u >> 8);
                buffer[++i] = (byte)(u >> 16);
                buffer[++i] = (byte)(u >> 24);
                ++i;
            }

            // Fill up any remaining bytes in the buffer.
            if (i < length)
            {
                u = NextUInt();
                buffer[i] = (byte)u;
                if (++i < length)
                {
                    buffer[i] = (byte)(u >> 8);
                    if (++i < length)
                    {
                        buffer[i] = (byte)(u >> 16);
                        if (++i < length)
                        {
                            buffer[i] = (byte)(u >> 24);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Resets the random number generator, so that it produces the same random number sequence again.
        /// </summary>
        public virtual void Reset()
        {
        }

        /// <summary>
        /// A next random 32-bit unsigned integer ∊[<see cref="uint.MinValue"/>, <see cref="uint.MaxValue"/>].
        /// </summary>
        /// <returns>A next random 32-bit unsigned integer.</returns>
        public abstract uint NextUInt();
    }
}
