namespace Mbs.Numerics.Random
{
    /// <summary>
    /// A common interface for all Gaussian random number generators.
    /// </summary>
    public interface INormalRandomGenerator
    {
        /// <summary>
        /// A double-precision floating point Gaussian random number.
        /// </summary>
        /// <returns>The next double-precision floating point Gaussian random number.</returns>
        double NextDouble();

        /// <summary>
        /// A double-precision floating point Gaussian random number given the distribution mean and the standard deviation.
        /// </summary>
        /// <param name="mean">Distribution mean.</param>
        /// <param name="stdDev">Distribution standard deviation.</param>
        /// <returns>The next double-precision floating point Gaussian random number.</returns>
        double NextDouble(double mean, double stdDev);

        /// <summary>
        /// Take a sample from the standard gaussian distribution, i.e. with mean of 0 and standard deviation of 1.
        /// </summary>
        /// <returns>The next double-precision floating point Gaussian random number.</returns>
        double NextDoubleStandard();

        /// <summary>
        /// Gets a value indicating whether the random number generator can be reset, so that it produces the same random number sequence again.
        /// </summary>
        bool CanReset { get; }

        /// <summary>
        /// Resets the random number generator, so that it produces the same random number sequence again.
        /// </summary>
        void Reset();
    }
}
