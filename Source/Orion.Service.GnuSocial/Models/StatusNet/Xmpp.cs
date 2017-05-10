using Newtonsoft.Json;

namespace Orion.Service.GnuSocial.Models.StatusNet
{
    public class Xmpp
    {
        [JsonProperty("enabled")]
        public bool IsEnabled { get; set; }

        [JsonProperty("server")]
        public bool Server { get; set; }

        [JsonProperty("port")]
        public bool Port { get; set; }

        [JsonProperty("user")]
        public bool User { get; set; }
    }
}