using Newtonsoft.Json;

namespace Orion.Service.Croudia.Models
{
    public class Entities
    {
        [JsonProperty("media")]
        public Media Media { get; set; }
    }
}