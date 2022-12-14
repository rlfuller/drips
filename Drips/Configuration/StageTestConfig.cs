namespace Drips.Configuration
{
    /// <summary>
    /// Used to store environment variables for a Stage Environment
    /// </summary>
    internal class StageTestConfig : ITestConfig
    {
        string ITestConfig.BaseUrl => "https://stage.magento.softwaretestingboard.com/";

        string ITestConfig.Username => "stageusername@test.com";

        string ITestConfig.Password => "Test123$";
        string ITestConfig.UserFirstName => "StageUserFirstName";
        string ITestConfig.UserLastName => "StageUserLastName";

        string ITestConfig.ApiBaseUrl => "https://stage.reqres.in/api/";

    }
}
