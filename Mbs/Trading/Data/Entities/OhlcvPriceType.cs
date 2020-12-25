// ReSharper disable once CheckNamespace
namespace Mbs.Trading.Data
{
    /// <summary>
    /// An ohlcv price type.
    /// </summary>
    public enum OhlcvPriceType
    {
        /// <summary>
        /// The closing price.
        /// </summary>
        Closing = 0,

        /// <summary>
        /// The opening price.
        /// </summary>
        Opening,

        /// <summary>
        /// The highest price.
        /// </summary>
        Highest,

        /// <summary>
        /// The lowest price.
        /// </summary>
        Lowest,

        /// <summary>
        /// The median price, calculated as (low + hight)/2.
        /// </summary>
        Median,

        /// <summary>
        /// The typical price, calculated as (low + hight + close)/3.
        /// </summary>
        Typical,

        /// <summary>
        /// The weighted price, calculated as (low + hight + open + close)/4.
        /// </summary>
        Weighted,
    }
}
