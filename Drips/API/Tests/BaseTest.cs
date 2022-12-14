using Drips.Configuration;
using RestSharp;

namespace Drips.API.Tests
{
    /// <summary>
    /// Superclass for API tests. Creates rest client and configuration. 
    /// </summary>
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
