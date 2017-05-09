using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;

using Microsoft.Toolkit.Uwp.UI.Controls;

using Orion.UWP.Models;
using Orion.UWP.Mvvm;
using Orion.UWP.Services.Interfaces;
using Orion.UWP.ViewModels.Contents;
using Orion.UWP.Views;

using Prism.Windows.Navigation;

using Reactive.Bindings;

namespace Orion.UWP.ViewModels
{
    public class MainPageViewModel : ViewModel
    {
        private readonly IAccountService _accountService;
        private readonly IDialogService _dialogService;
        private readonly ITimelineService _timelineService;

        public ReadOnlyReactiveCollection<TimelineViewModel> Timelines { get; }
        public ReactiveProperty<TimelineViewModel> SelectedTimeline { get; }
        public ReactiveProperty<HamburgerMenuGlyphItem> SelectedOptions { get; }

        public MainPageViewModel(GlobalNotifier globalNotifier, IAccountService accountService, IDialogService dialogService, ITimelineService timelineService)
        {
            _accountService = accountService;
            _dialogService = dialogService;
            _timelineService = timelineService;

            SelectedTimeline = new ReactiveProperty<TimelineViewModel>();
            SelectedTimeline.Where(w => w != null).Subscribe(w => { Debug.WriteLine(w); }).AddTo(this);
            SelectedOptions = new ReactiveProperty<HamburgerMenuGlyphItem>();
            SelectedOptions.Where(w => w != null).Subscribe(w => _dialogService.ShowDialogAsync(w.TargetPageType)).AddTo(this);
            Timelines = _timelineService.Timelines.ToReadOnlyReactiveCollection(w => new TimelineViewModel(globalNotifier, w));
        }

        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);

            if (_accountService.Accounts.Count == 0)
                await _dialogService.ShowDialogAsync<AuthorizationDialog>();
            else
                await _timelineService.RestoreAsync();
            DefaultAccount = new AccountViewModel(_accountService.Accounts.First(w => w.MarkAsDefault));
        }

        #region DefaultAccount

        private AccountViewModel _defaultAccount;

        public AccountViewModel DefaultAccount
        {
            get => _defaultAccount;
            set => SetProperty(ref _defaultAccount, value);
        }

        #endregion
    }
}