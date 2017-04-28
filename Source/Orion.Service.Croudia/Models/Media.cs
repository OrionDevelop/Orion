using Newtonsoft.Json;

namespace Orion.Service.Croudia.Models
{
    public class Media
    {
        [JsonProperty("media_url_https")]
        public string MediaUrl { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}