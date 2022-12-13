using Drips.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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

        [FindsBy(How = How.ClassName, Using = "counter-number")]
        private IWebElement cartQty { get; set; }

        [FindsBy(How = How.ClassName, Using = "showcart")]
        private IWebElement cartIcon { get; set; }

        [FindsBy(How = How.Id, Using = "top-cart-btn-checkout")]
        private IWebElement proceedToCheckoutButton { get; set; }

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

        public bool ClickWhenClickable(By by) {
            return ClickWhenClickable(driver.FindElement(by));
        }
        public bool ClickWhenClickable(IWebElement el)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            return wait.Until(d =>
            {
                try
                {
                    el.Click();
                    return true;
                }
                catch (ElementNotInteractableException) // also covers ElementClickInterceptedException, which is a direct subclass of ElementNotInteractableException
                {
                    return false;
                }
            });
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

            WaitForText(By.CssSelector("h1.page-title"), itemKeyword);

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

        public PageBase ClickMenuItem(string menuText)
        {
            driver.FindElement(By.LinkText(menuText)).Click();
            return this;
        }

        public string GetCartQuantityLabel()
        {
            WaitForText(By.ClassName("counter-number"), "1");
            return cartQty.Text;
        }

        public PageBase ClickCartIcon()
        {
            cartIcon.Click();
            return this;
        }

        public void ClickProceedToCheckoutButton()
        {
            proceedToCheckoutButton.Click();
        }
        
    }
}
