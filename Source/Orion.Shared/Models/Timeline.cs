using System;

using Newtonsoft.Json;

using Orion.Scripting;
using Orion.Scripting.Parsing;
using Orion.Shared.Absorb.Extensions;
using Orion.Shared.Absorb.Objects;

namespace Orion.Shared.Models
{
    public class StatusesTimeline : TimelineBase
    {
        /// <summary>
        ///     Querystring
        /// </summary>
        [JsonIgnore]
        private string _query;

        public string Query
        {
            get => _query;
            set
            {
                _query = value;
                // TODO: Type-based
                Filter = QueryCompiler.Compile<Status>(value);
            }
        }

        [JsonIgnore]
        public FilterQuery Filter { get; set; }

        public StatusesTimeline()
        {
            IsInstant = false;
        }

        public IObservable<StatusBase> GetAsObservable()
        {
            var connection = Account.ClientWrapper.CreateOrGetConnection(Filter.SourceStr);
            switch (Filter.SourceStr)
            {
                case "mention":
                case "mentions":
                    return connection.AsMentions(Account);

                case "notification":
                case "notifications":
                    return connection.AsNotifications();

                case "message":
                case "messages":
                    return connection.AsMessages();

                default:
                    return connection.AsStatus();
            }
        }

        public void Disconnect()
        {
            Account.ClientWrapper.Disconnect(Filter.SourceStr);
        }
    }
}