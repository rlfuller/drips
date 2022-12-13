using Drips.Configuration;
using Drips.Selenium;
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
