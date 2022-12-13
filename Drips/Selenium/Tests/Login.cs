using Drips.Selenium.Pages;
using Drips.Tests;

namespace Drips.Selenium.Tests
{
    internal class Login : BaseTest
    {
        [Test]
        public void LoginWithValidCredentials()
        {

            var basePage = new PageBase(driver);
            basePage.ClickSignInLink();

            var loginPage = new CustomerLoginPage(driver);

            loginPage.EnterEmailAddress(config.Username);
            loginPage.EnterPassword(config.Password);
            loginPage.ClickSignInButton();

            basePage = new PageBase(driver);

            var expectedSignInText = $"{config.UserFirstName} {config.UserLastName}";
            var signInMsg = basePage.WaitForSignInMessage(expectedSignInText);

            Assert.True(signInMsg.Contains(expectedSignInText));
        }

        [Test]
        public void LoginWithInvalidCredentials()
        {
            var basePage = new PageBase(driver);
            basePage.ClickSignInLink();

            var loginPage = new CustomerLoginPage(driver);

            loginPage.EnterEmailAddress("incorrect@notaregistereduser.com");
            loginPage.EnterPassword("chester");
            loginPage.ClickSignInButton();

            var expectedResults = "The account sign-in was incorrect or your account is disabled temporarily. Please wait and try again later.";

            var actualResults = loginPage.GetInvalidLoginMessage();
            Assert.That(actualResults, Is.EqualTo(expectedResults));
        }
    }
}
