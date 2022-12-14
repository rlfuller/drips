namespace Drips.Configuration
{

    /// <summary>
    /// Used to store environment variables needed to run tests in QA enviornment
    /// </summary>
    internal class QATestConfig : ITestConfig
    {
        string ITestConfig.BaseUrl => "https://magento.softwaretestingboard.com/";

        string ITestConfig.Username => "jdoe@test.com";

        string ITestConfig.Password => "Test123$";

        string ITestConfig.UserFirstName => "Jane";
        string ITestConfig.UserLastName => "Doe";

        string ITestConfig.ApiBaseUrl => "https://reqres.in/api/";
    }
}
