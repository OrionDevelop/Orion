using System;

using Newtonsoft.Json;

namespace Orion.Service.GnuSocial.Models
{
    public class Attention
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }

        [JsonProperty("fullname")]
        public string FullName { get; set; }

        [JsonProperty("profileurl")]
        public string ProfileUrl { get; set; }

        [JsonProperty("ostatus_uri")]
        public Uri OStatusUri { get; set; }
    }
}