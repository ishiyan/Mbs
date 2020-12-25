using System;
using Mbs.Trading.Currencies;
using Mbs.Trading.Orders.Enumerations;

namespace Mbs.Trading.Orders
{
    /// <summary>
    /// An immutable single order report.
    /// </summary>
    public class SingleOrderReport
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SingleOrderReport"/> class.
        /// </summary>
        /// <param name="transactionTime">The transaction date and time.</param>
        /// <param name="reportId">The report id.</param>
        /// <param name="reportType">The report type.</param>
        /// <param name="status">The status.</param>
        /// <param name="text">The accompanying text.</param>
        internal SingleOrderReport(
            DateTime transactionTime,
            string reportId,
            OrderReportType reportType,
            OrderStatus status,
            string text)
        {
            TransactionTime = transactionTime;
            ReportId = reportId;
            ReportType = reportType;
            Status = status;
            Text = text;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleOrderReport"/> class.
        /// </summary>
        /// <param name="transactionTime">The transaction date and time.</param>
        /// <param name="reportId">The report id.</param>
        /// <param name="reportType">The report type.</param>
        /// <param name="status">The status.</param>
        /// <param name="text">The accompanying text.</param>
        /// <param name="replaceSourceOrder">The replace source order.</param>
        /// <param name="replaceTargetOrder">The replace target order.</param>
        internal SingleOrderReport(
            DateTime transactionTime,
            string reportId,
            OrderReportType reportType,
            OrderStatus status,
            string text,
            SingleOrder replaceSourceOrder,
            SingleOrder replaceTargetOrder)
            : this(transactionTime, reportId, reportType, status, text)
        {
            ReplaceSourceOrder = replaceSourceOrder;
            ReplaceTargetOrder = replaceTargetOrder;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleOrderReport"/> class.
        /// </summary>
        /// <param name="transactionTime">The transaction date and time.</param>
        /// <param name="reportId">The report id.</param>
        /// <param name="reportType">The report type.</param>
        /// <param name="status">The status.</param>
        /// <param name="text">The accompanying text.</param>
        /// <param name="lastPrice">The price of this fill.</param>
        /// <param name="lastQuantity">The quantity bought/sold on the last fill.</param>
        /// <param name="leavesQuantity">The quantity open for further execution.</param>
        /// <param name="cumulativeQuantity">The total quantity filled.</param>
        /// <param name="averagePrice">The average price.</param>
        /// <param name="lastCommission">The commission of this fill.</param>
        /// <param name="cumulativeCommission">The total commission.</param>
        /// <param name="commissionCurrency">The commission currency.</param>
        internal SingleOrderReport(
            DateTime transactionTime,
            string reportId,
            OrderReportType reportType,
            OrderStatus status,
            string text,
            double lastPrice,
            double lastQuantity,
            double leavesQuantity,
            double cumulativeQuantity,
            double averagePrice,
            double lastCommission,
            double cumulativeCommission,
            CurrencyCode commissionCurrency)
            : this(transactionTime, reportId, reportType, status, text)
        {
            LastPrice = lastPrice;
            LastQuantity = lastQuantity;
            LeavesQuantity = leavesQuantity;
            CumulativeQuantity = cumulativeQuantity;
            AveragePrice = averagePrice;
            LastCommission = lastCommission;
            CumulativeCommission = cumulativeCommission;
            CommissionCurrency = commissionCurrency;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleOrderReport"/> class.
        /// </summary>
        /// <param name="transactionTime">The transaction date and time.</param>
        /// <param name="reportId">The report id.</param>
        /// <param name="reportType">The report type.</param>
        /// <param name="status">The status.</param>
        /// <param name="text">The accompanying text.</param>
        /// <param name="lastPrice">The price of this fill.</param>
        /// <param name="lastQuantity">The quantity bought/sold on the last fill.</param>
        /// <param name="leavesQuantity">The quantity open for further execution.</param>
        /// <param name="cumulativeQuantity">The total quantity filled.</param>
        /// <param name="averagePrice">The average price.</param>
        /// <param name="lastCommission">The commission of this fill.</param>
        /// <param name="cumulativeCommission">The total commission.</param>
        /// <param name="commissionCurrency">The commission currency.</param>
        /// <param name="replaceSourceOrder">The replace source order.</param>
        /// <param name="replaceTargetOrder">The replace target order.</param>
        internal SingleOrderReport(
            DateTime transactionTime,
            string reportId,
            OrderReportType reportType,
            OrderStatus status,
            string text,
            double lastPrice,
            double lastQuantity,
            double leavesQuantity,
            double cumulativeQuantity,
            double averagePrice,
            double lastCommission,
            double cumulativeCommission,
            CurrencyCode commissionCurrency,
            SingleOrder replaceSourceOrder,
            SingleOrder replaceTargetOrder)
            : this(
                transactionTime,
                reportId,
                reportType,
                status,
                text,
                lastPrice,
                lastQuantity,
                leavesQuantity,
                cumulativeQuantity,
                averagePrice,
                lastCommission,
                cumulativeCommission,
                commissionCurrency)
        {
            ReplaceSourceOrder = replaceSourceOrder;
            ReplaceTargetOrder = replaceTargetOrder;
        }

        /// <summary>
        /// Gets the date and time when the business represented by this report occurred.
        /// </summary>
        public DateTime TransactionTime { get; }

        /// <summary>
        /// Gets the current state of an order as understood by the broker.
        /// </summary>
        public OrderStatus Status { get; }

        /// <summary>
        /// Gets an action of this report.
        /// </summary>
        public OrderReportType ReportType { get; }

        /// <summary>
        /// Gets the unique identifier of a report as assigned by the sell-side (exchange, ECN, etc.).
        /// </summary>
        public string ReportId { get; }

        /// <summary>
        /// Gets the text that accompany this report.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Gets the replace source order. Filled when <see cref="ReportType"/> is <see cref="OrderReportType.Replaced"/> or <see cref="OrderReportType.ReplaceRejected"/>.
        /// </summary>
        public SingleOrder ReplaceSourceOrder { get; }

        /// <summary>
        /// Gets the replace target order. <see cref="ReportType"/> is <see cref="OrderReportType.Replaced"/> or <see cref="OrderReportType.ReplaceRejected"/>.
        /// </summary>
        public SingleOrder ReplaceTargetOrder { get; }

        /// <summary>
        /// Gets the price of this fill.
        /// </summary>
        public double LastPrice { get; }

        /// <summary>
        /// Gets the quantity bought/sold on the last fill.
        /// </summary>
        public double LastQuantity { get; }

        /// <summary>
        /// Gets the quantity open for further execution.
        /// </summary>
        public double LeavesQuantity { get; }

        /// <summary>
        /// Gets the total quantity filled.
        /// </summary>
        public double CumulativeQuantity { get; }

        /// <summary>
        /// Gets the average price.
        /// </summary>
        public double AveragePrice { get; }

        /// <summary>
        /// Gets the commission of this fill.
        /// </summary>
        public double LastCommission { get; }

        /// <summary>
        /// Gets the total commission.
        /// </summary>
        public double CumulativeCommission { get; }

        /// <summary>
        /// Gets the commission currency.
        /// </summary>
        public CurrencyCode CommissionCurrency { get; } = CurrencyCode.Xxx;
    }
}
