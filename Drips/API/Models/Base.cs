using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
