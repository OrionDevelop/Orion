using Orion.Service.Mastodon.Models;
using Orion.Shared.Absorb.Enums;

namespace Orion.Shared.Absorb.Objects.Events
{
    /// <summary>
    ///     User reblogged your status.
    /// </summary>
    public class ReblogEvent : EventBase
    {
        public ReblogEvent(Notification notification)
        {
            CreatedAt = notification.CreatedAt;
            Source = new User(notification.Account);
            Target = new Status(notification.Status);
            Type = nameof(ReblogEvent);
            EventType = EventType.Reblog;
        }
    }
}