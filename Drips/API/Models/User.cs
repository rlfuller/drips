using Newtonsoft.Json;

namespace Drips.API.Models
{
    internal class User
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

    }

    internal class UserBody : Base
    {
        [JsonProperty("data")]
        public User user { get; set; }
        
    }

    internal class UserListBody : Base
    {
        [JsonProperty("page")]
        public int CurrentPageNumber { get; set; }

        [JsonProperty("per_page")]
        public int PerPage { get; set; }

        [JsonProperty("total")]
        public int TotalUsers { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPages {get; set; }

        [JsonProperty("data")]
        public List<User> Users { get; set; }
    }

    internal class UserUpdateResponseBody
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("job")]
        public string Job { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }
    }

    internal class UserCreateResponseBody: UserUpdateResponseBody
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }

    internal class LoginResponseBody
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }

    internal class RegisterResponseBody : LoginResponseBody
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
