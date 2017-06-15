using Orion.Service.Mastodon.Models;
using Orion.Shared.Absorb.Enums;
using Orion.Shared.Enums;

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
            Provider = ProviderType.Twitter.ToString();
            EventType = EventType.Reblog;
        }
    }
}