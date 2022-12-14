using Drips.Configuration;
using Drips.Selenium;
using OpenQA.Selenium;

namespace Drips.Tests
{
    public class BaseTest
    {
        protected IWebDriver driver;
        protected ITestConfig config = TestConfigFactory.CurrentEnvironmentTestConfig;

        [SetUp]
        public void Setup()
        {
            driver = Driver.Create();
            driver.Navigate().GoToUrl(config.BaseUrl);
            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void Teardown()
        {
            driver.Close();
        }

        [OneTimeTearDown]
        public void TearDownOnce()
        {
            driver.Quit();
        }
    }
}
