using Drips.Configuration;
using RestSharp;

namespace Drips.API.Tests
{
    internal class BaseTest
    {
        protected RestClient client;
        protected ITestConfig config = TestConfigFactory.CurrentEnvironmentTestConfig;

        [SetUp]
        public void Setup()
        {
            client = new RestClient(config.ApiBaseUrl);
        }

    }
}
