using System;
using Mbs.Trading.Currencies;
using Mbs.Trading.Instruments;
using Mbs.Trading.Orders;

namespace Mbs.Trading.Portfolios
{
    /// <summary>
    /// An immutable portfolio position execution.
    /// </summary>
    public class PortfolioExecution
    {
        /// <summary>
        /// Gets the single order ticket.
        /// </summary>
        public ISingleOrderTicket SingleOrderTicket { get; }

        /// <summary>
        /// Gets the single order report.
        /// </summary>
        public SingleOrderReport SingleOrderReport { get; }

        /// <summary>
        /// Gets the execution instrument.
        /// </summary>
        public Instrument Instrument { get; }

        /// <summary>
        /// Gets the execution date and time.
        /// </summary>
        public DateTime DateTime { get; }

        /// <summary>
        /// Gets the execution commission in instrument's currency.
        /// </summary>
        public double Commission { get; }

        /// <summary>
        /// Gets the execution price in instrument's currency.
        /// </summary>
        public double Price { get; }

        /// <summary>
        /// Gets the execution quantity.
        /// </summary>
        public double Quantity { get; }

        /// <summary>
        /// Gets the Profit and Loss of this execution in instrument's currency.
        /// </summary>
        public double ProfitAndLoss { get; internal set; }

        /// <summary>
        /// Gets the realized Profit and Loss of this execution in instrument's currency.
        /// </summary>
        public double RealizedProfitAndLoss { get; internal set; }

        /// <summary>
        /// Gets the execution order side.
        /// </summary>
        public OrderSide Side { get; }

        /// <summary>
        /// Gets the intsrument's currency.
        /// </summary>
        public CurrencyCode Currency { get; }

        /// <summary>
        /// Gets the execution comment.
        /// </summary>
        public string Comment { get; }

        /// <summary>
        /// Gets the execution amount (positive or negative, depending on the execution side).
        /// </summary>
        public double Amount { get; }

        /// <summary>
        /// Gets the execution debt in instrument's currency (value minus margin).
        /// </summary>
        public double Debt { get; }

        /// <summary>
        /// Gets the (non-negative) execution value in instrument's currency (factored price times quantity).
        /// </summary>
        public double Value { get; }

        /// <summary>
        /// Gets the execution margin in instrument's currency (instrument margin times quantity).
        /// </summary>
        public double Margin { get; }

        /// <summary>
        /// Gets the execution net cash flow in instrument's currency (factored price times negative amount).
        /// </summary>
        public double NetCashFlow { get; }

        /// <summary>
        /// Gets the execution cash flow in instrument's currency (net cash flow minus commission).
        /// </summary>
        public double CashFlow { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PortfolioExecution"/> class.
        /// </summary>
        /// <param name="singleOrderTicket">The associated single order ticket.</param>
        /// <param name="singleOrderReport">The associated single order execution report.</param>
        internal PortfolioExecution(ISingleOrderTicket singleOrderTicket, SingleOrderReport singleOrderReport)
        {
            OrderReportType type = singleOrderReport.ReportType;
            if (type != OrderReportType.Filled && type != OrderReportType.PartiallyFilled)
                throw new ArgumentException($"Expected Filled or PartiallyFilled order report type, got {type}.");
            SingleOrderReport = singleOrderReport;
            SingleOrderTicket = singleOrderTicket;
            Instrument = singleOrderTicket.Order.Instrument;
            Currency = Instrument.Currency;
            DateTime = singleOrderReport.TransactionTime;
            Price = singleOrderReport.LastPrice;
            Quantity = singleOrderReport.LastQuantity;
            Comment = singleOrderReport.Text;
            Side = singleOrderTicket.Order.Side;
            switch (Side)
            {
                case OrderSide.Buy:
                case OrderSide.BuyMinus:
                    Amount = Quantity;
                    break;
                case OrderSide.Sell:
                case OrderSide.SellPlus:
                case OrderSide.SellShort:
                case OrderSide.SellShortExempt:
                    Amount = -Quantity;
                    break;
                default:
                    throw new ArgumentException($"Not supported order side: {Side}.");
            }

            // TODO: Is instrument.Margin an absolute value or a percentage?
            Margin = Instrument.Margin * Quantity;
            Value = Price * Quantity;
            NetCashFlow = -Amount * Price;

            double? factor = Instrument.Factor;
            if (factor.HasValue)
            {
                double temp = factor.Value;
                Value *= temp;
                NetCashFlow *= temp;
            }

            Debt = Math.Abs(Margin) < double.Epsilon ? 0 : Value - Margin;

            Commission = singleOrderReport.LastCommission;
            if (Currency != singleOrderReport.CommissionCurrency && Commission > 0)
            {
                var converter = singleOrderTicket.Order.Account.CurrencyConverter;
                if (converter != null)
                    Commission = converter.Convert(Commission, singleOrderReport.CommissionCurrency, Currency);
            }

            CashFlow = NetCashFlow - Commission;
        }
    }
}
