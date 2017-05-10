using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

using Orion.UWP.Models;
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
        public ObservableCollection<TimelineTypeViewModel> Timelines { get; }
        public ReactiveProperty<AccountViewModel> SelectedAccount { get; }
        public ReactiveProperty<TimelineTypeViewModel> SelectedTimeline { get; }
        public ReactiveProperty<bool> IsEnableOkCommand { get; }
        public AsyncReactiveCommand OkCommand { get; }

        public AddTimelineDialogViewModel(IAccountService accountService, ITimelineService timelineService)
        {
            Accounts = accountService.Accounts.ToReadOnlyReactiveCollection(w => new AccountViewModel(w));
            Timelines = new ObservableCollection<TimelineTypeViewModel>();
            SelectedAccount = new ReactiveProperty<AccountViewModel>();
            SelectedAccount.Where(w => w != null).Subscribe(w =>
            {
                Timelines.Clear();
                foreach (var timelineType in w.Account.Provider.GetSupportTimelineTypes())
                    Timelines.Add(new TimelineTypeViewModel(timelineType));
            }).AddTo(this);
            SelectedTimeline = new ReactiveProperty<TimelineTypeViewModel>();
            IsEnableOkCommand = new[]
            {
                SelectedAccount.Select(w => w != null),
                SelectedTimeline.Select(w => w != null)
            }.CombineLatestValuesAreAllTrue().ToReactiveProperty();
            OkCommand = new AsyncReactiveCommand();
            OkCommand.Subscribe(async () =>
            {
                var account = SelectedAccount.Value.Account;
                await timelineService.AddAsync(new Timeline {Account = account, AccountId = account.Id, TimelineType = SelectedTimeline.Value.TimelineType});
            });
        }
    }
}