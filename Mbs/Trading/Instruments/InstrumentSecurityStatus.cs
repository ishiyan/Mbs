namespace Mbs.Trading.Instruments
{
    /// <summary>
    /// Denotes the current state of the instrument.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Modeled after FIX SecurityStatus field.
    /// See http://fiximate.fixtrading.org/latestEP/.
    /// </para>
    /// </remarks>
    public enum InstrumentSecurityStatus
    {
        /// <summary>
        /// Instrument is active, i.e. trading is possible.
        /// </summary>
        Active,

        /// <summary>
        /// Instrument is active but only orders closing positions are allowed.
        /// </summary>
        ActiveClosingOrdersOnly,

        /// <summary>
        /// Instrument has previously been active and is now no longer traded but has not expired yet.
        /// </summary>
        /// <remarks>
        /// The instrument may become active again.
        /// </remarks>
        Inactive,

        /// <summary>
        /// Instrument has been temporarily disabled for trading.
        /// </summary>
        Suspended,

        /// <summary>
        /// Instrument is currently still active but will expire after the current business day.
        /// </summary>
        /// <remarks>
        /// For example, a contract that expires intraday (e.g. at noon time) and is no longer traded
        /// but will still show up in the current day's order book with related statistics.
        /// </remarks>
        PendingExpiry,

        /// <summary>
        /// Instrument has expired.
        /// </summary>
        /// <remarks>
        /// An instrument may expire due to reaching maturity or expired based on contract definitions
        /// or exchange rules.
        /// </remarks>
        Expired,

        /// <summary>
        /// Instrument is awaiting deletion from security reference data.
        /// </summary>
        PendingDeletion,

        /// <summary>
        /// <para/>
        /// Instrument has been removed from securities reference data.
        /// <para/>
        /// <para/>
        /// </summary>
        /// <remarks>
        /// <para>
        /// A delisted instrument would not trade on the exchange but it may still be traded
        /// over-the-counter.
        /// </para>
        /// <para>
        /// Delisting rules varY from exchange to exchange, which may include non-compliance of
        /// capitalization, revenue, consecutive minimum closing price.
        /// </para>
        /// <para>
        /// The instrument may become listed again once the instrument is back in compliance.
        /// </para>
        /// </remarks>
        Delisted,

        /// <summary>
        /// Instrument has breached a predefined price threshold.
        /// </summary>
        KnockedOut,

        /// <summary>
        /// Instrument reinstated, i.e. threshold has not been breached.
        /// </summary>
        KnockOutRevoked,
    }
}
