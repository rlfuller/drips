namespace Drips.Configuration
{
    internal class QATestConfig : ITestConfig
    {
        string ITestConfig.BaseUrl => "https://magento.softwaretestingboard.com/";

        string ITestConfig.Username => "jdoe@test.com";

        string ITestConfig.Password => "Test123$";
    }
}
