using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Drips.Selenium.Pages
{
    internal class CustomerLoginPage : BasePage
    {
        [FindsBy(How = How.Id, Using = "email")]
        private IWebElement emailInput { get; set; }

        [FindsBy(How = How.Id, Using = "pass")]
        private IWebElement passwordInput { get; set; }

        [FindsBy(How = How.Id, Using = "send2")]
        private IWebElement signInButton { get; set; }

        public CustomerLoginPage(IWebDriver driver) : base(driver)
        {
        }

        public CustomerLoginPage EnterEmailAddress(string email)
        {
            emailInput.Click();
            emailInput.SendKeys(email);
            return this;
        }

        public CustomerLoginPage EnterPassword(string password)
        {
            passwordInput.Click();
            passwordInput.SendKeys(password);
            return this;
        }

        public CustomerLoginPage ClickSignInButton()
        {
            signInButton.Click();
            return this;
        }

        public string GetInvalidLoginMessage()
        {
            return WaitForElement(By.CssSelector("div.message-error")).Text;
        }
    }
}
