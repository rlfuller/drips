using Drips.Configuration;
using Drips.Utilities;
using OpenQA.Selenium;

namespace Drips.Tests
{
    public class BaseTest
    {
        protected IWebDriver driver = Driver.GetInstance();
        protected ITestConfig config = TestConfigFactory.CurrentEnvironmentTestConfig;

        [SetUp]
        public void Setup()
        {
            driver = Driver.GetInstance();
            driver.Navigate().GoToUrl(config.BaseUrl);
        }

        [TearDown]
        public void Teardown()
        {
            driver.Close();
            driver.Quit();
        }
    }
}
