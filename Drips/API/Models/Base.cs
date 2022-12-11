using Newtonsoft.Json;

namespace Drips.API.Models
{
    internal class Base
    {
        [JsonProperty("support")]
        public Support Support { get; set; }
    }

    internal class Support
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

    }
}
