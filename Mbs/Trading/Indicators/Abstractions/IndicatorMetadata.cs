namespace Mbs.Trading.Indicators.Abstractions
{
    /// <summary>
    /// Describes output metadata of an indicator.
    /// </summary>
    public class IndicatorMetadata
    {
        /// <summary>
        /// Gets or sets the identification of an indicator.
        /// </summary>
        public IndicatorType IndicatorType { get; set; }

        /// <summary>
        /// Gets or sets a metadata object per indicator output.
        /// </summary>
        public Metadata[] Outputs { get; set; }
    }
}
