using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
        public ReactiveProperty<StatusViewModel> InReplyViewModel { get; }
        public ReactiveCollection<AccountViewModel> SelectedAccounts { get; }
        public ReactiveProperty<string> ErrorMessage { get; }
        public AsyncReactiveCommand<object> SendStatusCommand { get; }
        public ReactiveCommand ClearInReplyCommand { get; }

        public PostAreaPageViewModel(GlobalNotifier globalNotifier, IAccountService accountService)
        {
            var history = new History<string>(2);
            var sender = new StatusSender();
            Accounts = accountService.Accounts.ToReadOnlyReactiveCollection(w => new AccountViewModel(w));
            SelectedAccounts = new ReactiveCollection<AccountViewModel>();
            FirstSelectedAccount = Accounts.Select((w, i) => new {Value = w, Index = i}).SingleOrDefault(w => w.Value.Account.IsMarkAsDefault)?.Index ?? -1;
            StatusBody = new ReactiveProperty<string>();
            StatusBody.Subscribe(w => history.Store(w)).AddTo(this);
            InReplyViewModel = globalNotifier.ObserveProperty(w => w.InReplyStatus)
                                             .Select(w => w != null ? new StatusViewModel(w) : null)
                                             .ToReactiveProperty();
            ErrorMessage = sender.ErrorMessages
                                 .CollectionChangedAsObservable()
                                 .Select(w => w.Action == NotifyCollectionChangedAction.Reset ? null : string.Join(Environment.NewLine, sender.ErrorMessages))
                                 .ToReactiveProperty();
            //ErrorMessage.Where(w => !string.IsNullOrWhiteSpace(w)).Delay(TimeSpan.FromSeconds(10)).Subscribe(_ => ErrorMessage.Value = "").AddTo(this);
            SendStatusCommand = new[]
            {
                StatusBody.Select(w => w?.TrimEnd('\n', '\r')).Select(w => !string.IsNullOrEmpty(w) && w.Length <= 500),
                SelectedAccounts.CollectionChangedAsObservable().Select(w => SelectedAccounts.Count > 0)
            }.CombineLatestValuesAreAllTrue().ToAsyncReactiveCommand();
            SendStatusCommand.Subscribe(async w =>
            {
                var body = history[(string) w == "ENTER" ? -1 : 0];
                if (globalNotifier.InReplyStatus != null)
                    await sender.SendReplyAsync(globalNotifier.InReplyTimeline.Account, body, globalNotifier.InReplyStatus.Id);
                else
                    await sender.SendAsync(SelectedAccounts.Select(v => v.Account).ToList(), body);
            }).AddTo(this);
            ClearInReplyCommand = new ReactiveCommand();
            ClearInReplyCommand.Subscribe(_ => globalNotifier.ClearInReply()).AddTo(this);
            globalNotifier.ObserveProperty(w => w.InReplyStatus).Where(w => w != null)
                          .Subscribe(w => StatusBody.Value = $"@{w.User.ScreenName} ")
                          .AddTo(this);
            sender.ObserveProperty(w => w.HasErrorMessages).Where(w => !w).Subscribe(_ =>
            {
                StatusBody.Value = null;
                globalNotifier.ClearInReply();
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