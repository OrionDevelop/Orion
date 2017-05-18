using System;

using Newtonsoft.Json;

using Orion.Scripting;
using Orion.Scripting.Parsing;
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
            return Account.ClientWrapper.CreateOrGetConnection(Filter.SourceStr);
        }

        public void Disconnect()
        {
            Account.ClientWrapper.Disconnect(Filter.SourceStr);
        }
    }
}