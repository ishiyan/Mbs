namespace Mbs.Trading.Data.Generators.Chirp
{
    /// <summary>
    /// Enumerates chirp sweep shapes.
    /// </summary>
    public enum ChirpSweep
    {
        /// <summary>
        /// Linear period chirp sweep.
        /// </summary>
        LinearPeriod = 0,

        /// <summary>
        /// Linear frequency chirp sweep.
        /// </summary>
        LinearFrequency,

        /// <summary>
        /// Quadratic period chirp sweep.
        /// </summary>
        QuadraticPeriod,

        /// <summary>
        /// Quadratic frequency chirp sweep.
        /// </summary>
        QuadraticFrequency,

        /// <summary>
        /// Logarithmic period chirp sweep.
        /// </summary>
        LogarithmicPeriod,

        /// <summary>
        /// Logarithmic frequency chirp sweep.
        /// </summary>
        LogarithmicFrequency
    }
}
