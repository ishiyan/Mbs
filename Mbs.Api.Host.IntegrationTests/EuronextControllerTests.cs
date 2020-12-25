using System;
using System.Net.Http;
using System.Threading.Tasks;
using Mbs.Api.Services.Trading.Data.Historical;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mbs.Api.Host.IntegrationTests
{
    [TestClass]
    public sealed class EuronextControllerTests : IDisposable
    {
        private readonly WebApplicationFactory<Startup> factory = new WebApplicationFactory<Startup>();

        /// <inheritdoc />
        public void Dispose()
        {
            factory.Dispose();
        }

        [TestMethod]
        public async Task EuronextController_FetchOhlcvAsync_ValidParameters_ReturnsFetchedData()
        {
            var fixture = new TestFixture();
            var getRequest = new HttpRequestMessage(
                HttpMethod.Get,
                "/api/v1/data/historical/online/euronext/ohlcv?instrument.SecurityId=FR0010533075&instrument.SecurityIdSource=isin&instrument.Type=stock&instrument.Exchange.Mic=xpar");

            var response = await fixture.Client.SendAsync(getRequest).ConfigureAwait(false);

            // response.EnsureSuccessStatusCode()
            var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            Assert.IsNotNull(responseString);
            Assert.IsTrue(responseString.Contains("high", StringComparison.Ordinal));
        }

        [TestMethod]
        public async Task EuronextController_FetchOhlcvAsync_ValidParameters_ReturnsFetchedData2()
        {
            var fixture = new TestFixture();
            using var server = new TestServer(fixture.WebHostBuilder);

            var response = await server
                .CreateRequest("/api/v1/data/historical/online/euronext/ohlcv?instrument.SecurityId=FR0010533075&instrument.SecurityIdSource=isin&instrument.Type=stock&instrument.Exchange.Mic=xpar")
                .SendAsync("GET").ConfigureAwait(false);
            var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            Assert.IsNotNull(responseString);
            Assert.IsTrue(responseString.Contains("high", StringComparison.Ordinal));
        }

        [TestMethod]
        public async Task EuronextController_FetchOhlcvAsync_ValidParameters_ReturnsFetchedData3()
        {
            var client = factory.CreateClient();
            var response = await client.GetAsync("/api/v1/data/historical/online/euronext/ohlcv?instrument.SecurityId=FR0010533075&instrument.SecurityIdSource=isin&instrument.Type=stock&instrument.Exchange.Mic=xpar").ConfigureAwait(false);
            var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            Assert.IsNotNull(responseString);
            Assert.IsTrue(responseString.Contains("high", StringComparison.Ordinal));
        }

        [TestMethod]
        public async Task EuronextController_FetchOhlcvAsync_ValidParameters_ReturnsFetchedData4()
        {
            static void ConfigureTestServices(IServiceCollection services) =>
                services.AddSingleton<IEuronextHistoricalDataService>(new MockEuronextHistoricalDataService());
            var client = factory
                .WithWebHostBuilder(builder =>
                    builder.ConfigureTestServices(ConfigureTestServices))
                .CreateClient();

            var response = await client.GetAsync("/api/v1/data/historical/online/euronext/ohlcv?instrument.SecurityId=FR0010533075&instrument.SecurityIdSource=isin&instrument.Type=stock&instrument.Exchange.Mic=xpar").ConfigureAwait(false);
            var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            Assert.IsNotNull(responseString);
            Assert.IsTrue(responseString.Contains("high", StringComparison.Ordinal));
        }
    }
}
