using System.Collections.Generic;

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

        public MainPageViewModel(IAccountService accountService, IDialogService dialogService, ITimelineService timelineService)
        {
            _accountService = accountService;
            _dialogService = dialogService;
            _timelineService = timelineService;

            Timelines = _timelineService.Timelines.ToReadOnlyReactiveCollection(w => new TimelineViewModel(w));
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);

            if (_accountService.Accounts.Count == 0)
                _dialogService.ShowDialogAsync(typeof(AuthorizationDialog));
            else
                _timelineService.RestoreAsync();
        }
    }
}