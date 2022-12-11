using Drips.Configuration;
using Drips.Selenium;
using OpenQA.Selenium;

namespace Drips.Tests
{
    public class BaseTest
    {
        protected IWebDriver driver = Selenium.Driver.GetInstance();
        protected ITestConfig config = TestConfigFactory.CurrentEnvironmentTestConfig;

        [SetUp]
        public void Setup()
        {
            driver = Selenium.Driver.GetInstance();
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
