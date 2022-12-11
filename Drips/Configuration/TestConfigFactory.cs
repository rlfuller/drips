namespace Drips.Configuration
{
    internal class TestConfigFactory
    {
        internal static ITestConfig CurrentEnvironmentTestConfig => CreateTestConfig(System.Environment.GetEnvironmentVariable(Constants.AutomationEnvironmentVar));

        static ITestConfig CreateTestConfig(string env)
        {

            switch (env)
            {
                case Constants.Stage:
                    return new StageTestConfig();

                case Constants.QA:
                    return new QATestConfig();

                default:
                    throw new ArgumentException($"Environment missing or incorrect: {env}");

            }
        }
    }
}
