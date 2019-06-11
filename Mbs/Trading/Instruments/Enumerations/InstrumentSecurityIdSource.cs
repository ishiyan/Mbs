namespace Mbs.Trading.Instruments
{
    /// <summary>
    /// Identifies a source of the security id value.
    /// <para/>
    /// </summary>
    /// <remarks>
    /// <para>
    /// Modelled after FIX SecurityIDSource field.
    /// </para>
    /// <para>
    /// See http://fiximate.fixtrading.org/latestEP/
    /// </para>
    /// </remarks>
    public enum InstrumentSecurityIdSource
    {
        /// <summary>
        /// ISIN.
        /// </summary>
        Isin,

        /// <summary>
        /// CUISP.
        /// </summary>
        Cuisp,

        /// <summary>
        /// SEDOL.
        /// </summary>
        Sedol,

        /// <summary>
        /// RIC code.
        /// </summary>
        RicCode,

        /// <summary>
        /// Bloomberg symbol.
        /// </summary>
        BloombergSymbol,

        /// <summary>
        /// Bloomberg Open Symbology BBGID.
        /// </summary>
        BloombergOpenSymbologyBbgid,

        /// <summary>
        /// Exchange symbol.
        /// </summary>
        ExchangeSymbol,

        /// <summary>
        /// ISO currency code.
        /// </summary>
        IsoCurrencyCode
    }
}
