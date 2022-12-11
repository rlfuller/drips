using Drips.Configuration;
using Drips.Selenium;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
