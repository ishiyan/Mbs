using Mbs.Trading.Data;

namespace Mbs.Trading.Indicators.Abstractions
{
    /// <summary>
    /// Various extensions.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Creates a short string.
        /// </summary>
        /// <param name="component">A component to convert.</param>
        /// <returns>A short string.</returns>
        public static string ToShortString(this OhlcvComponent component)
        {
            switch (component)
            {
                case OhlcvComponent.OpeningPrice: return "o";
                case OhlcvComponent.HighestPrice: return "h";
                case OhlcvComponent.LowestPrice: return "l";
                case OhlcvComponent.ClosingPrice: return "c";
                case OhlcvComponent.TypicalPrice: return "t";
                case OhlcvComponent.MedianPrice: return "m";
                case OhlcvComponent.WeightedPrice: return "w";
                case OhlcvComponent.Volume: return "v";
                default: return "?";
            }
        }
    }
}
