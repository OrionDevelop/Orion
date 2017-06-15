using CoreTweet.Streaming;

using Orion.Service.Mastodon.Models;
using Orion.Shared.Absorb.Enums;
using Orion.Shared.Enums;

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
            Provider = ProviderType.Mastodon.ToString();
            Type = nameof(FavoriteEvent);
            EventType = EventType.Favorite;
        }

        public FavoriteEvent(EventMessage message)
        {
            CreatedAt = message.CreatedAt.ToLocalTime().LocalDateTime;
            Source = new User(message.Source);
            Target = new Status(message.TargetStatus);
            Provider = ProviderType.Twitter.ToString();
            Type = nameof(FavoriteEvent);
            EventType = EventType.Favorite;
        }
    }
}