namespace Mbs.Trading.Indicators.Abstractions
{
    /// <summary>
    /// An input data to create an indicator.
    /// </summary>
    public class IndicatorInput
    {
        /// <summary>
        /// Gets or sets the identification of an indicator.
        /// </summary>
        public IndicatorType IndicatorType { get; set; }

        /// <summary>
        /// Gets or sets parameters to create an indicator.
        /// </summary>
        public object Parameters { get; set; }

        /// <summary>
        /// Gets or sets outputs of an indicator.
        /// </summary>
        public int[] OutputKinds { get; set; }
    }
}
