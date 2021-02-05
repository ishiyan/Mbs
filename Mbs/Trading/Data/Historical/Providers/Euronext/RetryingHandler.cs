using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Mbs.Utilities;

namespace Mbs.Trading.Data.Historical.Providers.Euronext
{
    /// <summary>
    /// Retries download up to <see cref="Retries"/> times waiting <see cref="TimeoutSeconds"/> each attempt.
    /// </summary>
    public class RetryingHandler : DelegatingHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RetryingHandler"/> class.
        /// </summary>
        /// <param name="innerHandler">An inner handler.</param>
        public RetryingHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        /// <summary>
        /// Gets or sets the prefix to log information.
        /// </summary>
        public Func<string> LogPrefix { get; set; }

        /// <summary>
        /// Gets or sets per request download timeout in seconds.
        /// </summary>
        public Func<int> TimeoutSeconds { get; set; }

        /// <summary>
        /// Gets or sets the number of download retries.
        /// </summary>
        public Func<int> Retries { get; set; }

        /// <inheritdoc />
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            const string failedToGetHttpResponse = "failed to get HTTP response";

            string prefix = LogPrefix();
            int retries = Retries();
            int timeoutMilliseconds = TimeoutSeconds() * 1000;

            for (int i = 0; i <= retries; ++i)
            {
                if (i > 0)
                {
                    Log.Error($"{prefix} retrying {i} of {retries} [{request.RequestUri}] ...");
                }

                HttpResponseMessage response;
                try
                {
                    using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                    cts.CancelAfter(timeoutMilliseconds);
                    response = await base.SendAsync(request, cts.Token);
                    if (response.IsSuccessStatusCode)
                    {
                        return response;
                    }
                }
                catch (Exception e)
                {
                    Log.Error($"{prefix} {failedToGetHttpResponse}", e);
                    continue;
                }

                Log.Error($"{prefix} {failedToGetHttpResponse}, status code: {response.StatusCode}, reason: {response.ReasonPhrase}");
            }

            Log.Error($"{prefix} retry count exceeded {retries}, giving up [{request.RequestUri}].");
            throw new Exception($"{prefix} failed to download [{request.RequestUri}].");
        }
    }
}
