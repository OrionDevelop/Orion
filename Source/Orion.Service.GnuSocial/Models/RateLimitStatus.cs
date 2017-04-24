using System;

using Newtonsoft.Json;

namespace Orion.Service.GnuSocial.Models
{
    public class RateLimitStatus
    {
        [JsonProperty("reset_time_in_seconds")]
        public int ResetTimeInSeconds { get; set; }

        [JsonProperty("remaining_hits")]
        public int RemainingHits { get; set; }

        [JsonProperty("hourly_limit")]
        public int HourlyLimit { get; set; }

        [JsonProperty("reset_time")]
        public DateTime ResetTime { get; set; }
    }
}