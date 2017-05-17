using Orion.Shared.Absorb.Enums;

namespace Orion.Shared.Absorb.Objects
{
    /// <summary>
    ///     Follow, Favorite, Reblog and other events
    /// </summary>
    public class Event : StatusBase
    {
        public EventType EventType { get; set; }

        /// <summary>
        ///     Event message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     Target status
        /// </summary>
        public Status TargetStatus { get; set; }

        /// <summary>
        ///     Target user
        /// </summary>
        public User TargetUser { get; set; }
    }
}