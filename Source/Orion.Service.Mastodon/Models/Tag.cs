using Newtonsoft.Json;

namespace Orion.Service.Mastodon.Models
{
    public class Tag
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}