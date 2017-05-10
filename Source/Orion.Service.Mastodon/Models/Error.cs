using Newtonsoft.Json;

namespace Orion.Service.Mastodon.Models
{
    public class Error
    {
        [JsonProperty("error")]
        public string ErrorDesc { get; set; }
    }
}