namespace Mbs.Trading.Indicators.Abstractions
{
    /// <summary>
    /// Contains information about a single indicator output.
    /// </summary>
    public class Metadata
    {
        /// <summary>
        /// Gets or sets the identification of a single indicator output.
        /// </summary>
        public int Kind { get; set; }

        /// <summary>
        /// Gets or sets the type of an output.
        /// </summary>
        public IndicatorOutputType Type { get; set; }

        /// <summary>
        /// Gets or sets the identification of an instance of an output.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of an output.
        /// </summary>
        public string Description { get; set; }
    }
}
