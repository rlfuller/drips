using Drips.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drips.Selenium.Pages
{
    internal class PageBase
    {
        protected IWebDriver driver;
        protected ITestConfig config = TestConfigFactory.CurrentEnvironmentTestConfig;


        [FindsBy(How = How.Id, Using = "search")]
        private IWebElement searchInput { get; set; }

        [FindsBy(How = How.CssSelector, Using = "header li.authorization-link > a")]
        private IWebElement signInLink { get; set; }

        [FindsBy(How = How.CssSelector, Using = "header .greet > .logged-in")]
        private IWebElement signedInMsg { get; set; }

        public PageBase(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public IWebElement WaitForElement(By by)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            return wait.Until(d => d.FindElement(by));
        }

        public IList<IWebElement> WaitForElements(By by)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            return wait.Until(d => {
                d.FindElement(by);
                return d.FindElements(by);
            });
        }

        public string WaitForText(By by, string text)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(by).Text.Contains(text));
            return driver.FindElement(by).Text;
        }

        public PageBase SearchForItem(string itemKeyword)
        {
            searchInput.Click();
            searchInput.SendKeys(itemKeyword);
            searchInput.SendKeys(Keys.Enter);

            return this;
        }

        public PageBase ClickSignInLink()
        {
            WaitForElement(By.CssSelector("header li.authorization-link > a"));
            signInLink.Click();
            return this;
        }

        public string WaitForSignInMessage(string text)
        {
            return WaitForText(By.CssSelector("header .greet > .logged-in"), text);
        }

        
    }
}
