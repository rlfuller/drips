using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace Drips.Selenium
{
    /// <summary>
    /// Create a single instance of a webdriver, based on an environment variable
    /// </summary>
    public class Driver
    {
        public static IWebDriver CurrentEnvironmentWebDriver => Create(Environment.GetEnvironmentVariable(Constants.AutomationBrowserVar));

        public static IWebDriver Create(string? browser)
        {
            IWebDriver webDriver;

            if (browser is null)
            {
                browser = "";
            }

            switch (browser.ToLower())
            {
                case "firefox":
                    webDriver = new FirefoxDriver();
                    break;

                case "edge":
                    webDriver = new EdgeDriver();
                    break;

                default:
                    webDriver = new ChromeDriver();
                    break;
            }

            return webDriver;
        }
    }
}
