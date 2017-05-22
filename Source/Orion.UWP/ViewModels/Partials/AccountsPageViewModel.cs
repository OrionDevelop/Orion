using System;

using Orion.UWP.Mvvm;
using Orion.UWP.Services.Interfaces;
using Orion.UWP.ViewModels.Contents;
using Orion.UWP.Views;

using Reactive.Bindings;

namespace Orion.UWP.ViewModels.Partials
{
    public class AccountsPageViewModel : ViewModel
    {
        public ReadOnlyReactiveCollection<AccountViewModel> Accounts { get; }
        public ReactiveCommand AddCommand { get; }

        public AccountsPageViewModel(IAccountService accountService, IDialogService dialogService, ITimelineService timelineService)
        {
            Accounts = accountService.Accounts.ToReadOnlyReactiveCollection(w => new AccountViewModel(w)).AddTo(this);
            AddCommand = new ReactiveCommand();
            AddCommand.Subscribe(async w => await dialogService.ShowDialogAsync<AuthorizationDialog>()).AddTo(this);
        }
    }
}