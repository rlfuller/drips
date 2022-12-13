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


        [FindsBy(How = How.Id, Using = "search")]
        private IWebElement searchInput { get; set; }

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

        public PageBase SearchForItem(string itemKeyword)
        {
            searchInput.Click();
            searchInput.SendKeys(itemKeyword);
            searchInput.SendKeys(Keys.Enter);

            return this;
        }
    }
}
