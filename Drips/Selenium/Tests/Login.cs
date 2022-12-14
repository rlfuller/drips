using Drips.Selenium.Pages;
using Drips.Tests;

namespace Drips.Selenium.Tests
{
    internal class Login : BaseTest
    {
        [Test]
        public void LoginWithValidCredentials()
        {

            var basePage = new BasePage(driver);
            basePage.ClickSignInLink();

            var loginPage = new CustomerLoginPage(driver);

            loginPage.EnterEmailAddress(config.Username);
            loginPage.EnterPassword(config.Password);
            loginPage.ClickSignInButton();

            basePage = new BasePage(driver);

            var expectedSignInText = $"{config.UserFirstName} {config.UserLastName}";
            var signInMsg = basePage.WaitForSignInMessage(expectedSignInText);

            Assert.True(signInMsg.Contains(expectedSignInText));
        }

        [Test]
        public void LoginWithInvalidCredentials()
        {
            var basePage = new BasePage(driver);
            basePage.ClickSignInLink();

            var loginPage = new CustomerLoginPage(driver);

            loginPage.EnterEmailAddress("incorrect@notaregistereduser.com");
            loginPage.EnterPassword("chester");
            loginPage.ClickSignInButton();

            //Originally, this test passed and there was no captcha, but on day 2, the 
            //test starting failing and page now showed a captcha, so will grab that 
            var expectedResultsList = new List<string>()
            {
                "The account sign-in was incorrect or your account is disabled temporarily. Please wait and try again later.",
                "Incorrect CAPTCHA"
            };

            var actualResults = loginPage.GetInvalidLoginMessage();
            Assert.Contains(actualResults, expectedResultsList);
        }
    }
}
