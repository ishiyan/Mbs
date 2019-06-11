namespace Mbs.Trading.Data
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
        /// The median price, calculated as (low + hight)/2.
        /// </summary>
        MedianPrice,

        /// <summary>
        /// The typical price, calculated as (low + hight + close)/3.
        /// </summary>
        TypicalPrice,

        /// <summary>
        /// The weighted price, calculated as (low + hight + open + close)/4.
        /// </summary>
        WeightedPrice,

        /// <summary>
        /// The volume.
        /// </summary>
        Volume
    }
}
