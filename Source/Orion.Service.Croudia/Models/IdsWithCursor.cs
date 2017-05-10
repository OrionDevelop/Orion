using System.Collections.Generic;

using Newtonsoft.Json;

namespace Orion.Service.Croudia.Models
{
    public class IdsWithCursor
    {
        [JsonProperty("next_cursor")]
        public int NextCursor { get; set; }

        [JsonProperty("ids")]
        public IEnumerable<int> Ids { get; set; }

        [JsonProperty("next_cursor_str")]
        public string NextCursorStr { get; set; }

        [JsonProperty("previous_cursor")]
        public int PreviousCursor { get; set; }

        [JsonProperty("previous_cursor_str")]
        public string PreviousCursorStr { get; set; }
    }
}