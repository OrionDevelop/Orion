using Newtonsoft.Json;

namespace Orion.Service.Croudia.Models
{
    public class Location
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("woeid")]
        public int WoeId { get; set; }
    }
}