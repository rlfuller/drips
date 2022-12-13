using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V106.DOM;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drips.Selenium.Pages
{
    internal class SearchResultsPage : PageBase
    {

        [FindsBy(How = How.CssSelector, Using = "div.search.results")]
        private IWebElement searchResults { get; set; }

        [FindsBy(How = How.CssSelector, Using = "ol.product-items")]
        private IWebElement returnedResults { get; set; }

        [FindsBy(How = How.CssSelector, Using = "li.item.product.product-item")]
        private IList<IWebElement> allItemsFromSearch { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#maincontent div.message.notice")]
        private IWebElement noReturnedResults { get; set; }


        public SearchResultsPage(IWebDriver driver) : base(driver)
        {
        }
        

        public string GetMessage()
        {
            return WaitForElement(By.CssSelector("#maincontent div.message.notice")).Text;
        }

        public IList<IWebElement> GetSearchResults()
        {
            return WaitForElements(By.CssSelector("li.item.product.product-item"));
        }
    }
}
