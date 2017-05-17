using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Windows.Storage;

using Newtonsoft.Json;

using Orion.Shared;
using Orion.UWP.Services.Interfaces;

namespace Orion.UWP.Services
{
    internal class TimelineService : ITimelineService
    {
        private readonly IAccountService _accountService;
        private readonly ObservableCollection<Timeline> _timelines;

        public TimelineService(IAccountService accountService)
        {
            _accountService = accountService;
            _timelines = new ObservableCollection<Timeline>();
            Timelines = new ReadOnlyObservableCollection<Timeline>(_timelines);
        }

        public ReadOnlyObservableCollection<Timeline> Timelines { get; }

        public async Task InitializeAsync()
        {
            var defaultAccount = _accountService.Accounts.Single(w => w.MarkAsDefault);
            foreach (var timeline in defaultAccount.DefaultTimelines())
                _timelines.Add(new Timeline {Account = defaultAccount, AccountId = defaultAccount.Id, TimelineType = timeline});

            await SaveAsync();
        }

        public async Task ClearAsync()
        {
            _timelines.Clear();
            await SaveAsync();
        }

        public Task RestoreAsync()
        {
            _timelines.Clear();

            var roamingSettings = ApplicationData.Current.RoamingSettings;
            var timelines = JsonConvert.DeserializeObject<List<Timeline>>(roamingSettings.Values["Orion.Timeline"] as string);

            foreach (var timeline in timelines)
            {
                timeline.Account = _accountService.Accounts.Single(w => w.Id == timeline.AccountId);
                _timelines.Add(timeline);
            }

            return Task.CompletedTask;
        }

        public Task SaveAsync()
        {
            var roamingSettings = ApplicationData.Current.RoamingSettings;
            roamingSettings.Values["Orion.Timeline"] = JsonConvert.SerializeObject(_timelines.ToList());

            return Task.CompletedTask;
        }

        public async Task AddAsync(Timeline timeline)
        {
            _timelines.Add(timeline);
            await SaveAsync();
        }

        public async Task RemoveAsync(Timeline timeline)
        {
            _timelines.Remove(timeline);
            await SaveAsync();
        }

        public async Task OrderAsync(Timeline timeline, int index)
        {
            var oldIndex = _timelines.IndexOf(timeline);
            _timelines.Move(oldIndex, index);
            await SaveAsync();
        }
    }
}