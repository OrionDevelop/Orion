using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

using Orion.UWP.Models;
using Orion.UWP.Mvvm;
using Orion.UWP.Services.Interfaces;
using Orion.UWP.ViewModels.Contents;

using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace Orion.UWP.ViewModels.Partials
{
    public class PostAreaViewModel : ViewModel
    {
        private readonly History<string> _history;
        public ReadOnlyObservableCollection<AccountViewModel> Accounts { get; }
        public ReactiveProperty<string> StatusBody { get; }
        public ReactiveCollection<AccountViewModel> SelectedAccounts { get; }
        public AsyncReactiveCommand SendStatusCommand { get; }

        public PostAreaViewModel(IAccountService accountService)
        {
            _history = new History<string>(2);
            Accounts = accountService.Accounts.ToReadOnlyReactiveCollection(w => new AccountViewModel(w));
            SelectedAccounts = new ReactiveCollection<AccountViewModel>();
            StatusBody = new ReactiveProperty<string>();
            StatusBody.Subscribe(w => { _history.Store(w); }).AddTo(this);
            SendStatusCommand = new[]
            {
                StatusBody.Select(w => w?.TrimEnd('\n', '\r')).Select(w => !string.IsNullOrEmpty(w) && w.Length <= 500),
                SelectedAccounts.CollectionChangedAsObservable().Select(w => SelectedAccounts.Count > 0)
            }.CombineLatestValuesAreAllTrue().ToAsyncReactiveCommand();
            SendStatusCommand.Subscribe(async () =>
            {
                foreach (var account in SelectedAccounts)
                    await account.Account.ClientWrapper.UpdateAsync(_history[-1]);
                StatusBody.Value = null;
            });
        }
    }
}