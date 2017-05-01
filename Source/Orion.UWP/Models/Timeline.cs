using Newtonsoft.Json;

using Orion.UWP.Models.Enum;

namespace Orion.UWP.Models
{
    public class Timeline
    {
        /// <summary>
        ///
        /// </summary>
        public string Id { get; set; }

        [JsonIgnore]
        public Account Account { get; set; }

        public TimelineType TimelineType { get; set; }
    }
}