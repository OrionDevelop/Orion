using Orion.Service.Mastodon.Enum;

namespace Orion.Service.Mastodon.Models.Streaming
{
    public class NotificationMessage : MessageBase
    {
        public Notification Notification { get; set; }

        public NotificationMessage()
        {
            Type = MessageType.Notification;
        }
    }
}