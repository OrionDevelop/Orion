using Newtonsoft.Json;

namespace Orion.Service.GnuSocial.Models
{
    public class Geo
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("coordinates")]
        public double[] Coordinates { get; set; }
    }
}