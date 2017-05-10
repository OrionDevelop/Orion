using System.Collections.Generic;

using Newtonsoft.Json;

namespace Orion.Service.Croudia.Models
{
    public class StatusResults
    {
        [JsonProperty("statuses")]
        public IEnumerable<Status> Statuses { get; set; }

        [JsonProperty("search_metadata")]
        public SearchMetadata SearchMetadata { get; set; }
    }
}