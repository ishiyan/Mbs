namespace Mbs.Trading.Orders.Enumerations
{
    /// <summary>
    /// The states an order runs through during its lifetime.
    /// </summary>
    /// <remarks>
    /// See http://fiximate.fixtrading.org (<c>OrdStatus</c> field).
    /// </remarks>
    public enum OrderStatus
    {
        /// <summary>
        /// The order has been received by the broker and is being evaluated.
        /// </summary>
        /// <remarks>
        /// The order will proceed to the <see cref="PendingNew"/> status.
        /// </remarks>
        Accepted,

        /// <summary>
        /// The order has been accepted by the broker but not yet acknowledged for execution.
        /// </summary>
        /// <remarks>
        /// The order will proceed to the either <see cref="New"/> or the <see cref="Rejected"/> status.
        /// </remarks>
        PendingNew,

        /// <summary>
        /// The order has been acknowledged by the broker and becomes the outstanding order with no executions.
        /// </summary>
        /// <remarks>
        /// The order can proceed to the <see cref="Filled"/>, the <see cref="PartiallyFilled"/>, the <see cref="Expired"/>,
        /// the <see cref="PendingCancel"/>, the <see cref="PendingReplace"/>, or to the <see cref="Rejected"/> status.
        /// </remarks>
        New,

        /// <summary>
        /// The order has been rejected by the broker. No executions were done.
        /// </summary>
        /// <remarks>
        /// This is a terminal state of an order, no further changes are allowed.
        /// </remarks>
        Rejected,

        /// <summary>
        /// The order has been partially filled and has remaining quantity.
        /// </summary>
        /// <remarks>
        /// The order can proceed to the <see cref="Filled"/>, the <see cref="PendingCancel"/>, or to the <see cref="PendingReplace"/> status.
        /// </remarks>
        PartiallyFilled,

        /// <summary>
        /// The order has been completely filled.
        /// </summary>
        /// <remarks>
        /// This is a terminal state of an order, no further changes are allowed.
        /// </remarks>
        Filled,

        /// <summary>
        /// The order (with or without executions) has been canceled in broker’s system due to time in force instructions.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The only exceptions are <see cref="OrderTimeInForce.FillOrKill"/> and <see cref="OrderTimeInForce.ImmediateOrCancel"/>
        /// orders that have <see cref="Canceled"/> as terminal order state.
        /// </para>
        /// <para>
        /// This is a terminal state of an order, no further changes are allowed.
        /// </para>
        /// </remarks>
        Expired,

        /// <summary>
        /// A replace request has been sent to the broker, but the broker hasn't replaced the order yet.
        /// </summary>
        /// <remarks>
        /// The order will proceed back to the previous status.
        /// </remarks>
        PendingReplace,

        /// <summary>
        /// A cancel request has been sent to the broker, but the broker hasn't canceled the order yet.
        /// </summary>
        /// <remarks>
        /// The order will proceed to the either <see cref="Canceled"/> or back to the previous status.
        /// </remarks>
        PendingCancel,

        /// <summary>
        /// The order (with or without executions) has been canceled by the broker.
        /// <para/>
        /// </summary>
        /// <remarks>
        /// The order may still be partially filled.
        /// This is a terminal state of an order, no further changes are allowed.
        /// </remarks>
        Canceled,
    }
}
