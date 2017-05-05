using System;
using System.Diagnostics;

using Newtonsoft.Json;

using Orion.Service.Mastodon.Enum;

namespace Orion.Service.Mastodon.Models.Streaming
{
    public class MessageBase
    {
        public string Event { get; set; }
        public string Payload { get; set; }
        public MessageType Type { get; internal set; }

        internal static MessageBase CreateMessage(string eventLine, string payloadLine)
        {
            try
            {
                var @event = eventLine.Substring(eventLine.IndexOf(':') + 2);
                var payload = payloadLine.Substring(payloadLine.IndexOf(':') + 2);
                if (@event == "update")
                    return new StatusMessage {Status = JsonConvert.DeserializeObject<Status>(payload)};
                if (@event == "notification")
                    return new NotificationMessage {Notification = JsonConvert.DeserializeObject<Notification>(payload)};
                if (@event == "delete")
                    return new DeleteMessage {StatusId = long.Parse(payload)};
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            throw new NotSupportedException();
        }
    }
}