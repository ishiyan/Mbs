namespace Mbs.Trading.Indicators.Abstractions
{
    /// <summary>
    /// Describes output metadata of an indicator.
    /// </summary>
    public class IndicatorMetadata
    {
        /// <summary>
        /// Identifies the indicator.
        /// </summary>
        public IndicatorType IndicatorType { get; set; }

        /// <summary>
        /// A metadata object per indicator output.
        /// </summary>
        public Metadata[] Outputs { get; set; }
    }
}
