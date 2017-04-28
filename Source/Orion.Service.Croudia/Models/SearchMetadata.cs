using Newtonsoft.Json;

namespace Orion.Service.Croudia.Models
{
    public class SearchMetadata
    {
        [JsonProperty("completed_in")]
        public double CompletedIn { get; set; }

        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("max_id")]
        public int MaxId { get; set; }

        [JsonProperty("max_id_str")]
        public string MaxIdStr { get; set; }

        [JsonProperty("since_id")]
        public int SinceId { get; set; }

        [JsonProperty("since_id_str")]
        public string SinceIdMax { get; set; }

        [JsonProperty("next_results")]
        public string NextResults { get; set; }

        [JsonProperty("refresh_url")]
        public string RefreshUrl { get; set; }
    }
}