using Orion.Shared.Enums;

namespace Orion.Shared.Models
{
    public class TimelinePreset
    {
        public string Name { get; set; }

        public string Query { get; set; }

        public ProviderType ProviderType { get; set; }

        public Timeline CreateTimeline(Account account)
        {
            return new Timeline {Name = Name, Query = Query, Account = account, AccountId = account.Id};
        }
    }
}