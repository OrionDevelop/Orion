using Newtonsoft.Json;

namespace Orion.Service.GnuSocial.Models.StatusNet
{
    public class Profile
    {
        [JsonProperty("biolimit")]
        public string BioLimit { get; set; }
    }
}