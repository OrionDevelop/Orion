using System;

using Newtonsoft.Json;

using Orion.Shared.Absorb.Objects;
using Orion.Shared.Enums;

namespace Orion.UWP.Models
{
    public class Timeline
    {
        public string Id { get; set; }

        public string AccountId { get; set; }

        [JsonIgnore]
        public Account Account { get; set; }

        public TimelineType TimelineType { get; set; }

        public Timeline()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}