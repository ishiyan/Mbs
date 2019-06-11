namespace Mbs.Trading.Portfolios
{
    /// <summary>
    /// An algorithm used to group order executions into round trips.
    /// </summary>
    public enum RoundtripGrouping
    {
        /// <summary>
        /// A round trip is defined by (1) an order execution that establishes or increases a position
        /// and (2) an offsetting execution that reduces the position size.
        /// </summary>
        FillToFill,

        /// <summary>
        /// A round trip is defined by a sequence of order executions, from a flat position to a non-zero
        /// position which may increase or decrease in quantity, and back to a flat position.
        /// </summary>
        FlatToFlat,

        /// <summary>
        /// A round trip is defined by a sequence of order executions, from a flat position to a non-zero
        /// position and an offsetting execution that reduces the position size.
        /// </summary>
        FlatToReduced
    }
}
