using Newtonsoft.Json;

namespace Orion.Service.GnuSocial.Models.StatusNet
{
    public class Notice
    {
        [JsonProperty("contentlimit")]
        public string ContentLimit { get; set; }
    }
}