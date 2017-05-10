using Newtonsoft.Json;

namespace Orion.Service.Croudia.Models
{
    public class Source
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}