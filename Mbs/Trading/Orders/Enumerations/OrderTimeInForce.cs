namespace Mbs.Trading.Orders.Enumerations
{
    /// <summary>
    /// The time an order is to be traded.
    /// </summary>
    /// <remarks>
    /// See http://fiximate.fixtrading.org (<c>TimeInForce</c> field),
    /// https://www.interactivebrokers.com/en/software/tws/timeinforcetop.htm.
    /// </remarks>
    public enum OrderTimeInForce
    {
        /// <summary>
        /// An order to be executed within the trading day on which it was entered.
        /// </summary>
        Day,

        /// <summary>
        /// An order to be executed immediatly in whole or partially.
        /// </summary>
        /// <remarks>
        /// Any portion not so executed is to be canceled. Not to be confused with <see cref="FillOrKill"/>.
        /// </remarks>
        ImmediateOrCancel,

        /// <summary>
        /// An order to be executed immediatly in its entirety.
        /// </summary>
        /// <remarks>
        /// If not so executed, the order is to be canceled. Not to be confused with <see cref="ImmediateOrCancel"/>.
        /// </remarks>
        FillOrKill,

        /// <summary>
        /// An order that remains in effect until it is either executed or canceled.
        /// </summary>
        /// <remarks>
        /// Typically, GTC orders will be automatically be cancelled if a corporate action on a security
        /// results in a stock split (forward or reverse), exchange for shares, or distribution of shares.
        /// </remarks>
        GoodTillCanceled,

        /// <summary>
        /// An order that, if not executed, expires at the the specified date.
        /// </summary>
        GoodTillDate,

        /// <summary>
        /// An order to be executed at the opening or not at all.
        /// </summary>
        /// <remarks>
        /// All or part of any order not executed at the opening is treated as canceled.
        /// </remarks>
        AtOpen,

        /// <summary>
        /// An order to be executed at the closing or not at all.
        /// </summary>
        /// <remarks>
        /// <para>
        /// All or part of any order not executed at the closing is treated as canceled.
        /// </para>
        /// <para>
        /// Indicated price is to be around the closing price, however, not held to the closing price.
        /// </para>
        /// </remarks>
        AtClose,
    }
}
