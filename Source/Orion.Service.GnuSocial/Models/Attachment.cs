using Newtonsoft.Json;

namespace Orion.Service.GnuSocial.Models
{
    public class Attachment
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("mimetype")]
        public string MimeType { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("oembed")]
        public bool IsOembed { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }
}