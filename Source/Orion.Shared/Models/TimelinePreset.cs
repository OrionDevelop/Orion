using Orion.Shared.Enums;

namespace Orion.Shared.Models
{
    public class TimelinePreset
    {
        public string Name { get; set; }

        public string Query { get; set; }

        public bool IsEditable { get; set; } = false;

        public ProviderType ProviderType { get; set; }

        public StatusesTimeline CreateTimeline(Account account, string name = null, string query = null)
        {
            return IsEditable
                ? new StatusesTimeline {Name = name, Query = query, Account = account, AccountId = account.Id, IsEditable = IsEditable}
                : new StatusesTimeline {Name = Name, Query = Query, Account = account, AccountId = account.Id, IsEditable = IsEditable};
        }
    }
}