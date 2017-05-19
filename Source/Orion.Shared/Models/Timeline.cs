using System;

using Newtonsoft.Json;

using Orion.Scripting;
using Orion.Scripting.Parsing;
using Orion.Shared.Absorb.Extensions;
using Orion.Shared.Absorb.Objects;

namespace Orion.Shared.Models
{
    public class Timeline
    {
        /// <summary>
        ///     Querystring
        /// </summary>
        [JsonIgnore]
        private string _query;

        /// <summary>
        ///     ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Name
        /// </summary>
        public string Name { get; set; }

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

        /// <summary>
        ///     Editable timeline?
        /// </summary>
        public bool IsEditable { get; set; } = false;

        /// <summary>
        ///     Account ID
        /// </summary>
        public string AccountId { get; set; }

        [JsonIgnore]
        public Account Account { get; set; }

        public Timeline()
        {
            Id = Guid.NewGuid().ToString();
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