using System.Net.Http;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using UI;

namespace Tests.IntegrationTests.Util
{
    internal class BaseTestFixture
    {
        public BaseTestFixture()
        {
            var builder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<Startup>();

            TestServer = new TestServer(builder);
            Client = TestServer.CreateClient();
            ProductContext = TestServer.Host.Services.GetService<ProductContext>();

            FakeDbInitializer.Initialize(ProductContext);
        }

        public TestServer TestServer { get; set; }
        public ProductContext ProductContext { get; }
        public HttpClient Client { get; }

        public void Dispose()
        {
            Client.Dispose();
            TestServer.Dispose();
        }
    }
}