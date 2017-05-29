using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;

using Orion.UWP.Models;
using Orion.UWP.Mvvm;
using Orion.UWP.Services.Interfaces;
using Orion.UWP.ViewModels.Contents;

using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace Orion.UWP.ViewModels.Partials
{
    public class PostAreaPageViewModel : ViewModel
    {
        public ReadOnlyObservableCollection<AccountViewModel> Accounts { get; }
        public ReactiveProperty<string> StatusBody { get; }
        public ReactiveCollection<AccountViewModel> SelectedAccounts { get; }
        public AsyncReactiveCommand<object> SendStatusCommand { get; }

        public PostAreaPageViewModel(GlobalNotifier globalNotifier, IAccountService accountService)
        {
            var history = new History<string>(2);
            globalNotifier.ObserveProperty(w => w.InReplyStatus).Where(w => w != null)
                          .Subscribe(w => { StatusBody.Value = $"@{w.User.ScreenName} "; })
                          .AddTo(this);
            Accounts = accountService.Accounts.ToReadOnlyReactiveCollection(w => new AccountViewModel(w));
            SelectedAccounts = new ReactiveCollection<AccountViewModel>();
            FirstSelectedAccount = Accounts.Select((w, i) => new {Value = w, Index = i}).SingleOrDefault(w => w.Value.Account.IsMarkAsDefault)?.Index ?? -1;
            StatusBody = new ReactiveProperty<string>();
            StatusBody.Subscribe(w => history.Store(w)).AddTo(this);
            SendStatusCommand = new[]
            {
                StatusBody.Select(w => w?.TrimEnd('\n', '\r')).Select(w => !string.IsNullOrEmpty(w) && w.Length <= 500),
                SelectedAccounts.CollectionChangedAsObservable().Select(w => SelectedAccounts.Count > 0)
            }.CombineLatestValuesAreAllTrue().ToAsyncReactiveCommand();
            SendStatusCommand.Subscribe(async w =>
            {
                var accounts = SelectedAccounts.ToList();
                foreach (var account in accounts)
                    await account.Account.ClientWrapper.UpdateAsync(history[(string) w == "ENTER" ? -1 : 0], globalNotifier.InReplyStatus?.InReplyToStatusId);
                StatusBody.Value = null;
            }).AddTo(this);
        }

        #region FirstSelectedAccount

        private int _firstSelectedAccount;

        public int FirstSelectedAccount
        {
            get => _firstSelectedAccount;
            set => SetProperty(ref _firstSelectedAccount, value);
        }

        #endregion
    }
}