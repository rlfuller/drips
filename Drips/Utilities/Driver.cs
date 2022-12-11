using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace Drips.Utilities
{
    /// <summary>
    /// Create a single instance of a webdriver, based on an environment variable
    /// </summary>
    public class Driver
    {
        private static IWebDriver webDriver { get; set; }

        public static IWebDriver GetInstance()
        {
            if (webDriver is not null)
            {
                return webDriver;
            }

            string? browser = System.Environment.GetEnvironmentVariable(Constants.AutomationBrowserVar);

            if (browser is null)
            {
                browser = "";
            }

            switch (browser.ToLower())
            {
                case "chrome":
                    webDriver = new ChromeDriver();
                    break;

                case "edge":
                    webDriver = new EdgeDriver();
                    break;

                default:
                    webDriver = new FirefoxDriver();
                    break;
            }

            return webDriver;
        }
    }
}
