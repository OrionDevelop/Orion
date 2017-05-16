using Orion.Shared.Enums;
using Orion.UWP.Extensions;
using Orion.UWP.Mvvm;

namespace Orion.UWP.ViewModels.Contents
{
    public class TimelineTypeViewModel : ViewModel
    {
        public TimelineType TimelineType { get; }
        public string Name => TimelineType.ToName();
        public string Icon => TimelineType.ToIcon();

        public TimelineTypeViewModel(TimelineType timelineType)
        {
            TimelineType = timelineType;
        }
    }
}