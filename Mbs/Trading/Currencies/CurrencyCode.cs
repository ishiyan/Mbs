namespace Mbs.Trading.Currencies
{
    /// <summary>
    /// Currency representations according to ISO 4217.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This international standard specifies the structure for a three-letter alphabetic code for the representation of currencies.
    /// </para>
    /// <para>
    /// See http://en.wikipedia.org/wiki/ISO_4217.
    /// </para>
    /// </remarks>
    public enum CurrencyCode
    {
        /// <summary>No currency. Used to denote a transaction involving no currency.</summary>
        Xxx = 0,

        /// <summary>Argentine peso.</summary>
        Ars,

        /// <summary>Australian dollar.</summary>
        Aud,

        /// <summary>Brazilian real.</summary>
        Brl,

        /// <summary>Canadian dollar.</summary>
        Cad,

        /// <summary>Swiss franc.</summary>
        Chf,

        /// <summary>Chilean peso.</summary>
        Clp,

        /// <summary>Yuan renminbi.</summary>
        Cny,

        /// <summary>Czech koruna.</summary>
        Czk,

        /// <summary>Danish krone.</summary>
        Dkk,

        /// <summary>Euro.</summary>
        Eur,

        /// <summary>Pound sterling.</summary>
        Gbp,

        /// <summary>Penny sterling, ​1⁄100 of a <see cref="Gbp"/>.</summary>
        Gbx,

        /// <summary>Hong Kong dollar.</summary>
        Hkd,

        /// <summary>Hungary forint.</summary>
        Huf,

        /// <summary>Israeli new sheqel.</summary>
        Ils,

        /// <summary>Indian rupee.</summary>
        Inr,

        /// <summary>Icelandic krona.</summary>
        Isk,

        /// <summary>Japanese yen.</summary>
        Jpy,

        /// <summary>South Korean won.</summary>
        Krw,

        /// <summary>Mexican peso.</summary>
        Mxn,

        /// <summary>Norwegian krone.</summary>
        Nok,

        /// <summary>New Zeeland dollar.</summary>
        Nzd,

        /// <summary>Poland zloty.</summary>
        Pln,

        /// <summary>Romanian new leu.</summary>
        Ron,

        /// <summary>Russian rouble.</summary>
        Rub,

        /// <summary>Swedish krona.</summary>
        Sek,

        /// <summary>Singapore dollar.</summary>
        Sgd,

        /// <summary>Turkish new lira.</summary>
        Try,

        /// <summary>Taiwan new dollar.</summary>
        Twd,

        /// <summary>US dollar.</summary>
        Usd,

        /// <summary>Silver (one troy ounce).</summary>
        Xag,

        /// <summary>Gold (one troy ounce).</summary>
        Xau,

        /// <summary>Palladium (one troy ounce).</summary>
        Xpd,

        /// <summary>Platinum (one troy ounce).</summary>
        Xpt,

        /// <summary>Code reserved for testing purposes.</summary>
        Xts,

        /// <summary>South African rand.</summary>
        Zar,

        /// <summary>Bitcoin (cryptocurrency).</summary>
        Xbt,

        /// <summary>Bitcoin Cash (cryptocurrency).</summary>
        Xbc,

        /// <summary>Stellar Lumen (cryptocurrency).</summary>
        Xlm,

        /// <summary>Monero (cryptocurrency).</summary>
        Xmr,

        /// <summary>Ripple (cryptocurrency).</summary>
        Xrp,

        /// <summary>Dash (cryptocurrency).</summary>
        Dsh,

        /// <summary>Ether (cryptocurrency).</summary>
        Eth,

        /// <summary>Vertcoin (cryptocurrency).</summary>
        Vtc,

        /// <summary>Zcash (cryptocurrency).</summary>
        Zec,
    }
}
