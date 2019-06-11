using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mbs.Trading.Data;
using Mbs.Trading.Data.Historical;
using Mbs.Trading.Instruments;
using Mbs.Trading.Markets;
using Mbs.Trading.Time;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Historical.Providers
{
    [TestClass]
    public class EuronextHistoricalDataTests
    {
        #region Setup helpers
        private static readonly TimeSpan EndofdateClosingTimeSpan = new TimeSpan(18, 31, 42);
        private static readonly DateTime Year1970 = new DateTime(1970, 1, 1);
        private static readonly DateTime Year1999 = new DateTime(1999, 1, 1);

        private static class EuronextHistoricalDataAccessor
        {
            private static readonly IEnumerable<MethodInfo> DeclaredMethods = typeof(EuronextHistoricalData).GetTypeInfo().DeclaredMethods;
            private static readonly IEnumerable<FieldInfo> DeclaredFields = typeof(EuronextHistoricalData).GetTypeInfo().DeclaredFields;

            private static TReturnValue InvokeStatic<TReturnValue>([CallerMemberName] string callerName = null, params object[] parameter)
            {
                return (TReturnValue)DeclaredMethods.Single(info =>
                    info.Name.Equals(callerName, StringComparison.Ordinal)).Invoke(null, parameter);
            }

            private static void InvokeStaticVoid([CallerMemberName] string callerName = null, params object[] parameter)
            {
                DeclaredMethods.Single(info => info.Name.Equals(callerName, StringComparison.Ordinal)).Invoke(null, parameter);
            }

            private static async Task<TReturnValue> InvokeStaticAsync<TReturnValue>([CallerMemberName] string callerName = null, params object[] parameter)
            {
                return await (Task<TReturnValue>)DeclaredMethods.Single(info =>
                    info.Name.Equals(callerName, StringComparison.Ordinal)).Invoke(null, parameter);
            }

            private static TReturnValue StaticField<TReturnValue>(string fieldName)
            {
                return (TReturnValue)DeclaredFields.Single(info =>
                    info.Name.Equals(fieldName, StringComparison.Ordinal)).GetValue(null);
            }

            internal static HttpClient HttpClient()
            {
                return StaticField<HttpClient>("HttpClient");
            }

            internal static HttpClient CreateHttpClient()
            {
                return InvokeStatic<HttpClient>();
            }

            internal static async Task<Stream> GetResponseStreamAsync(HttpResponseMessage response)
            {
                object[] parameters = { response };
                return await InvokeStaticAsync<Stream>(parameter: parameters);
            }

            internal static string CreateUrl(string mic, string isin, InstrumentType instrumentType, bool isAdjusted, DateTime beginDate, DateTime endDate, out string referer)
            {
                object[] parameters = { mic, isin, instrumentType, isAdjusted, beginDate, endDate, null };
                string returnValue = InvokeStatic<string>(parameter: parameters);
                referer = (string)parameters[6];
                return returnValue;
            }

            // ReSharper disable once UnusedMethodReturnValue.Local
            internal static async Task<List<Ohlcv>> FetchAsync(HistoricalDataRequest historicalDataRequest, HttpClient httpClient = null)
            {
                object[] parameters = { historicalDataRequest, httpClient };
                return await InvokeStaticAsync<List<Ohlcv>>(parameter: parameters);
            }

            internal static void Validate(string mic, string isin, InstrumentType instrumentType, TimeGranularity timeGranularity)
            {
                object[] parameters = { mic, isin, instrumentType, timeGranularity };
                InvokeStaticVoid(parameter: parameters);
            }
        }

        private static long UnixMilliseconds(DateTime dateTime)
        {
            return (long)(dateTime - Year1970).TotalMilliseconds;
        }

        private static HistoricalDataRequest CreateSpec(string symbol, bool adjustedDataIfPresent, DateTime date1, DateTime date2,
            InstrumentType type, string isin, ExchangeMic mic)
        {
            var instrument = new Instrument(symbol);
            instrument.SetSecurityIdAs(InstrumentSecurityIdSource.Isin, isin);
            instrument.Exchange = new Exchange(mic);
            instrument.Type = type;

            return new HistoricalDataRequest(instrument, date1, date2, TimeGranularity.Day1, EndofdateClosingTimeSpan, adjustedDataIfPresent);
        }

        private static Stream StreamFromText(string text)
        {
            var memoryStream = new MemoryStream();
            var streamWriter = new StreamWriter(memoryStream);
            streamWriter.Write(text);
            streamWriter.Flush();
            memoryStream.Seek(0, SeekOrigin.Begin);

            return memoryStream;
        }

        private class TestDelegatingHandler : DelegatingHandler
        {
            public TestDelegatingHandler(HttpMessageHandler innerHandler)
                : base(innerHandler)
            {
            }

            public int TimeoutMilliseconds { private get; set; }
            public string Text { private get; set; }
            public HttpStatusCode StatusCode { private get; set; }
            public Action SendAsyncIsCalled { private get; set; }

            protected override Task<HttpResponseMessage> SendAsync( HttpRequestMessage request, CancellationToken cancellationToken)
            {
                if (TimeoutMilliseconds > 0)
                    Thread.Sleep(TimeoutMilliseconds);

                cancellationToken.ThrowIfCancellationRequested();
                SendAsyncIsCalled?.Invoke();

                return Task.FromResult(new HttpResponseMessage
                {
                    Content = new StringContent(Text, Encoding.UTF8, "application/json"),
                    StatusCode = StatusCode,
                    ReasonPhrase = "reason phrase"
                });
            }
        }

        private static async Task<List<Ohlcv>> SimulateFetchTimeoutStatusCode(HistoricalDataRequest historicalDataRequest, string text,
            int timeoutSeconds, HttpStatusCode statusCode, MockLogger mockLogger, int retryingHandlerTimeoutSeconds, int retryingHandlerRetries)
        {
            Log.SetLogger(mockLogger);

            var testDelegatingHandler = new TestDelegatingHandler(new HttpClientHandler { UseDefaultCredentials = true })
            {
                TimeoutMilliseconds = timeoutSeconds * 1000,
                Text = text,
                StatusCode = statusCode
            };

            var retryingHandler = new RetryingHandler(new HttpClientHandler { UseDefaultCredentials = true })
            {
                InnerHandler = testDelegatingHandler,
                LogPrefix = () => EuronextHistoricalData.Prefix,
                TimeoutSeconds = () => retryingHandlerTimeoutSeconds,
                Retries = () => retryingHandlerRetries
            };

            var client = new HttpClient(retryingHandler);
            var result = await EuronextHistoricalData.FetchAsync(historicalDataRequest, client);
            client.Dispose();
            return result;
        }

        private static async Task<List<Ohlcv>> SimulateFetchSendAsyncIsCalled(HistoricalDataRequest historicalDataRequest, string text, bool[] isCalled)
        {
            bool sendAsyncIsCalled = false;
            var testDelegatingHandler = new TestDelegatingHandler(new HttpClientHandler { UseDefaultCredentials = true })
            {
                TimeoutMilliseconds = -1,
                Text = text,
                StatusCode = HttpStatusCode.OK,
                SendAsyncIsCalled = () => { sendAsyncIsCalled = true; }
            };

            var retryingHandler = new RetryingHandler(new HttpClientHandler { UseDefaultCredentials = true })
            {
                InnerHandler = testDelegatingHandler,
                LogPrefix = () => EuronextHistoricalData.Prefix,
                TimeoutSeconds = () => 1,
                Retries = () => 0
            };

            var client = new HttpClient(retryingHandler);
            var result = await EuronextHistoricalData.FetchAsync(historicalDataRequest, client);
            client.Dispose();
            isCalled[0] = sendAsyncIsCalled;
            return result;
        }

        private static List<Ohlcv> SimulateParse(string json, string isin, TimeSpan? endofdayClosingTime, out int errorCount, out string lastErrorText)
        {
            var logger = new MockLogger();
            Log.SetLogger(logger);

            List<Ohlcv> list = EuronextHistoricalData.Parse(json, isin, endofdayClosingTime);

            logger.Errors(out errorCount, out lastErrorText);
            return list;
        }
        #endregion

        // ReSharper disable InconsistentNaming

        // [Ignore]
        [DataTestMethod]
        [DataRow(ExchangeMic.Xlon, "GB0000566504", "BLT", InstrumentType.Stock, 1426)]
        [DataRow(ExchangeMic.Tnlb, "ES0113211835", "BBV", InstrumentType.Stock, 427)]
        [DataRow(ExchangeMic.Xpar, "FR0010533075", "GET", InstrumentType.Stock, 2828)]
        public async Task EuronextHistoricalData_LiveTest(ExchangeMic mic, string isin, string symbol, InstrumentType instrumentType, int count)
        {
            HistoricalDataRequest spec = CreateSpec(symbol, true, DateTime.MinValue, DateTime.MaxValue, instrumentType, isin, mic);

            List<Ohlcv> list = await EuronextHistoricalData.FetchAsync(spec);
            EuronextHistoricalData.ClearDataCache();

            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count >= count);
        }

        #region Caching
        [TestMethod]
        public void EuronextHistoricalData_IsDataCached_NewValue_CorrectlySet()
        {
            bool previous = EuronextHistoricalData.IsDataCached;
            bool expected = !previous;
            EuronextHistoricalData.IsDataCached = expected;
            Assert.AreEqual(expected, EuronextHistoricalData.IsDataCached);
            EuronextHistoricalData.IsDataCached = previous;
        }

        [TestMethod]
        public async Task EuronextHistoricalData_ClearDataCache_NotCalled_DataFromCache()
        {
            // ReSharper disable StringLiteralTypo
            const string text =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\/08\/2012"",""open"":""5.85"",""high"":""5.918"",""low"":""5.84"",""close"":""5.893"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            // ReSharper restore StringLiteralTypo
            bool[] isCalled = { false };
            HistoricalDataRequest spec = CreateSpec(
                "GET", true, DateTime.MinValue, DateTime.MaxValue, InstrumentType.Stock, "FR0010533075", ExchangeMic.Xpar);

            EuronextHistoricalData.ClearDataCache();
            List<Ohlcv> firstList = await SimulateFetchSendAsyncIsCalled(spec, text, isCalled);
            Assert.AreEqual(1, firstList.Count, "first output list has exactly one element");
            Assert.IsTrue(isCalled[0], "SendAsync was called first time");

            isCalled[0] = false;
            List<Ohlcv> secondList = await SimulateFetchSendAsyncIsCalled(spec, text, isCalled);
            EuronextHistoricalData.ClearDataCache();
            Assert.AreEqual(1, secondList.Count, "second output list has exactly one element");
            Assert.IsFalse(isCalled[0], "SendAsync was not called second time");
        }

        [TestMethod]
        public async Task EuronextHistoricalData_ClearDataCache_Called_DataDownloaded()
        {
            // ReSharper disable StringLiteralTypo
            const string text =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\/08\/2012"",""open"":""5.85"",""high"":""5.918"",""low"":""5.84"",""close"":""5.893"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            // ReSharper restore StringLiteralTypo
            bool[] isCalled = { false };
            HistoricalDataRequest spec = CreateSpec(
                "GET", true, DateTime.MinValue, DateTime.MaxValue, InstrumentType.Stock, "FR0010533075", ExchangeMic.Xpar);

            EuronextHistoricalData.ClearDataCache();
            List<Ohlcv> firstList = await SimulateFetchSendAsyncIsCalled(spec, text, isCalled);
            Assert.AreEqual(1, firstList.Count, "first output list has exactly one element");
            Assert.IsTrue(isCalled[0], "SendAsync was called first time");

            EuronextHistoricalData.ClearDataCache();

            isCalled[0] = false;
            List<Ohlcv> secondList = await SimulateFetchSendAsyncIsCalled(spec, text, isCalled);
            EuronextHistoricalData.ClearDataCache();
            Assert.AreEqual(1, secondList.Count, "second output list has exactly one element");
            Assert.IsTrue(isCalled[0], "SendAsync was called second time");
        }

        [TestMethod]
        public async Task EuronextHistoricalData_IsDataCached_True_DataFromCache()
        {
            // ReSharper disable StringLiteralTypo
            const string text =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\/08\/2012"",""open"":""5.85"",""high"":""5.918"",""low"":""5.84"",""close"":""5.893"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            // ReSharper restore StringLiteralTypo
            bool[] isCalled = { false };
            HistoricalDataRequest spec = CreateSpec(
                "GET", true, DateTime.MinValue, DateTime.MaxValue, InstrumentType.Stock, "FR0010533075", ExchangeMic.Xpar);

            EuronextHistoricalData.ClearDataCache();
            bool previous = EuronextHistoricalData.IsDataCached;
            EuronextHistoricalData.IsDataCached = true;
            List<Ohlcv> firstList = await SimulateFetchSendAsyncIsCalled(spec, text, isCalled);
            Assert.AreEqual(1, firstList.Count, "first output list has exactly one element");
            Assert.IsTrue(isCalled[0], "SendAsync was called first time");

            isCalled[0] = false;
            List<Ohlcv> secondList = await SimulateFetchSendAsyncIsCalled(spec, text, isCalled);
            EuronextHistoricalData.IsDataCached = previous;
            EuronextHistoricalData.ClearDataCache();
            Assert.AreEqual(1, secondList.Count, "second output list has exactly one element");
            Assert.IsFalse(isCalled[0], "SendAsync was not called second time");
        }

        [TestMethod]
        public async Task EuronextHistoricalData_IsDataCached_False_DataDownloaded()
        {
            // ReSharper disable StringLiteralTypo
            const string text =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\/08\/2012"",""open"":""5.85"",""high"":""5.918"",""low"":""5.84"",""close"":""5.893"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            // ReSharper restore StringLiteralTypo
            bool[] isCalled = { false };
            HistoricalDataRequest spec = CreateSpec(
                "GET", true, DateTime.MinValue, DateTime.MaxValue, InstrumentType.Stock, "FR0010533075", ExchangeMic.Xpar);

            EuronextHistoricalData.ClearDataCache();
            bool previous = EuronextHistoricalData.IsDataCached;
            EuronextHistoricalData.IsDataCached = false;
            List<Ohlcv> firstList = await SimulateFetchSendAsyncIsCalled(spec, text, isCalled);
            Assert.AreEqual(1, firstList.Count, "first output list has exactly one element");
            Assert.IsTrue(isCalled[0], "SendAsync was called first time");

            isCalled[0] = false;
            List<Ohlcv> secondList = await SimulateFetchSendAsyncIsCalled(spec, text, isCalled);
            EuronextHistoricalData.IsDataCached = previous;
            EuronextHistoricalData.ClearDataCache();
            Assert.AreEqual(1, secondList.Count, "second output list has exactly one element");
            Assert.IsTrue(isCalled[0], "SendAsync was called second time");
        }
        #endregion

        #region TimeoutSeconds
        [TestMethod]
        public void EuronextHistoricalData_TimeoutSeconds_NewValue_CorrectlySet()
        {
            int previous = EuronextHistoricalData.TimeoutSeconds;
            int expected = 999;
            EuronextHistoricalData.TimeoutSeconds = expected;
            Assert.AreEqual(expected, EuronextHistoricalData.TimeoutSeconds);
            EuronextHistoricalData.TimeoutSeconds = previous;
        }

        [TestMethod]
        public async Task EuronextHistoricalData_TimeoutSeconds_HttpClientSendAsyncExceedsTimeout_OutputListEmpty()
        {
            // ReSharper disable StringLiteralTypo
            const string text =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\/08\/2012"",""open"":""5.85"",""high"":""5.918"",""low"":""5.84"",""close"":""5.893"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            // ReSharper restore StringLiteralTypo
            int testTimeoutSeconds = 1;
            const int testRetries = 2;
            const int expectedLogErrorCalls = 3 + 2 * testRetries;

            HistoricalDataRequest spec = CreateSpec(
                "GET", true, DateTime.MinValue, DateTime.MaxValue, InstrumentType.Stock, "FR0010533075", ExchangeMic.Xpar);
            var mockLogger = new MockLogger();

            List<Ohlcv> actualList = await SimulateFetchTimeoutStatusCode(
                spec, text, testTimeoutSeconds + 2, HttpStatusCode.OK, mockLogger, testTimeoutSeconds, testRetries);

            mockLogger.Errors(out int errorCount, out string _);
            Assert.AreEqual(0, actualList.Count, "output list is empty");
            Assert.AreEqual(expectedLogErrorCalls, errorCount, "error log called " + expectedLogErrorCalls + " times");
        }
        #endregion

        #region Retries
        [TestMethod]
        public void EuronextHistoricalData_Retries_NewValue_CorrectlySet()
        {
            int previous = EuronextHistoricalData.Retries;
            int expected = 9;
            EuronextHistoricalData.Retries = expected;
            Assert.AreEqual(expected, EuronextHistoricalData.Retries);
            EuronextHistoricalData.Retries = previous;
        }

        [TestMethod]
        public async Task EuronextHistoricalData_Retries_HttpClientSendAsyncBadStatusCode_PerformsRetriesOutputListEmpty()
        {
            // ReSharper disable StringLiteralTypo
            const string text =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\/08\/2012"",""open"":""5.85"",""high"":""5.918"",""low"":""5.84"",""close"":""5.893"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            // ReSharper restore StringLiteralTypo
            int testTimeout = 1;
            const int testRetries = 2;
            const int expectedLogErrorCalls = 3 + 2 * testRetries;

            HistoricalDataRequest spec = CreateSpec(
                "GET", true, DateTime.MinValue, DateTime.MaxValue, InstrumentType.Stock, "FR0010533075", ExchangeMic.Xpar);
            var mockLogger = new MockLogger();

            List<Ohlcv> actualList = await SimulateFetchTimeoutStatusCode(
                spec, text, testTimeout - 1, HttpStatusCode.Forbidden, mockLogger, testTimeout, testRetries);

            mockLogger.Errors(out int errorCount, out string _);
            Assert.AreEqual(0, actualList.Count, "output list is empty");
            Assert.IsFalse(spec.IsDataAdjusted.HasValue, "adjusted data has no value");
            Assert.AreEqual(expectedLogErrorCalls, errorCount, "error log called " + expectedLogErrorCalls + " times");
        }

        [TestMethod]
        public async Task EuronextHistoricalData_Retries_HttpClientSendAsyncTimeout_PerformsRetriesOutputListEmpty()
        {
            // ReSharper disable StringLiteralTypo
            const string text =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\/08\/2012"",""open"":""5.85"",""high"":""5.918"",""low"":""5.84"",""close"":""5.893"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            // ReSharper restore StringLiteralTypo
            const int testTimeout = 1;
            const int testRetries = 2;
            const int expectedLogErrorCalls = 3 + 2 * testRetries;

            HistoricalDataRequest spec = CreateSpec(
                "GET", true, DateTime.MinValue, DateTime.MaxValue, InstrumentType.Stock, "FR0010533075", ExchangeMic.Xpar);
            var mockLogger = new MockLogger();

            List<Ohlcv> actualList = await SimulateFetchTimeoutStatusCode(
                spec, text, testTimeout + 1, HttpStatusCode.OK, mockLogger, testTimeout, testRetries);

            mockLogger.Errors(out int errorCount, out string _);
            Assert.AreEqual(0, actualList.Count, "output list is empty");
            Assert.IsFalse(spec.IsDataAdjusted.HasValue, "adjusted data has no value");
            Assert.AreEqual(expectedLogErrorCalls, errorCount, "error log called " + expectedLogErrorCalls + " times");
        }
        #endregion

        #region CreateHttpClient
        [TestMethod]
        public void EuronextHistoricalData_CreateHttpClient_WhenCalled_CorrectlyInitialized()
        {
            HttpClient httpClient = EuronextHistoricalDataAccessor.CreateHttpClient();

            Assert.AreEqual(2, httpClient.DefaultRequestHeaders.UserAgent.Count, "UserAgent - count");
            Assert.IsTrue(httpClient.DefaultRequestHeaders.UserAgent.ToString()
                .Contains("Mozilla", StringComparison.Ordinal), "UserAgent - Mozilla");
            Assert.AreEqual(2, httpClient.DefaultRequestHeaders.AcceptEncoding.Count, "AcceptEncoding - count");
            Assert.IsTrue(httpClient.DefaultRequestHeaders.AcceptEncoding.ToString()
                .Contains("gzip", StringComparison.Ordinal), "AcceptEncoding - gzip");
            Assert.IsTrue(httpClient.DefaultRequestHeaders.AcceptEncoding.ToString()
                .Contains("deflate", StringComparison.Ordinal), "AcceptEncoding - deflate");
            Assert.IsTrue(httpClient.DefaultRequestHeaders.Accept.ToString()
                .Contains("json", StringComparison.Ordinal), "Accept - json");
            Assert.IsTrue(httpClient.DefaultRequestHeaders.Accept.ToString()
                .Contains("javascript", StringComparison.Ordinal), "Accept - javascript");

            httpClient.Dispose();
        }
        #endregion

        #region Construction
        [TestMethod]
        public void EuronextHistoricalData_Construction_StaticInitialization_CorrectlyInitialized()
        {
            HttpClient httpClient = EuronextHistoricalDataAccessor.HttpClient();

            Assert.AreEqual(2, httpClient.DefaultRequestHeaders.UserAgent.Count, "UserAgent - count");
            Assert.IsTrue(httpClient.DefaultRequestHeaders.UserAgent.ToString()
                .Contains("Mozilla", StringComparison.Ordinal), "UserAgent - Mozilla");
            Assert.AreEqual(2, httpClient.DefaultRequestHeaders.AcceptEncoding.Count, "AcceptEncoding - count");
            Assert.IsTrue(httpClient.DefaultRequestHeaders.AcceptEncoding.ToString()
                .Contains("gzip", StringComparison.Ordinal), "AcceptEncoding - gzip");
            Assert.IsTrue(httpClient.DefaultRequestHeaders.AcceptEncoding.ToString()
                .Contains("deflate", StringComparison.Ordinal), "AcceptEncoding - deflate");
            Assert.IsTrue(httpClient.DefaultRequestHeaders.Accept.ToString()
                .Contains("json", StringComparison.Ordinal), "Accept - json");
            Assert.IsTrue(httpClient.DefaultRequestHeaders.Accept.ToString()
                .Contains("javascript", StringComparison.Ordinal), "Accept - javascript");
        }
        #endregion

        #region Validate
        [DataTestMethod]
        [DataRow(ExchangeMic.Xams, InstrumentType.Index, TimeGranularity.Day1)]
        [DataRow(ExchangeMic.Xpar, InstrumentType.Stock, TimeGranularity.Week1)]
        [DataRow(ExchangeMic.Xbru, InstrumentType.Etf, TimeGranularity.Week2)]
        [DataRow(ExchangeMic.Xlis, InstrumentType.Etv, TimeGranularity.Month1)]
        [DataRow(ExchangeMic.Xams, InstrumentType.Inav, TimeGranularity.Month2)]
        [DataRow(ExchangeMic.Xams, InstrumentType.Fund, TimeGranularity.Year1)]
        public void EuronextHistoricalData_Validate_WhenValidInput_NoException(ExchangeMic mic, InstrumentType instrumentType, TimeGranularity timeGranularity)
        {
            EuronextHistoricalDataAccessor.Validate(mic.ToString(), "isin", instrumentType, timeGranularity);
            Assert.IsTrue(true);
        }
        #endregion

        #region CreateUrl
        [DataTestMethod]
        [DataRow(InstrumentType.Index)]
        [DataRow(InstrumentType.Stock)]
        [DataRow(InstrumentType.Etf)]
        public void EuronextHistoricalData_CreateUrl_WhenCalled_CorrectDates(InstrumentType type)
        {
            const bool isAdjusted = false;
            const string isin = "FR0010533075";
            const string mic = "Xams";
            DateTime beginDate = new DateTime(2010, 4, 15);
            DateTime endDate = new DateTime(2015, 4, 15);
            long beginMilliseconds = UnixMilliseconds(beginDate);
            long endMilliseconds = UnixMilliseconds(endDate);

            var uri = EuronextHistoricalDataAccessor.CreateUrl(mic, isin, type, isAdjusted, beginDate, endDate, out _);

            Assert.IsTrue(uri.Contains($"&from={beginMilliseconds}&", StringComparison.Ordinal), "(false, beginMilliseconds)");
            Assert.IsTrue(uri.Contains($"&to={endMilliseconds}&", StringComparison.Ordinal), "(false, endMilliseconds)");

            // When the beginDate is set to the DateTime.MinValue, then [1999, 1, 1] will be used.
            beginMilliseconds = UnixMilliseconds(Year1999);

            // When the endDate is set to the DateTime.MaxValue, then [today] will be used.
            endMilliseconds = UnixMilliseconds(DateTime.Today);

            uri = EuronextHistoricalDataAccessor.CreateUrl(mic, isin, type, isAdjusted, DateTime.MinValue, DateTime.MaxValue, out _);

            Assert.IsTrue(uri.Contains($"&from={beginMilliseconds}&", StringComparison.Ordinal), "(false, DateTime.MinValue)");
            Assert.IsTrue(uri.Contains($"&to={endMilliseconds}&", StringComparison.Ordinal), "(false, DateTime.MaxValue)");
        }

        [DataTestMethod]
        [DataRow(InstrumentType.Index)]
        [DataRow(InstrumentType.Stock)]
        [DataRow(InstrumentType.Etf)]
        public void EuronextHistoricalData_CreateUrl_WhenCalled_CorrectIsAdjustedData(InstrumentType type)
        {
            const string isin = "FR0010533075";
            const string mic = "XAMS";
            DateTime beginDate = new DateTime(2010, 4, 15);
            DateTime endDate = new DateTime(2015, 4, 15);

            var uri = EuronextHistoricalDataAccessor.CreateUrl(mic, isin, type, false, beginDate, endDate, out _);
            Assert.IsTrue(uri.Contains("&adjusted=0&", StringComparison.Ordinal), "not adjusted");

            uri = EuronextHistoricalDataAccessor.CreateUrl(mic, isin, type, true, beginDate, endDate, out _);
            Assert.IsTrue(uri.Contains("&adjusted=1&", StringComparison.Ordinal), "adjusted");
        }

        [DataTestMethod]
        [DataRow(InstrumentType.Index)]
        [DataRow(InstrumentType.Stock)]
        [DataRow(InstrumentType.Etf)]
        public void EuronextHistoricalData_CreateUrl_WhenCalled_CorrectUriParameters(InstrumentType type)
        {
            const string isin = "FR0010533075";
            const string mic = "XAMS";
            const string prefix =
                "https://www.euronext.com/sites/euronext.com/modules/common/common_listings/custom/nyx_eu_listings/nyx_eu_listings_price_chart/pricechart/pricechart.php?q=historical_data&";
            DateTime beginDate = new DateTime(2010, 4, 15);
            DateTime endDate = new DateTime(2015, 4, 15);

            var uri = EuronextHistoricalDataAccessor.CreateUrl(mic, isin, type, false, beginDate, endDate, out _);
            Assert.IsTrue(uri.Contains($"&isin={isin}&", StringComparison.Ordinal), "isin");
            Assert.IsTrue(uri.Contains("&mic=XAMS&", StringComparison.Ordinal), "mic");
            Assert.IsTrue(uri.Contains("&dateFormat=d/m/Y&", StringComparison.Ordinal), "dateFormat");
            Assert.IsTrue(uri.Contains("&locale=null", StringComparison.Ordinal), "locate");
            Assert.IsTrue(uri.StartsWith(prefix, StringComparison.Ordinal), "prefix");
        }

        [DataTestMethod]
        [DataRow(InstrumentType.Index, "https://indices.nyx.com/nl/products/indices/{0}-{1}/quotes")]
        [DataRow(InstrumentType.Stock, "https://europeanequities.nyx.com/en/products/equities/{0}-{1}/quotes")]
        [DataRow(InstrumentType.Etf, "https://etp.nyx.com/en/products/funds/{0}-{1}/quotes")]
        public void EuronextHistoricalData_CreateUrl_WhenCalled_CorrectReferrer(InstrumentType type, string refererFormat)
        {
            const string isin = "FR0010533075";
            const string mic = "XAMS";
            string expectedReferer = string.Format(CultureInfo.InvariantCulture, refererFormat, isin, mic);
            DateTime beginDate = new DateTime(2010, 4, 15);
            DateTime endDate = new DateTime(2015, 4, 15);

            EuronextHistoricalDataAccessor.CreateUrl(mic, isin, type, false, beginDate, endDate, out string actualReferer);

            Assert.AreEqual(expectedReferer, actualReferer);
        }
        #endregion

        #region FetchAsync
        [TestMethod, ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
        public async Task EuronextHistoricalData_FetchAsync_WhenGranularityIntraday_Exception()
        {
            var spec = new HistoricalDataRequest(new Instrument(),
                DateTime.MinValue, DateTime.MaxValue, TimeGranularity.Hour3, EndofdateClosingTimeSpan, false);

            await EuronextHistoricalDataAccessor.FetchAsync(spec);

        }

        [TestMethod, ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
        public async Task EuronextHistoricalData_FetchAsync_WhenMissingIsin_Exception()
        {
            HistoricalDataRequest spec = CreateSpec(
                "GET", true, DateTime.MinValue, DateTime.MaxValue, InstrumentType.Stock, null, ExchangeMic.Xpar);

            await EuronextHistoricalDataAccessor.FetchAsync(spec);
        }
        #endregion

        #region GetResponseStreamAsync
        [TestMethod]
        public async Task EuronextHistoricalData_GetResponseStreamAsync_WhenValidStream_ReturnsStream()
        {
            const string data = "Hello, world!";
            using (Stream stream = StreamFromText(data))
            {
                Stream returnedStream = await EuronextHistoricalDataAccessor.GetResponseStreamAsync(
                    new HttpResponseMessage {Content = new StreamContent(stream)});
                Assert.AreEqual(stream.Length, returnedStream.Length, "length");
                using (var streamReader = new StreamReader(stream))
                {
                    string actual = await streamReader.ReadLineAsync();
                    Assert.AreEqual(data, actual, "data");
                }
            }
        }

        [TestMethod, ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
        public async Task EuronextHistoricalData_GetResponseStreamAsync_WhenNullStream_Exception()
        {
            await EuronextHistoricalDataAccessor.GetResponseStreamAsync(new HttpResponseMessage());
        }
        #endregion

        #region Parse
        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenValidJson_OutputListFilled()
        {
            // ReSharper disable StringLiteralTypo
            const string json =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\/08\/2012"",""open"":""5.85"",""high"":""5.918"",""low"":""5.84"",""close"":""5.893"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""},{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""30\/08\/2012"",""open"":""5.87"",""high"":""5.91"",""low"":""5.831"",""close"":""5.887"",""nymberofshares"":""652,785"",""numoftrades"":""1,590"",""turnover"":""3,837,977.93"",""currency"":""EUR""},{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""31\/08\/2012"",""open"":""5.868"",""high"":""5.972"",""low"":""5.867"",""close"":""5.96"",""nymberofshares"":""910,871"",""numoftrades"":""1,732"",""turnover"":""5,415,294.12"",""currency"":""EUR""}]}";

            // ReSharper restore StringLiteralTypo
            List<Ohlcv> actualList = SimulateParse(json, "FR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(0, errorCount, "no errors logged");
            Assert.IsNull(lastErrorText, "no errors text");
            Assert.AreEqual(3, actualList.Count, "output list contains 3 items");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenEmptyJson_OutputListEmpty()
        {
            List<Ohlcv> actualList = SimulateParse(string.Empty, "FR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(1, errorCount, "one error logged");
            Assert.IsTrue(lastErrorText.Contains("no JSON data found", StringComparison.Ordinal), "error text matches pattern");
            Assert.AreEqual(0, actualList.Count, "output list is empty");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenJsonHasNoItems_OutputListEmpty()
        {
            List<Ohlcv> actualList = SimulateParse(@"{""data"":[]}", "FR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(1, errorCount, "one error logged");
            Assert.IsTrue(lastErrorText.Contains("no JSON data found", StringComparison.Ordinal), "error text matches pattern");
            Assert.AreEqual(0, actualList.Count, "output list is empty");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenJsonMissesStartPattern_OutputListEmpty()
        {
            // ReSharper disable StringLiteralTypo
            const string json =
                @"{""data"":[|""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\/08\/2012"",""open"":""5.85"",""high"":""5.918"",""low"":""5.84"",""close"":""5.893"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""},{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""30\/08\/2012"",""open"":""5.87"",""high"":""5.91"",""low"":""5.831"",""close"":""5.887"",""nymberofshares"":""652,785"",""numoftrades"":""1,590"",""turnover"":""3,837,977.93"",""currency"":""EUR""},{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""31\/08\/2012"",""open"":""5.868"",""high"":""5.972"",""low"":""5.867"",""close"":""5.96"",""nymberofshares"":""910,871"",""numoftrades"":""1,732"",""turnover"":""5,415,294.12"",""currency"":""EUR""}]}";

            // ReSharper restore StringLiteralTypo
            List<Ohlcv> actualList = SimulateParse(json, "FR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(1, errorCount, "one error logged");
            Assert.IsTrue(lastErrorText.Contains("no JSON data found", StringComparison.Ordinal), "error text matches pattern");
            Assert.AreEqual(0, actualList.Count, "output list is empty");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenInvalidItemDelimiter_ThisItemIsSkipped()
        {
            // ReSharper disable StringLiteralTypo
            const string json =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\/08\/2012"",""open"":""5.85"",""high"":""5.918"",""low"":""5.84"",""close"":""5.893"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}|{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""30\/08\/2012"",""open"":""5.87"",""high"":""5.91"",""low"":""5.831"",""close"":""5.887"",""nymberofshares"":""652,785"",""numoftrades"":""1,590"",""turnover"":""3,837,977.93"",""currency"":""EUR""},{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""31\/08\/2012"",""open"":""5.868"",""high"":""5.972"",""low"":""5.867"",""close"":""5.96"",""nymberofshares"":""910,871"",""numoftrades"":""1,732"",""turnover"":""5,415,294.12"",""currency"":""EUR""}]}";

            // ReSharper restore StringLiteralTypo
            List<Ohlcv> actualList = SimulateParse(json, "FR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(0, errorCount, "no errors logged");
            Assert.IsNull(lastErrorText, "no errors text");
            Assert.AreEqual(2, actualList.Count, "output list has 2 items");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenInvalidItem_ThisItemIsSkipped()
        {
            // ReSharper disable StringLiteralTypo
            const string json =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\/08\/2012"",""open"":""5.85"",""high"":""5.918"",""low"":""5.84"",""close"":""5.893"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""},{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""30z\/08\/2012"",""open"":""5.8"",""high"":""5.91"",""low"":""5.831"",""close"":""5.887"",""nymberofshares"":""652,785"",""numoftrades"":""1,590"",""turnover"":""3,837,977.93"",""currency"":""EUR""},{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""31\/08\/2012"",""open"":""5.868"",""high"":""5.972"",""low"":""5.867"",""close"":""5.96"",""nymberofshares"":""910,871"",""numoftrades"":""1,732"",""turnover"":""5,415,294.12"",""currency"":""EUR""}]}";

            // ReSharper restore StringLiteralTypo
            List<Ohlcv> actualList = SimulateParse(json, "FR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(1, errorCount, "one error logged");
            Assert.IsTrue(lastErrorText.Contains("invalid [date] ", StringComparison.Ordinal), "error text matches pattern");
            Assert.AreEqual(2, actualList.Count, "output list has 2 items");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenEmpty_ErrorLoggedItemSkipped()
        {
            List<Ohlcv> actualList = SimulateParse(@"{""data"":[{}]}", "FR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(actualList.Count, 0, "output is empty");
            Assert.AreEqual(1, errorCount, "error count is 1");
            Assert.IsNotNull(lastErrorText, "error text is not null");
            Assert.IsTrue(lastErrorText.Contains("invalid [ISIN] splitted item", StringComparison.Ordinal), "error text contains a pattern");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenMissingIsin_ErrorLoggedItemSkipped()
        {
            // ReSharper disable StringLiteralTypo
            const string json =
                @"{""data"":[{""MIC"":""Euronext Paris, London"",""date"":""29\/08\/2012"",""open"":""5.85"",""high"":""5.918"",""low"":""5.84"",""close"":""5.893"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            // ReSharper restore StringLiteralTypo
            List<Ohlcv> actualList = SimulateParse(json, "FR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(actualList.Count, 0, "output is empty");
            Assert.AreEqual(1, errorCount, "error count is 1");
            Assert.IsNotNull(lastErrorText, "error text is not null");
            Assert.IsTrue(lastErrorText.Contains("invalid [ISIN] splitted item", StringComparison.Ordinal), "error text contains a pattern");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenMissingIsinPattern_ErrorLoggedItemSkipped()
        {
            // ReSharper disable StringLiteralTypo
            const string json =
                @"{""data"":[{""ISIN"";""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\/08\/2012"",""open"":""5.85"",""high"":""5.918"",""low"":""5.84"",""close"":""5.893"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            // ReSharper restore StringLiteralTypo
            List<Ohlcv> actualList = SimulateParse(json, "FR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(actualList.Count, 0, "output is empty");
            Assert.AreEqual(1, errorCount, "error count is 1");
            Assert.IsNotNull(lastErrorText, "error text is not null");
            Assert.IsTrue(lastErrorText.Contains("invalid [ISIN] splitted item", StringComparison.Ordinal), "error text contains a pattern");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenMissingIsinClosingQuote_ErrorLoggedItemSkipped()
        {
            // ReSharper disable StringLiteralTypo
            const string json =
                @"{""data"":[{""ISIN"":""FR0010533075,""MIC"":""Euronext Paris, London"",""date"":""29\/08\/2012"",""open"":""5.85"",""high"":""5.918"",""low"":""5.84"",""close"":""5.893"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            // ReSharper restore StringLiteralTypo
            List<Ohlcv> actualList = SimulateParse(json, "FR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(actualList.Count, 0, "output is empty");
            Assert.AreEqual(1, errorCount, "error count is 1");
            Assert.IsNotNull(lastErrorText, "error text is not null");
            Assert.IsTrue(lastErrorText.Contains("invalid [ISIN] splitted item", StringComparison.Ordinal), "error text contains a pattern");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenIsinDoesNotMatch_ErrorLoggedItemSkipped()
        {
            // ReSharper disable StringLiteralTypo
            const string json =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\/08\/2012"",""open"":""5.85"",""high"":""5.918"",""low"":""5.84"",""close"":""5.893"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            // ReSharper restore StringLiteralTypo
            List<Ohlcv> actualList = SimulateParse(json, "zFR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(actualList.Count, 0, "output is empty");
            Assert.AreEqual(1, errorCount, "error count is 1");
            Assert.IsNotNull(lastErrorText, "error text is not null");
            Assert.IsTrue(lastErrorText.Contains("differs from [ISIN] item", StringComparison.Ordinal), "error text contains a pattern");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenMissingDatePattern_ErrorLoggedItemSkipped()
        {
            // ReSharper disable StringLiteralTypo
            const string json =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"";""29\/08\/2012"",""open"":""5.85"",""high"":""5.918"",""low"":""5.84"",""close"":""5.893"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            // ReSharper restore StringLiteralTypo
            List<Ohlcv> actualList = SimulateParse(json, "FR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(actualList.Count, 0, "output is empty");
            Assert.AreEqual(1, errorCount, "error count is 1");
            Assert.IsNotNull(lastErrorText, "error text is not null");
            Assert.IsTrue(lastErrorText.Contains("invalid [date] splitted item", StringComparison.Ordinal), "error text contains a pattern");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenMissingDateDayMonthBackSlash_ErrorLoggedItemSkipped()
        {
            // ReSharper disable StringLiteralTypo
            const string json =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29|/08\/2012"",""open"":""5.85"",""high"":""5.918"",""low"":""5.84"",""close"":""5.893"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            // ReSharper restore StringLiteralTypo
            List<Ohlcv> actualList = SimulateParse(json, "FR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(actualList.Count, 0, "output is empty");
            Assert.AreEqual(1, errorCount, "error count is 1");
            Assert.IsNotNull(lastErrorText, "error text is not null");
            Assert.IsTrue(lastErrorText.Contains("invalid [date] splitted item", StringComparison.Ordinal), "error text contains a pattern");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenMissingDateDayMonthForwardSlash_ErrorLoggedItemSkipped()
        {
            // ReSharper disable StringLiteralTypo
            const string json =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\|08\/2012"",""open"":""5.85"",""high"":""5.918"",""low"":""5.84"",""close"":""5.893"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            // ReSharper restore StringLiteralTypo
            List<Ohlcv> actualList = SimulateParse(json, "FR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(actualList.Count, 0, "output is empty");
            Assert.AreEqual(1, errorCount, "error count is 1");
            Assert.IsNotNull(lastErrorText, "error text is not null");
            Assert.IsTrue(lastErrorText.Contains("invalid [date] splitted item", StringComparison.Ordinal), "error text contains a pattern");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenMissingDateMonthYearBackSlash_ErrorLoggedItemSkipped()
        {
            // ReSharper disable StringLiteralTypo
            const string json =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\/08|/2012"",""open"":""5.85"",""high"":""5.918"",""low"":""5.84"",""close"":""5.893"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            // ReSharper restore StringLiteralTypo
            List<Ohlcv> actualList = SimulateParse(json, "FR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(actualList.Count, 0, "output is empty");
            Assert.AreEqual(1, errorCount, "error count is 1");
            Assert.IsNotNull(lastErrorText, "error text is not null");
            Assert.IsTrue(lastErrorText.Contains("invalid [date] splitted item", StringComparison.Ordinal), "error text contains a pattern");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenMissingDateMonthYearForwardSlash_ErrorLoggedItemSkipped()
        {
            // ReSharper disable StringLiteralTypo
            const string json =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\/08\|2012"",""open"":""5.85"",""high"":""5.918"",""low"":""5.84"",""close"":""5.893"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            // ReSharper restore StringLiteralTypo
            List<Ohlcv> actualList = SimulateParse(json, "FR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(actualList.Count, 0, "output is empty");
            Assert.AreEqual(1, errorCount, "error count is 1");
            Assert.IsNotNull(lastErrorText, "error text is not null");
            Assert.IsTrue(lastErrorText.Contains("invalid [date] splitted item", StringComparison.Ordinal), "error text contains a pattern");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenInvalidDateLength_ErrorLoggedItemSkipped()
        {
            // ReSharper disable StringLiteralTypo
            const string json =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""299\/08\/2012"",""open"":""5.85"",""high"":""5.918"",""low"":""5.84"",""close"":""5.893"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            // ReSharper restore StringLiteralTypo
            List<Ohlcv> actualList = SimulateParse(json, "FR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(actualList.Count, 0, "output is empty");
            Assert.AreEqual(1, errorCount, "error count is 1");
            Assert.IsNotNull(lastErrorText, "error text is not null");
            Assert.IsTrue(lastErrorText.Contains("invalid [date] splitted item", StringComparison.Ordinal), "error text contains a pattern");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenMissingOpenPattern_ErrorLoggedItemSkipped()
        {
            // ReSharper disable StringLiteralTypo
            const string json =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\/08\/2012"",""open"";""5.85"",""high"":""5.918"",""low"":""5.84"",""close"":""5.893"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            // ReSharper restore StringLiteralTypo
            List<Ohlcv> actualList = SimulateParse(json, "FR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(actualList.Count, 0, "output is empty");
            Assert.AreEqual(1, errorCount, "error count is 1");
            Assert.IsNotNull(lastErrorText, "error text is not null");
            Assert.IsTrue(lastErrorText.Contains("invalid [open] splitted item", StringComparison.Ordinal), "error text contains a pattern");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenMissingOpenClosingQuote_ErrorLoggedItemSkipped()
        {
            // ReSharper disable StringLiteralTypo
            const string json =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\/08\/2012"",""open"":""5.85,""high"":""5.918"",""low"":""5.84"",""close"":""5.893"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            // ReSharper restore StringLiteralTypo
            List<Ohlcv> actualList = SimulateParse(json, "FR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(actualList.Count, 0, "output is empty");
            Assert.AreEqual(1, errorCount, "error count is 1");
            Assert.IsNotNull(lastErrorText, "error text is not null");
            Assert.IsTrue(lastErrorText.Contains("invalid [open] splitted item", StringComparison.Ordinal), "error text contains a pattern");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenOpenContainsNull_ErrorLoggedItemSkipped()
        {
            // ReSharper disable StringLiteralTypo
            const string json =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\/08\/2012"",""open"":""null"",""high"":""5.918"",""low"":""5.84"",""close"":""5.893"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            // ReSharper restore StringLiteralTypo
            List<Ohlcv> actualList = SimulateParse(json, "FR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(actualList.Count, 0, "output is empty");
            Assert.AreEqual(1, errorCount, "error count is 1");
            Assert.IsNotNull(lastErrorText, "error text is not null");
            Assert.IsTrue(lastErrorText.Contains("invalid [open] splitted item", StringComparison.Ordinal), "error text contains a pattern");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenInvalidOpenNumber_ErrorLoggedItemSkipped()
        {
            // ReSharper disable StringLiteralTypo
            const string json =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\/08\/2012"",""open"":""5.b5"",""high"":""5.918"",""low"":""5.84"",""close"":""5.893"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            // ReSharper restore StringLiteralTypo
            List<Ohlcv> actualList = SimulateParse(json, "FR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(actualList.Count, 0, "output is empty");
            Assert.AreEqual(1, errorCount, "error count is 1");
            Assert.IsNotNull(lastErrorText, "error text is not null");
            Assert.IsTrue(lastErrorText.Contains("invalid [open] price [", StringComparison.Ordinal), "error text contains a pattern");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenMissingHighPattern_ErrorLoggedItemSkipped()
        {
            // ReSharper disable StringLiteralTypo
            const string json =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\/08\/2012"",""open"":""5.85"",""high"";""5.918"",""low"":""5.84"",""close"":""5.893"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            // ReSharper restore StringLiteralTypo
            List<Ohlcv> actualList = SimulateParse(json, "FR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(actualList.Count, 0, "output is empty");
            Assert.AreEqual(1, errorCount, "error count is 1");
            Assert.IsNotNull(lastErrorText, "error text is not null");
            Assert.IsTrue(lastErrorText.Contains("invalid [high] splitted item", StringComparison.Ordinal), "error text contains a pattern");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenMissingHighClosingQuote_ErrorLoggedItemSkipped()
        {
            // ReSharper disable StringLiteralTypo
            const string json =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\/08\/2012"",""open"":""5.85"",""high"":""5.918,""low"":""5.84"",""close"":""5.893"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            // ReSharper restore StringLiteralTypo
            List<Ohlcv> actualList = SimulateParse(json, "FR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(actualList.Count, 0, "output is empty");
            Assert.AreEqual(1, errorCount, "error count is 1");
            Assert.IsNotNull(lastErrorText, "error text is not null");
            Assert.IsTrue(lastErrorText.Contains("invalid [high] splitted item", StringComparison.Ordinal), "error text contains a pattern");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenHighContainsNull_ErrorLoggedItemSkipped()
        {
            // ReSharper disable StringLiteralTypo
            const string json =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\/08\/2012"",""open"":""5.85"",""high"":""null"",""low"":""5.84"",""close"":""5.893"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            // ReSharper restore StringLiteralTypo
            List<Ohlcv> actualList = SimulateParse(json, "FR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(actualList.Count, 0, "output is empty");
            Assert.AreEqual(1, errorCount, "error count is 1");
            Assert.IsNotNull(lastErrorText, "error text is not null");
            Assert.IsTrue(lastErrorText.Contains("invalid [high] splitted item", StringComparison.Ordinal), "error text contains a pattern");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenInvalidHighNumber_ErrorLoggedItemSkipped()
        {
            // ReSharper disable StringLiteralTypo
            const string json =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\/08\/2012"",""open"":""5.85"",""high"":""5.9b8"",""low"":""5.84"",""close"":""5.893"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            // ReSharper restore StringLiteralTypo
            List<Ohlcv> actualList = SimulateParse(json, "FR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(actualList.Count, 0, "output is empty");
            Assert.AreEqual(1, errorCount, "error count is 1");
            Assert.IsNotNull(lastErrorText, "error text is not null");
            Assert.IsTrue(lastErrorText.Contains("invalid [high] price [", StringComparison.Ordinal), "error text contains a pattern");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenMissingLowPattern_ErrorLoggedItemSkipped()
        {
            // ReSharper disable StringLiteralTypo
            const string json =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\/08\/2012"",""open"":""5.85"",""high"":""5.918"",""low"";""5.84"",""close"":""5.893"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            // ReSharper restore StringLiteralTypo
            List<Ohlcv> actualList = SimulateParse(json, "FR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(actualList.Count, 0, "output is empty");
            Assert.AreEqual(1, errorCount, "error count is 1");
            Assert.IsNotNull(lastErrorText, "error text is not null");
            Assert.IsTrue(lastErrorText.Contains("invalid [low] splitted item", StringComparison.Ordinal), "error text contains a pattern");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenMissingLowClosingQuote_ErrorLoggedItemSkipped()
        {
            // ReSharper disable StringLiteralTypo
            const string json =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\/08\/2012"",""open"":""5.85"",""high"":""5.918"",""low"":""5.84,""close"":""5.893"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            // ReSharper restore StringLiteralTypo
            List<Ohlcv> actualList = SimulateParse(json, "FR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(actualList.Count, 0, "output is empty");
            Assert.AreEqual(1, errorCount, "error count is 1");
            Assert.IsNotNull(lastErrorText, "error text is not null");
            Assert.IsTrue(lastErrorText.Contains("invalid [low] splitted item", StringComparison.Ordinal), "error text contains a pattern");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenLowContainsNull_ErrorLoggedItemSkipped()
        {
            // ReSharper disable StringLiteralTypo
            const string json =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\/08\/2012"",""open"":""5.85"",""high"":""5.918"",""low"":""null"",""close"":""5.893"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            // ReSharper restore StringLiteralTypo
            List<Ohlcv> actualList = SimulateParse(json, "FR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(actualList.Count, 0, "output is empty");
            Assert.AreEqual(1, errorCount, "error count is 1");
            Assert.IsNotNull(lastErrorText, "error text is not null");
            Assert.IsTrue(lastErrorText.Contains("invalid [low] splitted item", StringComparison.Ordinal), "error text contains a pattern");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenInvalidLowNumber_ErrorLoggedItemSkipped()
        {
            // ReSharper disable StringLiteralTypo
            const string json =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\/08\/2012"",""open"":""5.85"",""high"":""5.918"",""low"":""5.b4"",""close"":""5.893"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            // ReSharper restore StringLiteralTypo
            List<Ohlcv> actualList = SimulateParse(json, "FR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(actualList.Count, 0, "output is empty");
            Assert.AreEqual(1, errorCount, "error count is 1");
            Assert.IsNotNull(lastErrorText, "error text is not null");
            Assert.IsTrue(lastErrorText.Contains("invalid [low] price [", StringComparison.Ordinal), "error text contains a pattern");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenMissingClosePattern_ErrorLoggedItemSkipped()
        {
            // ReSharper disable StringLiteralTypo
            const string json =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\/08\/2012"",""open"":""5.85"",""high"":""5.918"",""low"":""5.84"",""close"";""5.893"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";            // ReSharper disable StringLiteralTypo

            // ReSharper restore StringLiteralTypo
            List<Ohlcv> actualList = SimulateParse(json, "FR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(actualList.Count, 0, "output is empty");
            Assert.AreEqual(1, errorCount, "error count is 1");
            Assert.IsNotNull(lastErrorText, "error text is not null");
            Assert.IsTrue(lastErrorText.Contains("invalid [close] splitted item", StringComparison.Ordinal), "error text contains a pattern");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenMissingCloseClosingQuote_ErrorLoggedItemSkipped()
        {
            // ReSharper disable StringLiteralTypo
            const string json =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\/08\/2012"",""open"":""5.85"",""high"":""5.918"",""low"":""5.84"",""close"":""5.893,""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            // ReSharper restore StringLiteralTypo
            List<Ohlcv> actualList = SimulateParse(json, "FR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(actualList.Count, 0, "output is empty");
            Assert.AreEqual(1, errorCount, "error count is 1");
            Assert.IsNotNull(lastErrorText, "error text is not null");
            Assert.IsTrue(lastErrorText.Contains("invalid [close] splitted item", StringComparison.Ordinal), "error text contains a pattern");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenCloseContainsNull_ErrorLoggedItemSkipped()
        {
            const string json =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\/08\/2012"",""open"":""5.85"",""high"":""5.918"",""low"":""5.84"",""close"":""null"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            List<Ohlcv> actualList = SimulateParse(json, "FR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(actualList.Count, 0, "output is empty");
            Assert.AreEqual(1, errorCount, "error count is 1");
            Assert.IsNotNull(lastErrorText, "error text is not null");
            Assert.IsTrue(lastErrorText.Contains("invalid [close] splitted item", StringComparison.Ordinal), "error text contains a pattern");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenInvalidCloseNumber_ErrorLoggedItemSkipped()
        {
            const string json =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\/08\/2012"",""open"":""5.85"",""high"":""5.918"",""low"":""5.84"",""close"":""5.8b3"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            List<Ohlcv> actualList = SimulateParse(json, "FR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(actualList.Count, 0, "output is empty");
            Assert.AreEqual(1, errorCount, "error count is 1");
            Assert.IsNotNull(lastErrorText, "error text is not null");
            Assert.IsTrue(lastErrorText.Contains("invalid [close] price [", StringComparison.Ordinal), "error text contains a pattern");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenMissingVolumePattern_ErrorLoggedItemSkipped()
        {
            const string json =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\/08\/2012"",""open"":""5.85"",""high"":""5.918"",""low"":""5.84"",""close"":""5.893"",""nymberofshares"";""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            List<Ohlcv> actualList = SimulateParse(json, "FR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(actualList.Count, 0, "output is empty");
            Assert.AreEqual(1, errorCount, "error count is 1");
            Assert.IsNotNull(lastErrorText, "error text is not null");
            Assert.IsTrue(lastErrorText.Contains("invalid [number of shares] splitted item", StringComparison.Ordinal), "error text contains a pattern");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenMissingVolumeClosingQuote_ErrorLoggedItemSkipped()
        {
            const string json =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\/08\/2012"",""open"":""5.85"",""high"":""5.918"",""low"":""5.84"",""close"":""5.893"",""nymberofshares"":""589,993,""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            List<Ohlcv> actualList = SimulateParse(json, "FR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(actualList.Count, 0, "output is empty");
            Assert.AreEqual(1, errorCount, "error count is 1");
            Assert.IsNotNull(lastErrorText, "error text is not null");
            Assert.IsTrue(lastErrorText.Contains("invalid [number of shares] splitted item", StringComparison.Ordinal), "error text contains a pattern");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenVolumeContainsNull_ErrorLoggedItemSkipped()
        {
            const string json =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\/08\/2012"",""open"":""5.85"",""high"":""5.918"",""low"":""5.84"",""close"":""5.893"",""nymberofshares"":""null"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            List<Ohlcv> actualList = SimulateParse(json, "FR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(actualList.Count, 0, "output is empty");
            Assert.AreEqual(1, errorCount, "error count is 1");
            Assert.IsNotNull(lastErrorText, "error text is not null");
            Assert.IsTrue(lastErrorText.Contains("invalid [number of shares] splitted item", StringComparison.Ordinal), "error text contains a pattern");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenInvalidVolumeNumber_ErrorLoggedItemSkipped()
        {
            const string json =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\/08\/2012"",""open"":""5.85"",""high"":""5.918"",""low"":""5.84"",""close"":""5.893"",""nymberofshares"":""589,9b3"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            List<Ohlcv> actualList = SimulateParse(json, "FR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(actualList.Count, 0, "output is empty");
            Assert.AreEqual(1, errorCount, "error count is 1");
            Assert.IsNotNull(lastErrorText, "error text is not null");
            Assert.IsTrue(lastErrorText.Contains("invalid [number of shares] splitted item", StringComparison.Ordinal), "error text contains a pattern");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenOpenHasThousandSeparator_ParsedNoErrorLogged()
        {
            const double delta = 0.0001;
            const string json =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\/08\/2012"",""open"":""5,666.85"",""high"":""5.918"",""low"":""5.84"",""close"":""5.893"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            List<Ohlcv> actualList = SimulateParse(json, "FR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(actualList.Count, 1, "output has a single item");
            Assert.AreEqual(0, errorCount, "error count is 0");
            Assert.IsNull(lastErrorText, "error text is null");
            Assert.AreEqual(5666.85, actualList[0].Open, delta, "open");
            Assert.AreEqual(5.918, actualList[0].High, delta, "high");
            Assert.AreEqual(5.84, actualList[0].Low, delta, "low");
            Assert.AreEqual(5.893, actualList[0].Close, delta, "close");
            Assert.AreEqual(589993, actualList[0].Volume, delta, "volume");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenOpenHasExponentNotation_ParsedNoErrorLogged()
        {
            const double delta = 0.000001;
            const string json =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\/08\/2012"",""open"":""5.85e-3"",""high"":""5.918"",""low"":""5.84"",""close"":""5.893"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            List<Ohlcv> actualList = SimulateParse(json, "FR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(actualList.Count, 1, "output has a single item");
            Assert.AreEqual(0, errorCount, "error count is 0");
            Assert.IsNull(lastErrorText, "error text is null");
            Assert.AreEqual(0.00585, actualList[0].Open, delta, "open");
            Assert.AreEqual(5.918, actualList[0].High, delta, "high");
            Assert.AreEqual(5.84, actualList[0].Low, delta, "low");
            Assert.AreEqual(5.893, actualList[0].Close, delta, "close");
            Assert.AreEqual(589993, actualList[0].Volume, delta, "volume");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenInputCorrect_ParsedNoErrorLogged()
        {
            const double delta = 0.0001;
            const string json =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\/08\/2012"",""open"":""5.85"",""high"":""5.918"",""low"":""5.84"",""close"":""5.893"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            List<Ohlcv> actualList = SimulateParse(json, "FR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(actualList.Count, 1, "output has a single item");
            Assert.AreEqual(0, errorCount, "error count is 0");
            Assert.IsNull(lastErrorText, "error text is null");
            Assert.AreEqual(5.85, actualList[0].Open, delta, "open");
            Assert.AreEqual(5.918, actualList[0].High, delta, "high");
            Assert.AreEqual(5.84, actualList[0].Low, delta, "low");
            Assert.AreEqual(5.893, actualList[0].Close, delta, "close");
            Assert.AreEqual(589993, actualList[0].Volume, delta, "volume");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenInputCorrectNoEndofdayClosingTime_ParsedNoErrorLogged()
        {
            const string json =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\/08\/2012"",""open"":""5.85"",""high"":""5.918"",""low"":""5.84"",""close"":""5.893"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            List<Ohlcv> actualList = SimulateParse(json, "FR0010533075", null, out int errorCount, out string lastErrorText);

            Assert.AreEqual(actualList.Count, 1, "output has a single item");
            Assert.AreEqual(0, errorCount, "error count is 0");
            Assert.IsNull(lastErrorText, "error text is null");
            Assert.AreEqual(new DateTime(2012, 8, 29, 19, 0, 0), actualList[0].Time, "date");
        }

        [TestMethod]
        public void EuronextHistoricalData_Parse_WhenInputCorrectWithEndofdayClosingTime_ParsedNoErrorLogged()
        {
            const string json =
                @"{""data"":[{""ISIN"":""FR0010533075"",""MIC"":""Euronext Paris, London"",""date"":""29\/08\/2012"",""open"":""5.85"",""high"":""5.918"",""low"":""5.84"",""close"":""5.893"",""nymberofshares"":""589,993"",""numoftrades"":""1,467"",""turnover"":""3,465,442.27"",""currency"":""EUR""}]}";

            List<Ohlcv> actualList = SimulateParse(json, "FR0010533075", new TimeSpan(16, 30, 15), out int errorCount, out string lastErrorText);

            Assert.AreEqual(actualList.Count, 1, "output has a single item");
            Assert.AreEqual(0, errorCount, "error count is 0");
            Assert.IsNull(lastErrorText, "error text is null");
            Assert.AreEqual(new DateTime(2012, 8, 29, 16, 30, 15), actualList[0].Time, "date");
        }
        #endregion
    }
}
