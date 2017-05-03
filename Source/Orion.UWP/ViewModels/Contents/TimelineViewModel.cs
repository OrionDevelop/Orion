using System;
using System.Collections.ObjectModel;

using Orion.UWP.Models;
using Orion.UWP.Models.Enum;
using Orion.UWP.Mvvm;

using Reactive.Bindings.Extensions;

namespace Orion.UWP.ViewModels.Contents
{
    public class TimelineViewModel : ViewModel
    {
        private readonly ObservableCollection<StatusViewModel> _statuses;
        private readonly Timeline _timeline;
        private bool _isInitialized;
        public string Name => _timeline.TimelineType.ToName();
        public string User => _timeline.Account.Credential.Username;
        public string Icon => _timeline.TimelineType.ToIcon();

        public ReadOnlyObservableCollection<StatusViewModel> Statuses
        {
            get
            {
                if (!_isInitialized)
                {
                    _isInitialized = true;
                    Initialize();
                }
                return new ReadOnlyObservableCollection<StatusViewModel>(_statuses);
            }
        }

        public TimelineViewModel(Timeline timeline)
        {
            _timeline = timeline;
            _statuses = new ObservableCollection<StatusViewModel>();
        }

        private void Initialize()
        {
            if (_timeline.TimelineType != TimelineType.HomeTimeline)
                return;

            _timeline.Account.ClientWrapper.GetTimelineAsObservable(_timeline.TimelineType)
                     .ObserveOnUIDispatcher()
                     .Subscribe(w => _statuses.Insert(0, new StatusViewModel(w)))
                     .AddTo(this);
        }
    }
}