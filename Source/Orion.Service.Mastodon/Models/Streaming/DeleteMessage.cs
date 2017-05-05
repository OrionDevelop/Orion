using Orion.Service.Mastodon.Enum;

namespace Orion.Service.Mastodon.Models.Streaming
{
    public class DeleteMessage : MessageBase
    {
        public long StatusId { get; set; }

        public DeleteMessage()
        {
            Type = MessageType.Delete;
        }
    }
}