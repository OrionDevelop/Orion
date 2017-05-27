using System;

using Orion.Shared.Models;
using Orion.UWP.Extensions;
using Orion.UWP.Mvvm;
using Orion.UWP.Services.Interfaces;

using Reactive.Bindings;

namespace Orion.UWP.ViewModels.Timelines
{
    public class TimelineViewModel : ViewModel
    {
        private readonly TimelineBase _timeline;

        public string Name => _timeline.Name;
        public string User => _timeline.Account.Credential.User.ScreenNameWithHost;
        public string Icon => _timeline.ToIcon();

        public ReactiveCommand DeleteCommand { get; }

        protected TimelineViewModel(ITimelineService timelineService, TimelineBase timeline)
        {
            _timeline = timeline;
            DeleteCommand = new ReactiveCommand();
            DeleteCommand.Subscribe(async w =>
            {
                Delete();
                await timelineService.RemoveAsync(timeline);
            }).AddTo(this);
        }

        protected virtual void Delete() { }
    }
}