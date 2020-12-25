namespace Mbs.Numerics.RandomGenerators
{
    /// <summary>
    /// A common interface for all random number generators.
    /// </summary>
    public interface IRandomGenerator
    {
        /// <summary>
        /// Gets a value indicating whether the random number generator can be reset, so that it produces the same  random number sequence again.
        /// </summary>
        bool CanReset { get; }

        /// <summary>
        /// A 32-bit random signed integer ∊[0, <see cref="int.MaxValue"/>).
        /// </summary>
        /// <returns>A 32-bit random signed integer.</returns>
        int NextInt();

        /// <summary>
        /// A 32-bit random signed integer ∊[0, <paramref name="maxValue"/>).
        /// </summary>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. The <paramref name="maxValue"/> must be greater than or equal to zero.</param>
        /// <returns>A 32-bit random signed integer.</returns>
        int NextInt(int maxValue);

        /// <summary>
        /// A 32-bit random signed integer ∊[<paramref name="minValue"/>, <paramref name="maxValue"/>).
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number to be generated.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. The <paramref name="maxValue"/> must be greater than or equal to <paramref name="minValue"/>.</param>
        /// <returns>A 32-bit random signed integer.</returns>
        int NextInt(int minValue, int maxValue);

        /// <summary>
        /// A double-precision floating point random number ∊[0.0, 1.0).
        /// </summary>
        /// <returns>A double-precision floating point random number.</returns>
        double NextDouble();

        /// <summary>
        /// A double-precision floating point random number ∊[0.0, <paramref name="maxValue"/>).
        /// </summary>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. The <paramref name="maxValue"/> must be greater than or equal to 0.0.</param>
        /// <returns>A double-precision floating point random number.</returns>
        double NextDouble(double maxValue);

        /// <summary>
        /// A double-precision floating point random number ∊[<paramref name="minValue"/>, <paramref name="maxValue"/>).
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number to be generated. The range between <paramref name="minValue"/> and <paramref name="maxValue"/> must be less than or equal to <see cref="double.MaxValue"/>.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. The <paramref name="maxValue"/> must be greater than or equal to <paramref name="minValue"/>. The range between <paramref name="minValue"/> and <paramref name="maxValue"/> must be less than or equal to <see cref="double.MaxValue"/>.</param>
        /// <returns>A double-precision floating point random number.</returns>
        double NextDouble(double minValue, double maxValue);

        /// <summary>
        /// Returns a random Boolean value.
        /// </summary>
        /// <returns>A <see cref="bool"/> value.</returns>
        bool NextBoolean();

        /// <summary>
        /// Fills the elements of a specified array of bytes with random numbers.
        /// Each element of the array of bytes is set to a random number ∊[0, <see cref="byte.MaxValue"/>].
        /// </summary>
        /// <param name="buffer">An array of bytes to contain random numbers.</param>
        void NextBytes(byte[] buffer);

        /// <summary>
        /// Resets the random number generator, so that it produces the same random number sequence again.
        /// </summary>
        void Reset();

        /// <summary>
        /// A next random 32-bit unsigned integer ∊[<see cref="uint.MinValue"/>, <see cref="uint.MaxValue"/>].
        /// </summary>
        /// <returns>A next random 32-bit unsigned integer.</returns>
        uint NextUInt();
    }
}
