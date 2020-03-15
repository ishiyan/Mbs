namespace Mbs.Trading.Indicators.Abstractions
{
    /// <summary>
    /// Contains information about a single indicator output.
    /// </summary>
    public class Metadata
    {
        /// <summary>
        /// Identifies a single indicator output.
        /// </summary>
        public int Kind { get; set; }

        /// <summary>
        /// A type of the output.
        /// </summary>
        public IndicatorOutputType Type { get; set; }

        /// <summary>
        /// Identifies an instance of the output.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Describes the output.
        /// </summary>
        public string Description { get; set; }
    }
}
