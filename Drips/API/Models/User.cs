using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
