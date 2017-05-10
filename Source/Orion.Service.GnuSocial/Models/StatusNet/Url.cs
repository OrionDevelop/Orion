using Newtonsoft.Json;

namespace Orion.Service.GnuSocial.Models.StatusNet
{
    public class Url
    {
        [JsonProperty("maxurllength")]
        public int MaxUrlLength { get; set; }

        [JsonProperty("maxnoticelength")]
        public int MaxNoticeLength { get; set; }
    }
}