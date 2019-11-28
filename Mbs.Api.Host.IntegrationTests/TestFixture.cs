using System.IO;
using System.Net.Http;
//using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;

namespace Mbs.Api.Host.IntegrationTests
{
    internal class TestFixture
    {
        // ReSharper disable once UnassignedGetOnlyAutoProperty
        public TestServer Server { get; }

        // ReSharper disable once UnassignedGetOnlyAutoProperty
        public HttpClient Client { get; }

        public IWebHostBuilder WebHostBuilder { get; }

        public TestFixture()
        {
            static string CalculateRelativeContentRootPath() =>
                Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "Mbs.Api.Host");

            // var s = CalculateRelativeContentRootPath();
            // var b = Directory.Exists(s);
            WebHostBuilder = new WebHostBuilder()
                .UseContentRoot(CalculateRelativeContentRootPath())
                .UseEnvironment("Development")

                // .UseUrls("http://localhost:51327")
                    /*.Configure(app =>
                    {
                        app.Run(async context =>
                        {
                            await context.Response.WriteAsync("Test response").ConfigureAwait(false);
                        });
                    });*/
                .UseStartup<Startup>();

            // Server = new TestServer(WebHostBuilder);
            // Client = Server.CreateClient();
        }
    }
}
