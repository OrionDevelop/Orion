using Newtonsoft.Json;

namespace Orion.Service.Mastodon.Models
{
    public class Token
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("created_at")]
        //[JsonConverter(typeof(JavaScriptDateTimeConverter))]
        public long CreatedAt { get; set; }
    }
}