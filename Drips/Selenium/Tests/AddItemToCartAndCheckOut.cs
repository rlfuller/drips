using Drips.Selenium.Pages;
using Drips.Tests;
using OpenQA.Selenium;

namespace Drips.Selenium.Tests
{
    internal class AddItemToCartAndCheckOut : BaseTest
    {
        [Test]
        public void AddItemToCartAndCheckoutHappyPath()
        {
            //Main page of the site
            string productName;
            var search = new BasePage(driver);
       
            search.SearchForItem("tote");
            
            //Results appear from search
            var resultsPage = new SearchResultsPage(driver);
            resultsPage.WaitForText(By.CssSelector("h1.page-title"), "tote");
            resultsPage.SelectRandomItemAndClick();

            //On the products page
            var productPage = new ProductPage(driver);

            //Grab the name of the product we've selected for verification in checkout
            productName = productPage.GetProductName();
            productPage.ClickAddToCartButton();

            //Check cart showing correct quantity
            var basePage = new BasePage(driver);
            var expectedQtyInCart = "1";

            var actualQtyInCart = basePage.GetCartQuantityLabel(expectedQtyInCart);

            Assert.That(actualQtyInCart, Is.EqualTo(expectedQtyInCart));

            basePage.ClickCartIcon().ClickProceedToCheckoutButton();

            //On the shipping page
            var checkoutShippingPage = new CheckoutShippingPage(driver);

            //validate our item in cart is correct
            var actualProduct = checkoutShippingPage
                .ToggleOrderSummary()
                .GetProductName();
            Assert.That(actualProduct, Is.EqualTo(productName));

            checkoutShippingPage.EnterShippingInformation();
            checkoutShippingPage.ClickNextButton();

            //On the payment page
            var checkoutPaymentPage = new CheckoutPaymentPage(driver);

            //validate that the shipping information we previous entered is correct
            var actualShippingInfo = checkoutPaymentPage.GetShippingInformation();
            Assert.True(actualShippingInfo.Contains(config.ShippingInfo["firstName"]));
            Assert.True(actualShippingInfo.Contains(config.ShippingInfo["lastName"]));
            Assert.True(actualShippingInfo.Contains(config.ShippingInfo["streetAddress"]));
            Assert.True(actualShippingInfo.Contains(config.ShippingInfo["city"]));
            Assert.True(actualShippingInfo.Contains(config.ShippingInfo["zip"]));
            Assert.True(actualShippingInfo.Contains(config.ShippingInfo["phone"]));

            checkoutPaymentPage.ClickPlaceOrderButton();

            //On the confirmation page
            var orderConfirmation = new OrderConfirmationSuccessPage(driver);

            //verify the page is Success
            Assert.True(orderConfirmation.GetPageTitle().Contains("Thank you"));
        }
    }
}
