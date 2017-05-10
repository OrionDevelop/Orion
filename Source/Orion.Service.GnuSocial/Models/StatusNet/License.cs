using Newtonsoft.Json;

namespace Orion.Service.GnuSocial.Models.StatusNet
{
    public class License
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("owner")]
        public string Owner { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }
    }
}