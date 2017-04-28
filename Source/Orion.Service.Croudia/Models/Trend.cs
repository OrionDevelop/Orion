using Newtonsoft.Json;

namespace Orion.Service.Croudia.Models
{
    public class Trend
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("promoted_content")]
        public object PromotedContent { get; set; }
    }
}