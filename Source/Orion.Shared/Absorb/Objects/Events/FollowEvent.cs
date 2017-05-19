using Orion.Service.Mastodon.Models;
using Orion.Shared.Absorb.Enums;

namespace Orion.Shared.Absorb.Objects.Events
{
    /// <summary>
    ///     User followed you.
    /// </summary>
    public class FollowEvent : EventBase
    {
        public FollowEvent(Notification notification)
        {
            Id = notification.Id;
            CreatedAt = notification.CreatedAt;
            Source = new User(notification.Account);
            Type = nameof(FollowEvent);
            EventType = EventType.Follow;
        }
    }
}