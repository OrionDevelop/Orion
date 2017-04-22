using System.Collections.Generic;

using Newtonsoft.Json;

namespace Orion.Service.Mastodon.Models
{
    public class Context
    {
        [JsonProperty("ancestors")]
        public List<Status> Ancestors { get; set; }

        [JsonProperty("descendants")]
        public List<Status> Descendants { get; set; }
    }
}