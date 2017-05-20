using Orion.Service.Mastodon.Models;
using Orion.Shared.Absorb.Enums;

namespace Orion.Shared.Absorb.Objects.Events
{
    /// <summary>
    ///     User favorited your status.
    /// </summary>
    public class FavoriteEvent : EventBase
    {
        public FavoriteEvent(Notification notification)
        {
            Id = notification.Id;
            CreatedAt = notification.CreatedAt;
            Source = new User(notification.Account);
            Target = new Status(notification.Status);
            Type = nameof(FavoriteEvent);
            EventType = EventType.Favorite;
        }
    }
}