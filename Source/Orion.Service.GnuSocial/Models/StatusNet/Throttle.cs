using Newtonsoft.Json;

namespace Orion.Service.GnuSocial.Models.StatusNet
{
    public class Throttle
    {
        [JsonProperty("enabled")]
        public bool IsEnabled { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("timespam")]
        public int Timespan { get; set; }
    }
}