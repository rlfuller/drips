using Drips.Configuration;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drips.Selenium.Pages
{
    internal class BasePage
    {
        protected IWebDriver driver;
        protected ITestConfig config = TestConfigFactory.CurrentEnvironmentTestConfig;

        [FindsBy(How = How.Id, Using = "search")]
        private IWebElement searchInput { get; set; }

        [FindsBy(How = How.ClassName, Using = "counter-number")]
        private IWebElement cartQty { get; set; }

        [FindsBy(How = How.ClassName, Using = "showcart")]
        private IWebElement cartIcon { get; set; }

        [FindsBy(How = How.Id, Using = "top-cart-btn-checkout")]
        private IWebElement proceedToCheckoutButton { get; set; }

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            WaitUntilReady();
            PageFactory.InitElements(driver, this);
        }

        /// <summary>
        /// Virtual method to be overridden by subclasses to indicate when that page should be considered ready.
        /// </summary>
        public virtual void WaitUntilReady()
        {
            WaitForElement(By.Id("search"));
        }

        public IWebElement WaitForElement(By by)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            return wait.Until(d => d.FindElement(by));
        }

        public bool ClickWhenClickable(By by)
        {
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

        public string? WaitForText(By by, string text)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            return wait.Until(d =>
            {
                var textFound = d.FindElement(by).Text;
                if (textFound.Contains(text))
                {
                    return textFound;
                }
                return null;
            });
        }

        public IWebElement ScrollIntoView(IWebElement el)
        {
            //Scroll so the element is in view and can be interacted with
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true)", el);

            //Certain browsers (firefox) take extra before scroll completes
            Thread.Sleep(200);

            return el;
        }

        public BasePage SearchForItem(string itemKeyword)
        {
            searchInput.Click();
            searchInput.SendKeys(itemKeyword);
            searchInput.SendKeys(Keys.Enter);

            WaitForText(By.CssSelector("h1.page-title"), itemKeyword);

            return this;
        }

        public BasePage ClickSignInLink()
        {
            WaitForElement(By.CssSelector("header li.authorization-link > a")).Click();
            return this;
        }

        public string? WaitForSignInMessage(string text)
        {
            return WaitForText(By.CssSelector("header .greet > .logged-in"), text);
        }

        public BasePage ClickMenuItem(string menuText)
        {
            driver.FindElement(By.LinkText(menuText)).Click();
            return this;
        }

        public string GetCartQuantityLabel(string quantity)
        {
            WaitForText(By.ClassName("counter-number"), quantity);
            return cartQty.Text;
        }

        public BasePage ClickCartIcon()
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
