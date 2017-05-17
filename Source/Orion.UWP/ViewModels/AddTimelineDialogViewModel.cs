using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;

using Orion.Shared;
using Orion.UWP.Mvvm;
using Orion.UWP.Services.Interfaces;
using Orion.UWP.ViewModels.Contents;

using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace Orion.UWP.ViewModels
{
    public class AddTimelineDialogViewModel : ViewModel
    {
        public ReadOnlyObservableCollection<AccountViewModel> Accounts { get; }
        public ObservableCollection<TimelinePresetViewModel> Timelines { get; }
        public ReactiveProperty<AccountViewModel> SelectedAccount { get; }
        public ReactiveProperty<TimelinePresetViewModel> SelectedTimeline { get; }
        public ReactiveProperty<bool> IsEnableOkCommand { get; }
        public AsyncReactiveCommand OkCommand { get; }

        public AddTimelineDialogViewModel(IAccountService accountService, ITimelineService timelineService)
        {
            Accounts = accountService.Accounts.ToReadOnlyReactiveCollection(w => new AccountViewModel(w));
            Timelines = new ObservableCollection<TimelinePresetViewModel>();
            SelectedAccount = new ReactiveProperty<AccountViewModel>();
            SelectedAccount.Where(w => w != null).Subscribe(w =>
            {
                Timelines.Clear();
                foreach (var timelineType in SharedConstants.TimelinePresets.Where(v => v.ProviderType == w.Account.Provider.ProviderType))
                    Timelines.Add(new TimelinePresetViewModel(timelineType));
            }).AddTo(this);
            SelectedTimeline = new ReactiveProperty<TimelinePresetViewModel>();
            IsEnableOkCommand = new[]
            {
                SelectedAccount.Select(w => w != null),
                SelectedTimeline.Select(w => w != null)
            }.CombineLatestValuesAreAllTrue().ToReactiveProperty();
            OkCommand = new AsyncReactiveCommand();
            OkCommand.Subscribe(async () =>
            {
                var account = SelectedAccount.Value.Account;
                await timelineService.AddAsync(SelectedTimeline.Value.TimelinePreset.CreateTimeline(account));
            });
        }
    }
}