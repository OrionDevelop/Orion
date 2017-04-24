using Newtonsoft.Json;

namespace Orion.Service.GnuSocial.Models.StatusNet
{
    public class Integration
    {
        [JsonProperty("source")]
        public string Source { get; set; }
    }
}