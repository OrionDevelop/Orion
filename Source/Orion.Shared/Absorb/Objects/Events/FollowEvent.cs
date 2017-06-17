using CoreTweet.Streaming;

using Orion.Service.Mastodon.Models;
using Orion.Shared.Absorb.Enums;
using Orion.Shared.Enums;

namespace Orion.Shared.Absorb.Objects.Events
{
    /// <summary>
    ///     User followed you.
    /// </summary>
    public class FollowEvent : EventBase
    {
        public FollowEvent(Notification notification)
        {
            CreatedAt = notification.CreatedAt;
            Source = new User(notification.Account);
            Type = nameof(FollowEvent);
            Provider = ProviderType.Mastodon.ToString();
            EventType = EventType.Follow;
        }

        public FollowEvent(EventMessage message)
        {
            CreatedAt = message.CreatedAt.ToLocalTime().LocalDateTime;
            Source = new User(message.Source);
            Type = nameof(FollowEvent);
            Provider = ProviderType.Twitter.ToString();
            EventType = EventType.Follow;
        }
    }
}