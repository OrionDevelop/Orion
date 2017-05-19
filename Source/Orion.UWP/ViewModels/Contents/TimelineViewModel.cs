using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

using Orion.Shared.Absorb.Objects;
using Orion.Shared.Absorb.Objects.Events;
using Orion.Shared.Models;
using Orion.UWP.Extensions;
using Orion.UWP.Models;
using Orion.UWP.Mvvm;
using Orion.UWP.Services.Interfaces;

using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace Orion.UWP.ViewModels.Contents
{
    public class TimelineViewModel : ViewModel
    {
        private readonly GlobalNotifier _globalNotifier;
        private readonly ObservableCollection<StatusBaseViewModel> _statuses;
        private readonly Timeline _timeline;
        private bool _isInitialized;
        public string Name => _timeline.Name;
        public string User => _timeline.Account.Credential.User.ScreenNameWithHost;
        public string Icon => _timeline.ToIcon();
        public ReactiveCommand ClearCommand { get; }
        public ReactiveCommand DeleteCommand { get; }

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

        public TimelineViewModel(GlobalNotifier globalNotifier, ITimelineService timelineService, Timeline timeline)
        {
            _globalNotifier = globalNotifier;
            _timeline = timeline;
            _statuses = new ObservableCollection<StatusBaseViewModel>();
            ClearCommand = new ReactiveCommand();
            ClearCommand.Subscribe(w => _statuses.Clear()).AddTo(this);
            DeleteCommand = new ReactiveCommand();
            DeleteCommand.Subscribe(w =>
            {
                timeline.Disconnect();
                timelineService.RemoveAsync(timeline);
            }).AddTo(this);
        }

        private void Initialize()
        {
            _timeline.GetAsObservable()
                     .Where(w =>
                     {
                         if (w is Status)
                             return (bool) _timeline.Filter.Delegate.DynamicInvoke(w);
                         return true;
                     })
                     .ObserveOnUIDispatcher()
                     .Select(w =>
                     {
                         if (w is Status status)
                             return new StatusViewModel(_globalNotifier, status) as StatusBaseViewModel;
                         if (w is DeleteEvent)
                             return null;
                         if (w is EventBase @event)
                             return new NotificationViewModel(_globalNotifier, @event) as StatusBaseViewModel;
                         return null;
                     })
                     .Where(w => w != null)
                     .Subscribe(w => _statuses.Insert(0, w))
                     .AddTo(this);
        }
    }
}