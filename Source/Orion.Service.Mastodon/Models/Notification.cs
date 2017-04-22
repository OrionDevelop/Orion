using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using Orion.Service.Mastodon.Enum;

namespace Orion.Service.Mastodon.Models
{
    public class Notification
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public NotificationType Type { get; set; }

        [JsonProperty("created_at")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("account")]
        public Account Account { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }
    }
}