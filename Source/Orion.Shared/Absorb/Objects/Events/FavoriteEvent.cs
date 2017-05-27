using CoreTweet.Streaming;

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
            CreatedAt = notification.CreatedAt;
            Source = new User(notification.Account);
            Target = new Status(notification.Status);
            Type = nameof(FavoriteEvent);
            EventType = EventType.Favorite;
        }

        public FavoriteEvent(EventMessage message)
        {
            CreatedAt = message.CreatedAt.ToLocalTime().LocalDateTime;
            Source = new User(message.Source);
            Target = new Status(message.TargetStatus);
            Type = nameof(FavoriteEvent);
            EventType = EventType.Favorite;
        }
    }
}