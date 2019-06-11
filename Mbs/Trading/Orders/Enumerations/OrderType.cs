namespace Mbs.Trading.Orders
{
    /// <summary>
    /// Identifies an order type.
    /// </summary>
    /// <remarks>
    /// See http://fiximate.fixtrading.org (<c>OrdType</c> field),
    /// https://www.interactivebrokers.com/en/software/tws/ordertypestop.htm.
    /// </remarks>
    public enum OrderType
    {
        /// <summary>
        /// A market order.
        /// </summary>
        /// <remarks>
        /// <para>
        /// A market order is an order to buy (sell) an asset at the ask (bid) price currently available in the marketplace.
        /// </para>
        /// <para>
        /// This order may increase the likelihood of a fill and the speed of execution, but provides no price protection
        /// and may fill at a price far lower (higher) than the current bid (ask).
        /// </para>
        /// </remarks>
        Market,

        /// <summary>
        /// A market-if-touched order.
        /// </summary>
        /// <remarks>
        /// <para>
        /// A market-if-touched is an order to buy (sell) a stated amount of an asset as soon as the market goes below (above)
        /// a preset price, at which point it becomes a <see cref="Market"/> order.
        /// </para>
        /// <para>
        /// This order is similar to a <see cref="Stop"/> order, except that a <see cref="MarketIfTouched"/> sell order is placed
        /// above the current market price, and a <see cref="Stop"/> sell order is placed below.
        /// </para>
        /// </remarks>
        MarketIfTouched,

        /// <summary>
        /// A limit order.
        /// </summary>
        /// <remarks>
        /// <para>
        /// A limit order is an order to buy (sell) only at a specified limit price or better, above (below) the limit price.
        /// </para>
        /// <para>
        /// A limit order may not get filled if the price never reaches the specified limit price.
        /// </para>
        /// </remarks>
        Limit,

        /// <summary>
        /// A stop order.
        /// </summary>
        /// <remarks>
        /// <para>
        /// A buy (sell) stop order becomes a <see cref="Market"/> order when the last traded price is
        /// greater (less) -than-or-equal to the stop price.
        /// </para>
        /// <para>
        /// A buy (sell) stop price is always above (below) the current market price.
        /// </para>
        /// <para>
        /// A stop order may not get filled if the price never reaches the specified stop price.
        /// </para>
        /// <para>
        /// </para>
        /// </remarks>
        Stop,

        /// <summary>
        /// A stop-limit order.
        /// </summary>
        /// <remarks>
        /// <para>
        /// A buy (sell) stop-Limit order becomes a <see cref="Limit"/> order when the last traded price
        /// is greater (less) -than-or-equal to the stop price.
        /// </para>
        /// <para>
        /// A buy (sell) stop price is always above (below) the current market price.
        /// </para>
        /// <para>
        /// A stop-limit order may not get filled if the price never reaches the specified stop price.
        /// </para>
        /// </remarks>
        StopLimit,

        /// <summary>
        /// A trailing-stop order.
        /// </summary>
        /// <remarks>
        /// <para>
        /// A trailing-buy (sell) stop order is an order entered with a stop price at a fixed amount above (below)
        /// the market price that creates a moving or trailing activation price, hence the name.
        /// </para>
        /// <para>
        /// If the market price falls (rises), the stop loss price rises (falls) by the increased amount,
        /// but if the stock price falls, the stop loss price remains the same.
        /// </para>
        /// <para>
        /// The reverse is true for a buy trailing stop order.
        /// </para>
        /// </remarks>
        TrailingStop,

        /// <summary>
        /// A market on close order.
        /// </summary>
        /// <remarks>
        /// A market-on-close order will execute as a <see cref="Market"/> order as close to the closing price as possible.
        /// </remarks>
        MarketOnClose,

        /// <summary>
        /// A market-to-limit order.
        /// </summary>
        /// <remarks>
        /// <para>
        /// A market-to-limit order is a <see cref="Market"/> order to execute at the current best price.
        /// </para>
        /// <para>
        /// If the entire order does not immediately execute at the market price, the remainder of the order is re-submitted as a
        /// <see cref="Limit"/> order with the limit price set to the price at which the market order portion of the order executed.
        /// </para>
        /// </remarks>
        MarketToLimit,

        /// <summary>
        /// A limit-if-touched order.
        /// </summary>
        /// <remarks>
        /// <para>
        /// A limit-if-touched order is designed to buy (or sell) a contract below (or above) the market, at the limit price or better.
        /// </para>
        /// </remarks>
        LimitIfTouched,

        /// <summary>
        /// A limit-on-close order.
        /// </summary>
        /// <remarks>
        /// This order will fill at the closing price if that price is at or better than the submitted limit price.
        /// Otherwise, the order will be canceled.
        /// </remarks>
        LimitOnClose
    }
}
