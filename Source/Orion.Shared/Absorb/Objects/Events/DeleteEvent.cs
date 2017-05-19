using Orion.Service.Mastodon.Models.Streaming;
using Orion.Shared.Absorb.Enums;

namespace Orion.Shared.Absorb.Objects.Events
{
    /// <summary>
    ///     User deleted own status.
    /// </summary>
    public class DeleteEvent : EventBase
    {
        public DeleteEvent(DeleteMessage message)
        {
            Id = message.StatusId;
            Type = nameof(DeleteEvent);
            EventType = EventType.Delete;
        }
    }
}