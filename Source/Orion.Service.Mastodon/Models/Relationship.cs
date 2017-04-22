using Newtonsoft.Json;

namespace Orion.Service.Mastodon.Models
{
    public class Relationship
    {
        [JsonProperty("following")]
        public bool IsFollowing { get; set; }

        [JsonProperty("followed_by")]
        public bool IsFollowedBy { get; set; }

        [JsonProperty("blocking")]
        public bool IsBlocking { get; set; }

        [JsonProperty("muting")]
        public bool IsMuting { get; set; }

        [JsonProperty("requested")]
        public bool IsRequested { get; set; }
    }
}