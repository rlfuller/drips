using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace Drips.Selenium.Pages
{
    internal class OrderConfirmationSuccessPage : BasePage
    {
        [FindsBy(How = How.CssSelector, Using = ".page-title")]
        private IWebElement pageTitle { get; set; }
        
        public OrderConfirmationSuccessPage(IWebDriver driver) : base(driver)
        {
        }

        public override void WaitUntilReady()
        {
            WaitForText(By.CssSelector(".page-title"), "Thank you for your purchase!");
        }

        public string GetPageTitle()
        {
            return pageTitle.Text;
        }
    }
}
