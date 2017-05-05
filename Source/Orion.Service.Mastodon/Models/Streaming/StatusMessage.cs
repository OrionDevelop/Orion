using Orion.Service.Mastodon.Enum;

namespace Orion.Service.Mastodon.Models.Streaming
{
    public class StatusMessage : MessageBase
    {
        public Status Status { get; set; }

        public StatusMessage()
        {
            Type = MessageType.Update;
        }
    }
}