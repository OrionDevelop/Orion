using Newtonsoft.Json;

namespace Orion.Service.GnuSocial.Models.StatusNet
{
    // StatusNet instance configuration.

    public class Config
    {
        [JsonProperty("site")]
        public Site Site { get; set; }

        [JsonProperty("license")]
        public License License { get; set; }

        [JsonProperty("nickname")]
        public Nickname Nickname { get; set; }

        [JsonProperty("profile")]
        public Profile Profile { get; set; }

        [JsonProperty("group")]
        public Group Group { get; set; }

        [JsonProperty("notice")]
        public Notice Notice { get; set; }

        [JsonProperty("throttle")]
        public Throttle Throttle { get; set; }

        [JsonProperty("xmpp")]
        public Xmpp Xmpp { get; set; }

        [JsonProperty("integration")]
        public Integration Integration { get; set; }

        [JsonProperty("attachments")]
        public Attachments Attachments { get; set; }

        [JsonProperty("url")]
        public Url Url { get; set; }
    }
}