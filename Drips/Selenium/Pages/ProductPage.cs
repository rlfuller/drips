using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.Xml.Linq;

namespace Drips.Selenium.Pages
{
    internal class ProductPage : PageBase
    {

        [FindsBy(How = How.Id, Using = "qty")]
        private IWebElement quantityInput { get; set; }

        [FindsBy(How = How.Id, Using = "qty-error")]
        private IWebElement quantityError { get; set; }

        [FindsBy(How = How.Id, Using = "product-addtocart-button")]
        private IWebElement addToCartButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".page-title")]
        private IWebElement itemName { get; set;  }

        [FindsBy(How = How.XPath, Using = "//*[contains(@role, 'alert')]")]
        private IWebElement itemAddedToCartAlert { get; set; }


        public ProductPage(IWebDriver driver) : base(driver)
        {
            WaitForElement(By.CssSelector("h1.page-title"));
        }
        
        public ProductPage AddQuantity(string qty)
        {
            quantityInput.Click();
            quantityInput.SendKeys(qty);

            return this;
        }

        //default is 1
        public ProductPage AddItemToCartViaQuantityInput()
        {
            quantityInput.Click();
            quantityInput.SendKeys(Keys.Enter);
            Thread.Sleep(5000);
            WaitForElement(By.XPath("//*[contains(@role, 'alert')]"));

            return this;
        }

        public ProductPage ClickAddToCartButton()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", addToCartButton);
            //js.ExecuteScript("arguments[0].click();", addToCartButton);
            ClickWhenClickable(addToCartButton);
            return this;
        }

        public string GetQtyErrorMessage()
        {
            return quantityError.Text;
        }

        public string GetProductName()
        {
            return itemName.Text;
        }
    }
}
