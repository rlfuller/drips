using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Drips.Selenium.Pages
{
    internal class CheckoutShippingPage : PageBase
    {
        [FindsBy(How = How.Id, Using = "customer-email")]
        private IWebElement emailAddressInput { get; set; }

        [FindsBy(How = How.Id, Using = "SDLCV7S")]
        private IWebElement firstNameInput { get; set; }

        [FindsBy(How = How.Id, Using = "POIIDJV")]
        private IWebElement lastNameInput { get; set; }

        [FindsBy(How = How.Id, Using = "LE7QFRQ")]
        private IWebElement streetAddressInput { get; set; }

        [FindsBy(How = How.XPath, Using = ".//*[@name='city'")]
        private IWebElement cityInput { get; set; }

        [FindsBy(How = How.XPath, Using = ".//*[@name='region_id'")]
        private IWebElement stateProvinceSelect { get; set; }

        [FindsBy(How = How.XPath, Using = ".//*[@name='telephone'")]
        private IWebElement phoneInput { get; set; }

        private IWebElement shippingTableRateRadioButton { get; set; }

        [FindsBy(How = How.XPath, Using = ".//*[@data-role='opc-continue'")]
        private IWebElement nextButton { get; set; }

        [FindsBy(How = How.Id, Using = "SL1BM1V")]
        private IWebElement zipCodeInput { get; set; }

        public void WaitForPageToLoad()
        {
            WaitForElement(By.CssSelector(".opc-block-summary"));
        }

        public CheckoutShippingPage(IWebDriver driver) : base(driver)
        {
        }
    }
}
