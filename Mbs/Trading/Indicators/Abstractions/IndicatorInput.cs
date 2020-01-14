namespace Mbs.Trading.Indicators.Abstractions
{
    /// <summary>
    /// An input data to create an indicator.
    /// </summary>
    public class IndicatorInput
    {
        /// <summary>
        /// Identifies the indicator.
        /// </summary>
        public IndicatorType IndicatorType;

        /// <summary>
        /// Parameters to create the indicator.
        /// </summary>
        public object Parameters;

        /// <summary>
        /// Outputs of the indicator.
        /// </summary>
        public int[] OutputKinds;
    }
}
