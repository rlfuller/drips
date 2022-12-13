using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Drips.Selenium.Pages
{
    internal class CheckoutPaymentPage : PageBase
    {
        [FindsBy(How = How.CssSelector, Using = ".actions-toolbar > .primary > button")]
        private IWebElement placeOrderButton { get; set;  }

        [FindsBy(How = How.CssSelector, Using = ".actions-toolbar")]
        private IWebElement productName { get; set; }

        public CheckoutPaymentPage(IWebDriver driver) : base(driver)
        {
        }

        public string GetProductName()
        {
            return productName.Text;
        }

        public void ClickPlaceOrderButton()
        {
            placeOrderButton.Click();
        }
    }
}
