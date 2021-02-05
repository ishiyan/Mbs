using System;
using Mbs.Trading.Data.Entities;

namespace Mbs.Trading.Data.Live
{
    /// <summary>
    /// Temporal entity types.
    /// </summary>
    internal static class Types
    {
        /// <summary>
        /// The ohlcv type.
        /// </summary>
        internal static readonly Type OhlcvType = typeof(Ohlcv);

        /// <summary>
        /// The trade type.
        /// </summary>
        internal static readonly Type TradeType = typeof(Trade);

        /// <summary>
        /// The quote type.
        /// </summary>
        internal static readonly Type QuoteType = typeof(Quote);

        /// <summary>
        /// The scalar type.
        /// </summary>
        internal static readonly Type ScalarType = typeof(Scalar);
    }
}
