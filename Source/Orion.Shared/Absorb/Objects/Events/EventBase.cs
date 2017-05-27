using System;

using CoreTweet.Streaming;

using Orion.Service.Mastodon.Enum;
using Orion.Service.Mastodon.Models.Streaming;
using Orion.Shared.Absorb.Enums;

using MastodonMessage = Orion.Service.Mastodon.Models.Streaming.MessageBase;
using TwitterMessage = CoreTweet.Streaming.StreamingMessage;
using MastodonDeleteMessage = Orion.Service.Mastodon.Models.Streaming.DeleteMessage;
using TwitterDeleteMessage = CoreTweet.Streaming.DeleteMessage;
using MastodonMessageType = Orion.Service.Mastodon.Enum.MessageType;
using TwitterMessageType = CoreTweet.Streaming.MessageType;

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

        public EventBase()
        {
            Id = 0;
            IgnoreIdDuplication = true;
        }

        public static StatusBase CreateEventFromMessage(TwitterMessage message)
        {
            switch (message.Type)
            {
                case TwitterMessageType.DeleteStatus:
                    return new DeleteEvent(message as TwitterDeleteMessage);

                case TwitterMessageType.Event:
                    var msg = (EventMessage) message;
                    switch (msg.Event)
                    {
                        case EventCode.Favorite:
                            return new FavoriteEvent(msg);

                        case EventCode.Follow:
                            return new FollowEvent(msg);

                        default:
                            return null;
                    }

                default:
                    return null;
            }
        }

        public static StatusBase CreateEventFromMessage(MastodonMessage message)
        {
            if (message is ThumpMessage)
                return null;

            switch (message.Type)
            {
                case MastodonMessageType.Notification:
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

                case Service.Mastodon.Enum.MessageType.Delete:
                    return new DeleteEvent(message as MastodonDeleteMessage);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}