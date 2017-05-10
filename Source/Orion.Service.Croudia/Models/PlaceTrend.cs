using System;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Orion.Service.Croudia.Models
{
    public class PlaceTrend
    {
        [JsonProperty("as_of")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime AsOf { get; set; }

        [JsonProperty("created_at")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("locations")]
        public Location Locations { get; set; }

        [JsonProperty("trends")]
        public IEnumerable<Trend> Trends { get; set; }
    }
}