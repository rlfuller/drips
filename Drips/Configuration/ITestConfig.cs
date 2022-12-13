namespace Drips.Configuration
{
    public interface ITestConfig
    {
        string BaseUrl { get; }

        /// <summary>
        /// Pseudo random number 'Seed' so we can make all randomness reproducible.
        /// </summary>
        long Seed
        {
            get
            {
                var seed = System.Environment.GetEnvironmentVariable("Seed");
                try
                {
                    return long.Parse(seed);
                }
                catch
                {
                    return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
                }
                
            }
        }

        string Username { get; }
        string Password { get; }
        string UserFirstName { get; }
        string UserLastName { get; }

        string ApiBaseUrl { get; }

        Random Random {
            get
            {
                string? seed = Environment.GetEnvironmentVariable("Seed");
                try
                {
                    return new Random(int.Parse(seed));
                }
                catch
                {
                    return new Random();
                }

            }
        }

    }
}
