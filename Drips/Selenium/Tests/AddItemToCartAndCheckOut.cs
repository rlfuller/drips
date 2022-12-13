using Drips.Selenium.Pages;
using Drips.Tests;
using OpenQA.Selenium;

namespace Drips.Selenium.Tests
{
    internal class AddItemToCartAndCheckOut : BaseTest
    {
        internal string ProductName;

        [Test]

        public void AddItemToCartAndCheckoutHappyPath()
        {

            var search = new PageBase(driver);
       
            search.SearchForItem("tote");

            
            var resultsPage = new SearchResultsPage(driver);
            resultsPage.WaitForText(By.CssSelector("h1.page-title"), "tote");
            resultsPage.SelectRandomItemAndClick();

            var productPage = new ProductPage(driver);

            ProductName = productPage.GetProductName();

            productPage.ClickAddToCartButton();

            //productPage.AddItemToCartViaQuantityInput();

            var basePage = new PageBase(driver);
            var expectedQtyInCart = "1";

            var actualQtyInCart = basePage.GetCartQuantityLabel();

            Assert.That(actualQtyInCart, Is.EqualTo(expectedQtyInCart));


            basePage.ClickCartIcon().ClickProceedToCheckoutButton();

            var checkoutShippingPage = new CheckoutShippingPage(driver);
            checkoutShippingPage.WaitForPageToLoad();




           // var loginPage = new CustomerLoginPage(driver);

          //  loginPage.EnterEmailAddress(config.Username);
          //  loginPage.EnterPassword(config.Password);
          //  loginPage.ClickSignInButton();

          //  basePage = new PageBase(driver);

         //   var expectedSignInText = $"{config.UserFirstName} {config.UserLastName}";
         //   var signInMsg = basePage.WaitForSignInMessage(expectedSignInText);

         //   Assert.True(signInMsg.Contains(expectedSignInText));
        }
    }
}
