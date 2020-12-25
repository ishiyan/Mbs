namespace Mbs.Numerics.RandomGenerators.FractionalBrownianMotion
{
    /// <summary>
    /// Enumerates fractional Brownian motion algorithms.
    /// </summary>
    public enum FractionalBrownianMotionAlgorithm
    {
        /// <summary>
        /// Generates a fractional Brownian motion or a fractional Gaussian noise using the Hosking method.
        /// </summary>
        Hosking = 0,

        /// <summary>
        /// Generates a fractional Brownian motion or a fractional Gaussian noise using the approximate Paxson method.
        /// </summary>
        Paxson,

        /// <summary>
        /// Generates a fractional Brownian motion or a fractional Gaussian noise using the circulant method.
        /// </summary>
        Circulant,

        /// <summary>
        /// Generates a fractional Brownian motion or a fractional Gaussian noise using the approximate circulant method.
        /// </summary>
        ApproximateCirculant,
    }
}
