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

        Dictionary<string, string> ShippingInfo
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    { "email", Username },
                    { "firstName", UserFirstName },
                    { "lastName", UserLastName },
                    { "streetAddress", "1 Main Street" },
                    { "city", "Charlotte" },
                    { "state", "North Carolina" },
                    { "zip", "28203" },
                    { "country", "United States" },
                    { "phone", "123456789" }
                };
            }
        }

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
