using System;

namespace Mbs.Trading.Portfolios.Enumerations
{
    /// <summary>
    /// The portfolio position sell price monitor mode.
    /// </summary>
    [Flags]
    public enum PortfolioMonitors
    {
        /// <summary>
        /// Do not monitor.
        /// </summary>
        None = 0,

        /// <summary>
        /// Monitor on quote.
        /// </summary>
        Quote = 1,

        /// <summary>
        /// Monitor on trade.
        /// </summary>
        Trade = 2,

        /// <summary>
        /// Monitor on ohlcv.
        /// </summary>
        Ohlcv = 4,
    }
}