using Newtonsoft.Json;

namespace Orion.Service.Mastodon.Models
{
    public class RegistApp
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("redirect_uri")]
        public string RedirectUri { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }
    }
}