using Newtonsoft.Json;

namespace Orion.Service.GnuSocial.Models.StatusNet
{
    public class Group
    {
        [JsonProperty("desclimit")]
        public string DescLimit { get; set; }
    }
}