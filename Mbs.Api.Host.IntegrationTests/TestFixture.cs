using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.PlatformAbstractions;

namespace Mbs.Api.Host.IntegrationTests
{
    internal class TestFixture
    {
        public TestServer Server { get; }

        public HttpClient Client { get; }

        public IWebHostBuilder WebHostBuilder { get; }

        public TestFixture()
        {
            string CalculateRelativeContentRootPath() =>
                Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "..", "..", "..", "..", "Mbs.Api.Host");

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
