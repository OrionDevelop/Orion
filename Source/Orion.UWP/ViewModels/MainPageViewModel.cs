using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;

using Microsoft.Toolkit.Uwp.UI.Controls;

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

        public MainPageViewModel(IAccountService accountService, IDialogService dialogService, ITimelineService timelineService)
        {
            _accountService = accountService;
            _dialogService = dialogService;
            _timelineService = timelineService;

            SelectedTimeline = new ReactiveProperty<TimelineViewModel>();
            SelectedTimeline.Where(w => w != null).Subscribe(w => { Debug.WriteLine(w); }).AddTo(this);
            SelectedOptions = new ReactiveProperty<HamburgerMenuGlyphItem>();
            SelectedOptions.Where(w => w != null).Subscribe(w => _dialogService.ShowDialogAsync(w.TargetPageType)).AddTo(this);
            Timelines = _timelineService.Timelines.ToReadOnlyReactiveCollection(w => new TimelineViewModel(w));
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);

            if (_accountService.Accounts.Count == 0)
                _dialogService.ShowDialogAsync<AuthorizationDialog>();
            else
                _timelineService.RestoreAsync();
        }
    }
}