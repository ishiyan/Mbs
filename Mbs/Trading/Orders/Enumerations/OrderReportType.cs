namespace Mbs.Trading.Orders.Enumerations
{
    /// <summary>
    /// Identifies an order report event.
    /// </summary>
    public enum OrderReportType
    {
        /// <summary>
        /// An <c>OrderStatus.PendingNew</c> report.
        /// </summary>
        PendingNew,

        /// <summary>
        /// An <c>OrderStatus.New</c> report.
        /// </summary>
        New,

        /// <summary>
        /// An <c>OrderStatus.Rejected</c> report.
        /// </summary>
        Rejected,

        /// <summary>
        /// An <c>OrderStatus.PartiallyFilled</c> report.
        /// </summary>
        PartiallyFilled,

        /// <summary>
        /// An <c>OrderStatus.Filled</c> report.
        /// </summary>
        Filled,

        /// <summary>
        /// An <c>OrderStatus.Expired</c> report.
        /// </summary>
        Expired,

        /// <summary>
        /// An <c>OrderStatus.PendingReplace</c> report.
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
        /// An <c>OrderStatus.PendingCancel</c> report.
        /// </summary>
        PendingCancel,

        /// <summary>
        /// An <c>OrderStatus.Canceled</c> report.
        /// </summary>
        Canceled,

        /// <summary>
        /// An order cancel rejected report.
        /// </summary>
        CancelRejected,

        /// <summary>
        /// An order status report.
        /// </summary>
        OrderStatus,
    }
}
