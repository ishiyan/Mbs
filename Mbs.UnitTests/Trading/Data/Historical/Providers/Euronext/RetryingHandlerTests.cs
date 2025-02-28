﻿using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mbs.Trading.Data.Historical.Providers.Euronext;
using Mbs.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.UnitTests.Trading.Data.Historical.Providers.Euronext
{
    [TestClass]
    public class RetryingHandlerTests
    {
        [TestMethod]
        [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
        public async Task RetryingHandler_SendAsync_AllRetriesTimeout_ThrowsException()
        {
            const string text = "foobar";

            int testTimeoutSeconds = 1;
            const int testRetries = 2;
            const int expectedLogErrorCalls = 3 + 2 * testRetries;

            var mockLogger = new MockLogger();

            string result = await SimulateSendAsync(
                text,
                testTimeoutSeconds + 2,
                HttpStatusCode.OK,
                mockLogger,
                testTimeoutSeconds,
                testRetries);

            mockLogger.Errors(out int errorCount, out string _);
            Assert.IsNull(result, "result is null");
            Assert.AreEqual(expectedLogErrorCalls, errorCount, "error log called " + expectedLogErrorCalls + " times");
        }

        [TestMethod]
        public async Task RetryingHandler_SendAsync_NoTimeout_ReturnsResult()
        {
            const string text = "foobar";

            int testTimeoutSeconds = 1;
            const int testRetries = 2;
            const int expectedLogErrorCalls = 0;

            var mockLogger = new MockLogger();

            string result = await SimulateSendAsync(
                text,
                testTimeoutSeconds - 1,
                HttpStatusCode.OK,
                mockLogger,
                testTimeoutSeconds,
                testRetries);

            mockLogger.Errors(out int errorCount, out string _);
            Assert.IsNotNull(result, "result is not null");
            Assert.AreEqual(text, result, "output equals input");
            Assert.AreEqual(expectedLogErrorCalls, errorCount, "error log called " + expectedLogErrorCalls + " times");
        }

        [TestMethod]
        public void RetryingHandler_LogPrefix_WhenSet_GetsCorrectValue()
        {
            static string LogPrefixAction() => "foo";
            static string LogPrefixActionAnother() => "bar";

            using var retryingHandler = new RetryingHandler(new HttpClientHandler { UseDefaultCredentials = true })
            {
                LogPrefix = LogPrefixAction,
            };

            Assert.AreEqual(LogPrefixAction, retryingHandler.LogPrefix);
            Assert.AreNotEqual(LogPrefixActionAnother, retryingHandler.LogPrefix);
        }

        [TestMethod]
        public void RetryingHandler_TimeoutSeconds_WhenSet_GetsCorrectValue()
        {
            static int TimeoutSecondsAction() => 42;
            static int TimeoutSecondsActionAnother() => 43;

            using var retryingHandler = new RetryingHandler(new HttpClientHandler { UseDefaultCredentials = true })
            {
                TimeoutSeconds = TimeoutSecondsAction,
            };

            Assert.AreEqual(TimeoutSecondsAction, retryingHandler.TimeoutSeconds);
            Assert.AreNotEqual(TimeoutSecondsActionAnother, retryingHandler.TimeoutSeconds);
        }

        [TestMethod]
        public void RetryingHandler_Retries_WhenSet_GetsCorrectValue()
        {
            static int RetriesAction() => 42;
            static int RetriesActionAnother() => 43;

            using var retryingHandler = new RetryingHandler(new HttpClientHandler { UseDefaultCredentials = true })
            {
                Retries = RetriesAction,
            };

            Assert.AreEqual(RetriesAction, retryingHandler.Retries);
            Assert.AreNotEqual(RetriesActionAnother, retryingHandler.Retries);
        }

        private static async Task<string> SimulateSendAsync(string text, int timeoutSeconds, HttpStatusCode statusCode, MockLogger mockLogger, int retryingHandlerTimeoutSeconds, int retryingHandlerRetries)
        {
            var testDelegatingHandler = new TestDelegatingHandler(new HttpClientHandler { UseDefaultCredentials = true })
            {
                TimeoutMilliseconds = timeoutSeconds * 1000,
                Text = text,
                StatusCode = statusCode,
            };

            var retryingHandler = new RetryingHandler(new HttpClientHandler { UseDefaultCredentials = true })
            {
                InnerHandler = testDelegatingHandler,
                LogPrefix = () => EuronextHistoricalData.Prefix,
                TimeoutSeconds = () => retryingHandlerTimeoutSeconds,
                Retries = () => retryingHandlerRetries,
            };

            string url = "http://foo.bar.org";

            Log.SetLogger(mockLogger);

            using var client = new HttpClient(retryingHandler);
            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            using var cancelSource = new CancellationTokenSource();
            using HttpResponseMessage response = await client.SendAsync(request, cancelSource.Token);
            await using Stream responseStream = await response.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(responseStream);
            return await streamReader.ReadToEndAsync();
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

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                if (TimeoutMilliseconds > 0)
                {
                    Thread.Sleep(TimeoutMilliseconds);
                }

                cancellationToken.ThrowIfCancellationRequested();

                return Task.FromResult(new HttpResponseMessage
                {
                    Content = new StringContent(Text, Encoding.UTF8, "application/json"),
                    StatusCode = StatusCode,
                    ReasonPhrase = "reason phrase",
                });
            }
        }
    }
}
