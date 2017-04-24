using Newtonsoft.Json;

namespace Orion.Service.GnuSocial.Models
{
    public class Rights
    {
        [JsonProperty("delete_user")]
        public bool IsDeleteUser { get; set; }

        [JsonProperty("delete_others_notice")]
        public bool IsDeleteOthersNotice { get; set; }

        [JsonProperty("silence")]
        public bool IsSilence { get; set; }

        [JsonProperty("sandbox")]
        public bool IsSandbox { get; set; }
    }
}