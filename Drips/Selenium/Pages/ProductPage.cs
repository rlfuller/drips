using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.Xml.Linq;

namespace Drips.Selenium.Pages
{
    internal class ProductPage : BasePage
    {

        [FindsBy(How = How.Id, Using = "qty")]
        private IWebElement quantityInput { get; set; }

        [FindsBy(How = How.Id, Using = "qty-error")]
        private IWebElement quantityError { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".page-title")]
        private IWebElement itemName { get; set;  }

        public ProductPage(IWebDriver driver) : base(driver)
        {
        }

        public override void WaitUntilReady()
        {
            WaitForElement(By.CssSelector("h1.page-title"));
        }

        public ProductPage ClickAddToCartButton()
        {
            var el = WaitForElement(By.Id("product-addtocart-button"));
            ClickWhenClickable(el);
            return this;
        }

        public string GetProductName()
        {
            return itemName.Text;
        }
    }
}
