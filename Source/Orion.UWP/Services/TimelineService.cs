using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Windows.Storage;

using Newtonsoft.Json;

using Orion.Shared.Models;
using Orion.UWP.Services.Interfaces;

namespace Orion.UWP.Services
{
    internal class TimelineService : ITimelineService
    {
        private readonly IAccountService _accountService;
        private readonly ObservableCollection<TimelineBase> _timelines;

        public TimelineService(IAccountService accountService)
        {
            _accountService = accountService;
            _timelines = new ObservableCollection<TimelineBase>();
            Timelines = new ReadOnlyObservableCollection<TimelineBase>(_timelines);
        }

        public ReadOnlyObservableCollection<TimelineBase> Timelines { get; }

        public async Task InitializeAsync()
        {
            var defaultAccount = _accountService.Accounts.Single(w => w.IsMarkAsDefault);
            foreach (var preset in defaultAccount.DefaultTimelines())
                _timelines.Add(preset.CreateTimeline(defaultAccount));

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
            var jsonSettings = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All};
            var timelines = JsonConvert.DeserializeObject<List<TimelineBase>>(roamingSettings.Values["Orion.Timeline"] as string, jsonSettings);

            foreach (var timeline in timelines)
            {
                var account = _accountService.Accounts.SingleOrDefault(w => w.Id == timeline.AccountId);
                if (account == null)
                {
                    Debug.WriteLine($"Timeline \"{timeline.Name}\" (ID: {timeline.Id}) is removed.");
                    Debug.WriteLine("Reason: Account credentials is invalid or unknown account.");
                    continue;
                }
                timeline.Account = account;
                _timelines.Add(timeline);
            }

            return Task.CompletedTask;
        }

        public Task SaveAsync()
        {
            var roamingSettings = ApplicationData.Current.RoamingSettings;
            var jsonSettings = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All};
            roamingSettings.Values["Orion.Timeline"] = JsonConvert.SerializeObject(_timelines.Where(w => !w.IsInstant).ToList(), jsonSettings);

            return Task.CompletedTask;
        }

        public async Task AddAsync(TimelineBase timeline)
        {
            _timelines.Add(timeline);
            await SaveAsync();
        }

        public async Task RemoveAsync(TimelineBase timeline)
        {
            _timelines.Remove(timeline);
            await SaveAsync();
        }

        public async Task OrderAsync(TimelineBase timeline, int index)
        {
            var oldIndex = _timelines.IndexOf(timeline);
            _timelines.Move(oldIndex, index);
            await SaveAsync();
        }
    }
}