using Orion.Shared.Enums;

namespace Orion.Shared.Models
{
    public class TimelinePreset
    {
        public string Name { get; set; }

        public string Query { get; set; }

        public bool IsEditable { get; set; } = false;

        public ProviderType ProviderType { get; set; }

        public Timeline CreateTimeline(Account account, string name = null, string query = null)
        {
            return IsEditable
                ? new Timeline {Name = name, Query = query, Account = account, AccountId = account.Id, IsEditable = IsEditable}
                : new Timeline {Name = Name, Query = Query, Account = account, AccountId = account.Id, IsEditable = IsEditable};
        }
    }
}