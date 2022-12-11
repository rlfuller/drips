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
        public int Page { get; set; }
        [JsonProperty("per_page")]
        public int PerPage { get; set; }
        [JsonProperty("total")]
        public int Total { get; set; }
        [JsonProperty("total_pages")]
        public int TotalPages {get; set; }

        [JsonProperty("data")]
        public List<User> users { get; set; }
    }

    internal class UserUpdateResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("job")]
        public string Job { get; set; }
        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }
    }

    internal class UserCreateResponse: UserUpdateResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }

    internal class LoginResponse
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }

    internal class RegisterResponse : LoginResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
