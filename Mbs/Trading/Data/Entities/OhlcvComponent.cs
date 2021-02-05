namespace Mbs.Trading.Data.Entities
{
    /// <summary>
    /// An ohlcv component.
    /// </summary>
    public enum OhlcvComponent
    {
        /// <summary>
        /// The closing price.
        /// </summary>
        ClosingPrice = 0,

        /// <summary>
        /// The opening price.
        /// </summary>
        OpeningPrice,

        /// <summary>
        /// The highest price.
        /// </summary>
        HighestPrice,

        /// <summary>
        /// The lowest price.
        /// </summary>
        LowestPrice,

        /// <summary>
        /// The median price, calculated as (low + high)/2.
        /// </summary>
        MedianPrice,

        /// <summary>
        /// The typical price, calculated as (low + high + close)/3.
        /// </summary>
        TypicalPrice,

        /// <summary>
        /// The weighted price, calculated as (low + high + open + close)/4.
        /// </summary>
        WeightedPrice,

        /// <summary>
        /// The volume.
        /// </summary>
        Volume,
    }
}
