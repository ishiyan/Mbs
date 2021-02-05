using Mbs.Trading.Data;
using Mbs.Trading.Data.Entities;

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
            return component switch
            {
                OhlcvComponent.OpeningPrice => "o",
                OhlcvComponent.HighestPrice => "h",
                OhlcvComponent.LowestPrice => "l",
                OhlcvComponent.ClosingPrice => "c",
                OhlcvComponent.TypicalPrice => "t",
                OhlcvComponent.MedianPrice => "m",
                OhlcvComponent.WeightedPrice => "w",
                OhlcvComponent.Volume => "v",
                _ => "?"
            };
        }
    }
}
