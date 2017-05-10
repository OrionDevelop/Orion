using System.Collections.Generic;

using Newtonsoft.Json;

namespace Orion.Service.Croudia.Models
{
    public class Relation
    {
        [JsonProperty("id_str")]
        public string IdStr { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }

        [JsonProperty("connection")]
        public IEnumerable<string> Connection { get; set; }

        [JsonProperty("following")]
        public bool IsFollowing { get; set; }

        [JsonProperty("followed_by")]
        public bool IsFollowedBy { get; set; }

        [JsonProperty("muting")]
        public bool? IsMuting { get; set; }

        [JsonProperty("blocking")]
        public bool? IsBlocking { get; set; }
    }
}