using Newtonsoft.Json;

namespace Orion.Service.Mastodon.Models
{
    public class Instance
    {
        [JsonProperty("uri")]
        public string Url { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}