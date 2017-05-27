using Orion.Shared.Absorb.Enums;

using MastodonDeleteMessage = Orion.Service.Mastodon.Models.Streaming.DeleteMessage;
using TwitterDeleteMessage = CoreTweet.Streaming.DeleteMessage;

namespace Orion.Shared.Absorb.Objects.Events
{
    /// <summary>
    ///     User deleted own status.
    /// </summary>
    public class DeleteEvent : EventBase
    {
        public DeleteEvent(MastodonDeleteMessage message)
        {
            Id = message.StatusId;
            Type = nameof(DeleteEvent);
            EventType = EventType.Delete;
            IsBroadcastStatus = true;
        }

        public DeleteEvent(TwitterDeleteMessage message)
        {
            Id = message.Id;
            Type = nameof(DeleteEvent);
            EventType = EventType.Delete;
            IsBroadcastStatus = true;
        }
    }
}