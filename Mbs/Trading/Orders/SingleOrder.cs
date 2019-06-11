using System;
using Mbs.Trading.Instruments;
using Mbs.Trading.Portfolios;

namespace Mbs.Trading.Orders
{
    /// <summary>
    /// A single order.
    /// </summary>
    public class SingleOrder
    {
        /// <summary>
        /// Gets or sets the associated portfolio.
        /// </summary>
        public Portfolio Portfolio { get; set; }

        /// <summary>
        /// Gets or sets the associated account.
        /// </summary>
        public Account Account { get; set; } // TODO: account is already in portfolio

        /// <summary>
        /// Gets or sets the free format text string.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the order type.
        /// </summary>
        public OrderType Type { get; set; } = OrderType.Market;

        /// <summary>
        /// Gets or sets the order side.
        /// </summary>
        public OrderSide Side { get; set; } = OrderSide.Buy;

        /// <summary>
        /// Gets or sets the order Time In Force validity.
        /// </summary>
        public OrderTimeInForce TimeInForce { get; set; } = OrderTimeInForce.Day;

        /// <summary>
        /// Gets or sets the instrument.
        /// </summary>
        public Instrument Instrument { get; set; }

        /// <summary>
        /// Gets or sets the total order quantity to execute. <c>NaN</c> if not set.
        /// </summary>
        public double Quantity { get; set; } = double.NaN;

        /// <summary>
        /// Gets or sets minimum quantity of an order to be executed. <c>NaN</c> if not set.
        /// </summary>
        public double MinimumQuantity { get; set; } = double.NaN;

        /// <summary>
        /// Gets or sets the limit price per unit of quantity (e.g. per share). <c>NaN</c> if not set.
        /// Required for limit order types. For FX orders, should be the "all-in" rate (spot rate adjusted for forward points). Can be used to specify a limit price for a pegged order, previously indicated, etc.
        /// </summary>
        public double LimitPrice { get; set; } = double.NaN;

        /// <summary>
        /// Gets or sets the stop price per unit of quantity (e.g. per share). <c>NaN</c> if not set. Required for the <c>stop</c> and <c>stop limit</c> order types.
        /// </summary>
        public double StopPrice { get; set; } = double.NaN;

        /// <summary>
        /// Gets or sets the trailing distance. <c>NaN</c> if not set.
        /// </summary>
        public double TrailingDistance { get; set; } = double.NaN;

        /// <summary>
        /// Gets or sets the date and time this order request was created by the trader, trading system, or intermediary.
        /// </summary>
        public DateTime CreationTime { get; set; } = new DateTime(0L);

        /// <summary>
        /// Gets or sets the order expiration date/time for the orders with the <c>GTD</c> <c>TimeInForce</c> value.
        /// </summary>
        public DateTime ExpirationTime { get; set; } = new DateTime(0L);
    }
}
