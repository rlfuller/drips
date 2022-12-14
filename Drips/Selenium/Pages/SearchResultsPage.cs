using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.PageObjects;

namespace Drips.Selenium.Pages
{
    internal class SearchResultsPage : BasePage
    {

        [FindsBy(How = How.CssSelector, Using = "div.search.results")]
        private IWebElement searchResults { get; set; }

        [FindsBy(How = How.CssSelector, Using = "ol.product-items")]
        private IWebElement returnedResults { get; set; }

        [FindsBy(How = How.CssSelector, Using = "li.product-item")]
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
            return WaitForElements(By.CssSelector("li.product-item"));
        }

        public void SelectRandomItemAndClick(int remaining = 3)
        {
            try
            {
                //GetSearchResults returns a list of products from search. We are going to
                //click on a random item in the list
                var searchResults = GetSearchResults();
                int randomIndex = config.Random.Next(0, searchResults.Count);

                var item = searchResults[randomIndex];

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("arguments[0].scrollIntoView(true);", item);

                ClickWhenClickable(item);
            }
            catch (StaleElementReferenceException ex)
            {
                if (remaining > 0)
                {
                    SelectRandomItemAndClick(--remaining);
                }
                else
                {
                    throw ex;
                }
            }
        }
    }
}
