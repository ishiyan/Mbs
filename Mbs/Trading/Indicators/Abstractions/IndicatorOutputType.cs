namespace Mbs.Trading.Indicators.Abstractions
{
    /// <summary>
    /// An indicator output type.
    /// </summary>
    public enum IndicatorOutputType
    {
        /// <summary>
        /// Holds a time stamp and a value.
        /// </summary>
        Scalar,

        /// <summary>
        /// Holds a time stamp and two values representing upper and lower lines of a band.
        /// </summary>
        Band,

        /// <summary>
        /// Holds a time stamp and an array of values representing a heat-map column.
        /// </summary>
        HeatMap,
    }
}
