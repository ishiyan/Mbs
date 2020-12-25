// ReSharper disable once CheckNamespace
namespace Mbs.Trading.Orders
{
    /// <summary>
    /// Identifies an order side.
    /// </summary>
    /// <remarks>
    /// See http://fiximate.fixtrading.org (<c>Side</c> field),
    /// https://www.interactivebrokers.com/en/software/tws/ordertypestop.htm.
    /// </remarks>
    public enum OrderSide
    {
        /// <summary>
        /// Refers to the buying of a security.
        /// </summary>
        Buy,

        /// <summary>
        /// Refers to the selling of a security.
        /// </summary>
        Sell,

        /// <summary>
        /// A buy order provided that the price must drop below the previous market price.
        /// </summary>
        /// <remarks>
        /// <para>
        /// A round-lot order to buy “minus” is an order to buy a stated amount of a stock provided that its price is not higher
        /// than the last sale if the last sale was a “minus” or “zero minus” tick and not higher than the last sale minus the
        /// minimum fractional change in the stock if the last sale was a “plus” or “zero plus” tick.
        /// </para>
        /// <para>
        /// A limit price order to buy “minus” also states the highest price at which it can be executed.
        /// </para>
        /// <para>
        /// “Zero minus tick” is a trade executed on an exchange at the same price as the preceding trade,
        /// but at a lower price than the last trade of a different price.
        /// </para>
        /// <para>
        /// For example, if a succession of trades occur in the following order - $10.25, $10.00, and $10.00 -
        /// the last trade would be considered a “zero minus tick” or “zero downtick” trade.
        /// </para>
        /// <para>
        /// “Zero plus tick” is a trade that is executed at the same price as the preceding trade
        /// but at a higher price than the last trade of a different price.
        /// </para>
        /// <para>
        /// For example, if a succession of trades occurs at $10, $10.25 and $10.25 again,
        /// the latter trade would be considered a “zero plus tick”, or “zero uptick”, trade.
        /// </para>
        /// <para>
        /// For more than 70 years there was an “uptick rule” as established by the U.S. Securities
        /// and Exchange Commission (SEC); the rule stated that stocks could be shorted only on an uptick
        /// or a zero plus tick, not on a downtick or a zero minus tick. This rule was lifted in 2007.
        /// </para>
        /// </remarks>
        BuyMinus,

        /// <summary>
        /// A sell order provided that the price must rise above the previous market price.
        /// </summary>
        /// <remarks>
        /// <para>
        /// A round-lot order to sell “plus” is an order to sell a stated amount of a stock provided that
        /// its price is not lower than the last sale if the last sale was a “plus” or “zero plus” tick
        /// and not lower than the last sale minus the minimum fractional change in the stock if the last
        /// sale was a “minus” or “zero minus” tick.
        /// </para>
        /// <para>
        /// A limit-price order to sell “plus” also states the lowest price at which it can be executed.
        /// </para>
        /// <para>
        /// “Zero plus tick” is a trade that is executed at the same price as the preceding trade
        /// but at a higher price than the last trade of a different price.
        /// </para>
        /// <para>
        /// For example, if a succession of trades occurs at $10, $10.25 and $10.25 again,
        /// the latter trade would be considered a “zero plus tick”, or “zero uptick”, trade.
        /// </para>
        /// <para>
        /// “Zero minus tick” is a trade executed on an exchange at the same price as the preceding trade,
        /// but at a lower price than the last trade of a different price.
        /// </para>
        /// <para>
        /// For example, if a succession of trades occur in the following order - $10.25, $10.00, and $10.00 -
        /// the last trade would be considered a “zero minus tick” or “zero downtick” trade.
        /// </para>
        /// <para>
        /// For more than 70 years there was an “uptick rule” as established by the U.S. Securities
        /// and Exchange Commission (SEC); the rule stated that stocks could be shorted only on an uptick
        /// or a zero plus tick, not on a downtick or a zero minus tick. This rule was lifted in 2007.
        /// </para>
        /// </remarks>
        SellPlus,

        /// <summary>
        /// An order to sell a security that the seller does not own.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Can only be executed on a “plus” or “zero plus” tick.
        /// </para>
        /// <para>
        /// “Zero plus tick” is a trade that is executed at the same price as the preceding trade
        /// but at a higher price than the last trade of a different price.
        /// </para>
        /// <para>
        /// For example, if a succession of trades occurs at $10, $10.25 and $10.25 again,
        /// the latter trade would be considered a “zero plus tick”, or “zero uptick”, trade.
        /// </para>
        /// <para>
        /// “Zero minus tick” is a trade executed on an exchange at the same price as the preceding trade,
        /// but at a lower price than the last trade of a different price.
        /// </para>
        /// <para>
        /// For example, if a succession of trades occur in the following order - $10.25, $10.00, and $10.00 -
        /// the last trade would be considered a “zero minus tick” or “zero downtick” trade.
        /// </para>
        /// <para>
        /// For more than 70 years there was an “uptick rule” as established by the U.S. Securities
        /// and Exchange Commission (SEC); the rule stated that stocks could be shorted only on an uptick
        /// or a zero plus tick, not on a downtick or a zero minus tick. This rule was lifted in 2007.
        /// </para>
        /// </remarks>
        SellShort,

        /// <summary>
        /// Short sale exempt from short-sale rules.
        /// </summary>
        /// <remarks>
        /// <para>
        /// A special trading situation where a short sale is allowed on a minustick. The owners of
        /// a convertible trading at parity can sell the equivalent amount of common short on a minus tick,
        /// assuming they have the firm intention to convert.
        /// </para>
        /// <para>
        /// “Zero plus tick” is a trade that is executed at the same price as the preceding trade
        /// but at a higher price than the last trade of a different price.
        /// </para>
        /// <para>
        /// For example, if a succession of trades occurs at $10, $10.25 and $10.25 again,
        /// the latter trade would be considered a “zero plus tick”, or “zero uptick”, trade.
        /// </para>
        /// <para>
        /// “Zero minus tick” is a trade executed on an exchange at the same price as the preceding trade,
        /// but at a lower price than the last trade of a different price.
        /// </para>
        /// <para>
        /// For example, if a succession of trades occur in the following order - $10.25, $10.00, and $10.00 -
        /// the last trade would be considered a “zero minus tick” or “zero downtick” trade.
        /// </para>
        /// <para>
        /// For more than 70 years there was an “uptick rule” as established by the U.S. Securities
        /// and Exchange Commission (SEC); the rule stated that stocks could be shorted only on an uptick
        /// or a zero plus tick, not on a downtick or a zero minus tick. This rule was lifted in 2007.
        /// </para>
        /// </remarks>
        SellShortExempt,
    }
}
