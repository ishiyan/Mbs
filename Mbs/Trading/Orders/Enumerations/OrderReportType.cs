namespace Mbs.Trading.Orders
{
#pragma warning disable 1584,1711,1572,1581,1580
    /// <summary>
    /// Identifies an order report event.
    /// </summary>
    public enum OrderReportType
    {
        /// <summary>
        /// An <see cref="OrderStatus.PendingNew"/> report.
        /// </summary>
        PendingNew,

        /// <summary>
        /// An <see cref="OrderStatus.New"/> report.
        /// </summary>
        New,

        /// <summary>
        /// An <see cref="OrderStatus.Rejected"/> report.
        /// </summary>
        Rejected,

        /// <summary>
        /// An <see cref="OrderStatus.PartiallyFilled"/> report.
        /// </summary>
        PartiallyFilled,

        /// <summary>
        /// An <see cref="OrderStatus.Filled"/> report.
        /// </summary>
        Filled,

        /// <summary>
        /// An <see cref="OrderStatus.Expired"/> report.
        /// </summary>
        Expired,

        /// <summary>
        /// An <see cref="OrderStatus.PendingReplace"/> report.
        /// </summary>
        PendingReplace,

        /// <summary>
        /// An order replaced report.
        /// </summary>
        Replaced,

        /// <summary>
        /// An order replace rejected report.
        /// </summary>
        ReplaceRejected,

        /// <summary>
        /// An <see cref="OrderStatus.PendingCancel"/> report.
        /// </summary>
        PendingCancel,

        /// <summary>
        /// An <see cref="OrderStatus.Canceled"/> report.
        /// </summary>
        Canceled,

        /// <summary>
        /// An order cancel rejected report.
        /// </summary>
        CancelRejected,

        /// <summary>
        /// An order status report.
        /// </summary>
        OrderStatus
    }
#pragma warning restore 1584,1711,1572,1581,1580
}
