using System;

namespace Orion.UWP.Models.Absorb
{
    public class Notification : StatusBase
    {
        private readonly Service.Mastodon.Models.Notification _mastodonNotification;

        public NotificationType NotificationType { get; }
        public Status Status { get; }

        public Notification(Service.Mastodon.Models.Notification notification)
        {
            _mastodonNotification = notification;
            Type = StatusType.Notification;

            Id = notification.Id;
            CreatedAt = notification.CreatedAt;
            User = new User(notification.Account);
            Status = notification.Status != null ? new Status(notification.Status) : null;
            switch (notification.Type)
            {
                case Service.Mastodon.Enum.NotificationType.Mention:
                    NotificationType = NotificationType.Mention;
                    break;

                case Service.Mastodon.Enum.NotificationType.Reblog:
                    NotificationType = NotificationType.Reblogged;
                    break;

                case Service.Mastodon.Enum.NotificationType.Favourite:
                    NotificationType = NotificationType.Favorited;
                    break;

                case Service.Mastodon.Enum.NotificationType.Follow:
                    NotificationType = NotificationType.Followed;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}