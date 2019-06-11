namespace Mbs.Api
{
    /// <summary>
    /// Defines common constants.
    /// </summary>
    internal static class Constants
    {
        /// <summary>
        /// Specifies the application/json content type.
        /// </summary>
        public const string ApplicationJsonContentType = "application/json;charset=UTF-8";

        /// <summary>
        /// A first version of an api.
        /// </summary>
        public const string ApiVersionOne = "1";

        /// <summary>
        /// A version of the api segment.
        /// </summary>
        public const string ApiVersionOneSegment = "api/v" + ApiVersionOne + "/";

        /// <summary>
        /// An ohlcv segment.
        /// </summary>
        public const string OhlcvSegment = "ohlcvs";

        /// <summary>
        /// A quote segment.
        /// </summary>
        public const string QuoteSegment = "quotes";

        /// <summary>
        /// A trade segment.
        /// </summary>
        public const string TradeSegment = "trades";

        /// <summary>
        /// A scalar segment.
        /// </summary>
        public const string ScalarSegment = "scalars";

        /// <summary>
        /// A data generation group name.
        /// </summary>
        public const string DataGenerationGroupName = "Data: generation";

        /// <summary>
        /// A data generation segment.
        /// </summary>
        public const string DataGenerationSegment = "data/generators/";

        /// <summary>
        /// An online historical data group name.
        /// </summary>
        public const string OnlineHistoricalDataGroupName = "Data: historical online";

        /// <summary>
        /// An online historical data segment.
        /// </summary>
        public const string OnlineHistoricalDataSegment = "data/historical/online/";

        /// <summary>
        /// An offline historical data group name.
        /// </summary>
        public const string OfflineHistoricalDataGroupName = "Data: historical offline";

        /// <summary>
        /// An offline historical data segment.
        /// </summary>
        public const string OfflineHistoricalDataSegment = "data/historical/offline/";

        /// <summary>
        /// An instrument lists group name.
        /// </summary>
        public const string InstrumentListsGroupName = "Instruments: lists";

        /// <summary>
        /// An instrument lists segment.
        /// </summary>
        public const string InstrumentListsSegment = "instruments/lists/";
    }
}
