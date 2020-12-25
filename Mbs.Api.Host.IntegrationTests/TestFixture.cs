using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

// ReSharper disable UnassignedGetOnlyAutoProperty
namespace Mbs.Api.Host.IntegrationTests
{
    internal class TestFixture
    {
        public TestFixture()
        {
            static string CalculateRelativeContentRootPath() =>
                Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "Mbs.Api.Host");

            WebHostBuilder = new WebHostBuilder()
                .UseContentRoot(CalculateRelativeContentRootPath())
                .UseEnvironment("Development")
                .UseStartup<Startup>();
        }

        public TestServer Server { get; }

        public HttpClient Client { get; }

        public IWebHostBuilder WebHostBuilder { get; }
    }
}
