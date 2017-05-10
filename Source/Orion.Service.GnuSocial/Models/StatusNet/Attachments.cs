using Newtonsoft.Json;

namespace Orion.Service.GnuSocial.Models.StatusNet
{
    public class Attachments
    {
        [JsonProperty("uploads")]
        public bool CanUpload { get; set; }

        [JsonProperty("file_quote")]
        public int FileQuota { get; set; }
    }
}