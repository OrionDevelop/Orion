using System;
using System.Diagnostics;
using System.Reactive.Linq;

using Orion.UWP.Mvvm;
using Orion.UWP.Services.Interfaces;
using Orion.UWP.ViewModels.Contents;

using Reactive.Bindings;

namespace Orion.UWP.ViewModels
{
    public class AppShellViewModel : ViewModel
    {
        public ReadOnlyReactiveCollection<TimelineViewModel> Timelines { get; }

        public AppShellViewModel(IAccountService accountService, ITimelineService timelineService)
        {
            Timelines = timelineService.Timelines.ToReadOnlyReactiveCollection(w => new TimelineViewModel(w));
            timelineService.Timelines.ToObservable().Subscribe(w => { Debug.WriteLine(""); });
        }
    }
}