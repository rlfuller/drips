using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Drips.Selenium.Pages
{
    internal class OrderConfirmationSuccessPage : PageBase
    {
        [FindsBy(How = How.CssSelector, Using = ".checkout-success > p > span")]
        private IWebElement orderNumber { get; set; }
        
        public OrderConfirmationSuccessPage(IWebDriver driver) : base(driver)
        {
        }

        public string GetOrderNumber()
        {
            return orderNumber.Text;
        }
    }
}
