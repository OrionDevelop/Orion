using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

using Orion.UWP.Models;
using Orion.UWP.Models.Absorb;
using Orion.UWP.Models.Enum;
using Orion.UWP.Mvvm;

using Reactive.Bindings.Extensions;

namespace Orion.UWP.ViewModels.Contents
{
    public class TimelineViewModel : ViewModel
    {
        private readonly GlobalNotifier _globalNotifier;
        private readonly ObservableCollection<StatusBaseViewModel> _statuses;
        private readonly Timeline _timeline;
        private bool _isInitialized;
        public string Name => _timeline.TimelineType.ToName();
        public string User => _timeline.Account.Credential.Username;
        public string Icon => _timeline.TimelineType.ToIcon();

        public ReadOnlyObservableCollection<StatusBaseViewModel> Statuses
        {
            get
            {
                if (!_isInitialized)
                {
                    _isInitialized = true;
                    Initialize();
                }
                return new ReadOnlyObservableCollection<StatusBaseViewModel>(_statuses);
            }
        }

        public TimelineViewModel(GlobalNotifier globalNotifier, Timeline timeline)
        {
            _globalNotifier = globalNotifier;
            _timeline = timeline;
            _statuses = new ObservableCollection<StatusBaseViewModel>();
        }

        private void Initialize()
        {
            _timeline.Account.ClientWrapper.GetTimelineAsObservable(_timeline.TimelineType)
                     .ObserveOnUIDispatcher()
                     .Select(w =>
                     {
                         if (w.Type == StatusType.Status)
                             return new StatusViewModel(_globalNotifier, (Status) w) as StatusBaseViewModel;
                         return new NotificationViewModel(_globalNotifier, (Notification) w) as StatusBaseViewModel;
                     })
                     .Subscribe(w => _statuses.Insert(0, w))
                     .AddTo(this);
        }
    }
}