using System;

using Newtonsoft.Json;

namespace Orion.Shared.Models
{
    public class Timeline
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
        ///     Querystring
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        ///     Editable timeline?
        /// </summary>
        public bool IsEditable { get; set; } = false;

        /// <summary>
        ///     Account ID
        /// </summary>
        public string AccountId { get; set; }

        [JsonIgnore]
        public Account Account { get; set; }

        public Timeline()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}