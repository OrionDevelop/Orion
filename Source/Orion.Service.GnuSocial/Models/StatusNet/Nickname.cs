using System.Collections.Generic;

using Newtonsoft.Json;

namespace Orion.Service.GnuSocial.Models.StatusNet
{
    public class Nickname
    {
        [JsonProperty("featured")]
        public List<string> Featured { get; set; }
    }
}