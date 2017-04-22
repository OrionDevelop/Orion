using Newtonsoft.Json;

namespace Orion.Service.Mastodon.Models
{
    public class Mention
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("acct")]
        public string Acct { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }
    }
}