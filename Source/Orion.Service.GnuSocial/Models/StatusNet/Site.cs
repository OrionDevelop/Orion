using Newtonsoft.Json;

namespace Orion.Service.GnuSocial.Models.StatusNet
{
    public class Site
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("server")]
        public string Server { get; set; }

        [JsonProperty("theme")]
        public string Theme { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }

        [JsonProperty("fancy")]
        public bool IsFancy { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("broughtby")]
        public string BroughtBy { get; set; }

        [JsonProperty("broughtbyurl")]
        public string BroughtByUrl { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("closed")]
        public bool IsClosed { get; set; }

        [JsonProperty("inviteonly")]
        public bool IsInviteOnly { get; set; }

        [JsonProperty("private")]
        public bool IsPrivate { get; set; }

        [JsonProperty("textlimit")]
        public string TextLimit { get; set; }

        [JsonProperty("ssl")]
        public string Ssl { get; set; }

        [JsonProperty("sslserver")]
        public string SslServer { get; set; }
    }
}