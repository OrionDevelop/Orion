using System;
using System.Linq;

using Orion.UWP.Mvvm;
using Orion.UWP.Services.Interfaces;
using Orion.UWP.ViewModels.Contents;
using Orion.UWP.Views;

using Reactive.Bindings;

using AuthorizationDialog = Orion.UWP.Views.Dialogs.AuthorizationDialog;

namespace Orion.UWP.ViewModels.Partials
{
    public class AccountsPageViewModel : ViewModel
    {
        public ReadOnlyReactiveCollection<AccountViewModel> Accounts { get; }
        public ReactiveProperty<AccountViewModel> SelectedAccount { get; }
        public ReactiveCommand AddCommand { get; }
        public ReactiveCommand RemoveCommand { get; }

        public AccountsPageViewModel(IAccountService accountService, IDialogService dialogService, ITimelineService timelineService)
        {
            Accounts = accountService.Accounts.ToReadOnlyReactiveCollection(w => new AccountViewModel(w)).AddTo(this);
            SelectedAccount = new ReactiveProperty<AccountViewModel>();
            AddCommand = new ReactiveCommand();
            AddCommand.Subscribe(async w => await dialogService.ShowDialogAsync<AuthorizationDialog>()).AddTo(this);
            RemoveCommand = new ReactiveCommand();
            RemoveCommand.Subscribe(async _ =>
            {
                var removes = timelineService.Timelines.Where(w => w.AccountId == SelectedAccount.Value.Account.Id).ToList();
                foreach (var timeline in removes)
                    await timelineService.RemoveAsync(timeline);
                await accountService.DeleteAsync(SelectedAccount.Value.Account);
            });
        }
    }
}