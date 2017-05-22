using System;

using Orion.Service.Mastodon.Enum;
using Orion.Service.Mastodon.Models.Streaming;
using Orion.Shared.Absorb.Enums;

using MastodonMessage = Orion.Service.Mastodon.Models.Streaming.MessageBase;

namespace Orion.Shared.Absorb.Objects.Events
{
    /// <summary>
    ///     Follow, Favorite, Reblog and other events
    /// </summary>
    public abstract class EventBase : StatusBase
    {
        public User Source
        {
            get => User;
            set => User = value;
        }

        public Status Target { get; set; }

        public EventType EventType { get; set; }

        public static StatusBase CreateEventFromMessage(MastodonMessage message)
        {
            if (message is ThumpMessage)
                return null;

            switch (message.Type)
            {
                case MessageType.Notification:
                    var msg = (NotificationMessage) message;
                    switch (msg.Notification.Type)
                    {
                        case NotificationType.Mention:
                            return new Status(msg.Notification.Status);

                        case NotificationType.Reblog:
                            return new ReblogEvent(msg.Notification);

                        case NotificationType.Favourite:
                            return new FavoriteEvent(msg.Notification);

                        case NotificationType.Follow:
                            return new FollowEvent(msg.Notification);

                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                case MessageType.Delete:
                    return new DeleteEvent(message as DeleteMessage);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}