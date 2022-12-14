namespace Drips.Configuration
{
    internal class TestConfigFactory
    {
        /// <summary>
        /// Based on an AutomationEnvironment variable that should be setup on the machine on which this project runs.
        /// </summary>
        internal static ITestConfig CurrentEnvironmentTestConfig => CreateTestConfig(System.Environment.GetEnvironmentVariable(Constants.AutomationEnvironmentVar));

        /// <summary>
        /// Creates a configuration object depending on which environment we want to test.  Values are QA, Stage
        /// </summary>
        /// <param name="env">String representing the environment</param>
        /// <returns>config object</returns>
        /// <exception cref="ArgumentException"></exception>
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
