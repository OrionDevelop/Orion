using System;

namespace Orion.Shared.Absorb.Objects
{
    public class StatusBase
    {
        /// <summary>
        ///     ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     Created at
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        ///     User
        /// </summary>
        public User User { get; set; }

        /// <summary>
        ///     Type
        /// </summary>
        public StatusType Type { get; set; }
    }
}