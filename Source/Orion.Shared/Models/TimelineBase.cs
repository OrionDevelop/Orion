using System;

using Newtonsoft.Json;

namespace Orion.Shared.Models
{
    public class TimelineBase
    {
        /// <summary>
        ///     ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     If set to `true`, user can edit timeline name and other properties (if given).
        /// </summary>
        public bool IsEditable { get; set; }

        /// <summary>
        ///     If set to `true`, this timeline don't save to storage for next session.
        /// </summary>
        [JsonIgnore]
        public bool IsInstant { get; set; } = true;

        /// <summary>
        ///     Account ID
        /// </summary>
        public string AccountId { get; set; }

        [JsonIgnore]
        public Account Account { get; set; }

        public TimelineBase()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}