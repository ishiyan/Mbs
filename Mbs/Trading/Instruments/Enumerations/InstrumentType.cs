namespace Mbs.Trading.Instruments
{
    /// <summary>
    /// Identifies instrument types.
    /// </summary>
    public enum InstrumentType
    {
        /// <summary>
        /// An undefined instrument type.
        /// </summary>
        Undefined,

        /// <summary>
        /// An index.
        /// </summary>
        Index,

        /// <summary>
        /// A stock.
        /// </summary>
        Stock,

        /// <summary>
        /// An exchange-traded fund.
        /// </summary>
        Etf,

        /// <summary>
        /// An exchange-traded commodity.
        /// </summary>
        Etv,

        /// <summary>
        /// An intraday indicative net asset value of an <see cref="Etf"/> or <see cref="Etv"/> based on the market values of its underlying constituents.
        /// </summary>
        Inav,

        /// <summary>
        /// A fund.
        /// </summary>
        Fund,

        /// <summary>
        /// An option.
        /// </summary>
        Option,

        /// <summary>
        /// A future.
        /// </summary>
        Future,

        /// <summary>
        /// A contract for difference.
        /// </summary>
        Cfd,

        /// <summary>
        /// A Forex instrument.
        /// </summary>
        Forex,

        /// <summary>
        /// A crypto currency.
        /// </summary>
        Crypto
    }
}
