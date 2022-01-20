using System.Net.Http;
using DAL;
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
            UserContext = TestServer.Host.Services.GetService<UserContext>();

            FakeDbInitializer.Initialize(UserContext);
        }

        public TestServer TestServer { get; set; }
        public UserContext UserContext { get; }
        public HttpClient Client { get; }

        public void Dispose()
        {
            Client.Dispose();
            TestServer.Dispose();
        }
    }
}