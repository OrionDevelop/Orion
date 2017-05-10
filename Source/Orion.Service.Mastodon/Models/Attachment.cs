using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using Orion.Service.Mastodon.Enum;

namespace Orion.Service.Mastodon.Models
{
    public class Attachment
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public AttachmentType Type { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("remote_url")]
        public string RemoteUrl { get; set; }

        [JsonProperty("preview_url")]
        public string PreviewUrl { get; set; }

        [JsonProperty("text_url")]
        public string TextUrl { get; set; }
    }
}