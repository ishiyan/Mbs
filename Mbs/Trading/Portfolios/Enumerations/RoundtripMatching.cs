namespace Mbs.Trading.Portfolios.Enumerations
{
    /// <summary>
    /// An algorithm used to match the offsetting order executions.
    /// </summary>
    public enum RoundtripMatching
    {
        /// <summary>
        /// Offsetting order executions will be matched in FIFO order.
        /// </summary>
        FirstInFirstOut,

        /// <summary>
        /// Offsetting order executions will be matched in LIFO order.
        /// </summary>
        LastInFirstOut,
    }
}
