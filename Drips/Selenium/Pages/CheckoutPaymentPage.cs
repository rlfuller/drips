﻿using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Drips.Selenium.Pages
{
    internal class CheckoutPaymentPage : BasePage
    {
        [FindsBy(How = How.CssSelector, Using = ".actions-toolbar .checkout")]
        private IWebElement placeOrderButton { get; set;  }

        [FindsBy(How = How.CssSelector, Using = ".opc-block-summary")]
        private IWebElement orderSummary { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".opc-block-shipping-information")]
        private IWebElement shippingInformation { get; set; }
        
        public CheckoutPaymentPage(IWebDriver driver) : base(driver)
        {
        }

        public override void WaitUntilReady()
        {
            //wait for the order summary to appear before taking any actions on the page
            //indication that the elements are loaded
            WaitForElement(By.CssSelector(".shipping-information-content"));
        }

        public void ClickPlaceOrderButton()
        {
            ClickWhenClickable(placeOrderButton);
        }

        public string GetShippingInformation()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true)", shippingInformation);
            Thread.Sleep(150);
            return shippingInformation.Text;
        }
    }
}
