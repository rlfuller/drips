using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.PageObjects;

namespace Drips.Selenium.Pages
{
    internal class CheckoutShippingPage : BasePage
    {
        [FindsBy(How = How.CssSelector, Using = "#checkout-step-shipping div > input#customer-email")]
        private IWebElement emailAddressInput { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[name='firstname']")]
        private IWebElement firstNameInput { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[name='lastname']")]
        private IWebElement lastNameInput { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[name='street[0]']")]
        private IWebElement streetAddressInput { get; set; }

        [FindsBy(How = How.XPath, Using = ".//*[@name='city']")]
        private IWebElement cityInput { get; set; }

        [FindsBy(How = How.XPath, Using = ".//*[@name='region_id']")]
        private IWebElement stateProvinceSelect { get; set; }

        [FindsBy(How = How.XPath, Using = ".//*[@name='telephone']")]
        private IWebElement phoneInput { get; set; }

        [FindsBy(How = How.Id, Using = "label_carrier_flatrate_flatrate")]
        private IWebElement shippingFlatRateRadioButton { get; set; }

        [FindsBy(How = How.XPath, Using = ".//*[@data-role='opc-continue']")]
        private IWebElement nextButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[name='postcode']")]
        private IWebElement zipCodeInput { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".product-item-name")]
        private IWebElement productName { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".items-in-cart")]
        private IWebElement orderSummary { get; set; }

        public override void WaitUntilReady()
        {
            WaitForElement(By.CssSelector(".product-item-name"));
            WaitForElement(By.Id("shipping"));
        }

        public CheckoutShippingPage(IWebDriver driver) : base(driver)
        {
        }

        public CheckoutShippingPage EnterShippingInformation()
        {
            emailAddressInput.SendKeys(config.ShippingInfo["email"]);
            firstNameInput.SendKeys(config.ShippingInfo["firstName"]);
            lastNameInput.SendKeys(config.ShippingInfo["lastName"]);
            streetAddressInput.SendKeys(config.ShippingInfo["streetAddress"]);
            cityInput.SendKeys(config.ShippingInfo["city"]);
            ClickWhenClickable(ScrollIntoView(stateProvinceSelect));
            stateProvinceSelect.SendKeys(config.ShippingInfo["state"]);
            zipCodeInput.SendKeys(config.ShippingInfo["zip"]);
            phoneInput.SendKeys(config.ShippingInfo["phone"]);
            ClickWhenClickable(shippingFlatRateRadioButton);

            return this;
        }

        public void ClickNextButton()
        {
            nextButton.Click();
        }

        public string GetProductName()
        {
            return WaitForElement(By.CssSelector(".product-item-name")).Text;
        }

        public CheckoutShippingPage ToggleOrderSummary()
        {
            ClickWhenClickable(orderSummary);
            return this;
        }
    }
}
