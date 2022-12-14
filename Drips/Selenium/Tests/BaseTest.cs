using Drips.Configuration;
using Drips.Selenium;
using OpenQA.Selenium;

namespace Drips.Tests
{
    /// <summary>
    /// Superclass inherited by all Selenium tests. This class will setup the driver as well as create a configuration object based on the environment. It will also handle setup and teardown actions that are common to all tests.
    /// </summary>
    public class BaseTest
    {
        protected IWebDriver driver;
        protected ITestConfig config = TestConfigFactory.CurrentEnvironmentTestConfig;

        [SetUp]
        public void Setup()
        {
            driver = Driver.CurrentEnvironmentWebDriver;
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
