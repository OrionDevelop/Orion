using Orion.UWP.Models;
using Orion.UWP.Models.Enum;
using Orion.UWP.Mvvm;

namespace Orion.UWP.ViewModels.Contents
{
    public class TimelineViewModel : ViewModel
    {
        private readonly Timeline _timeline;
        public string Name => _timeline.TimelineType.ToName();
        public string User => _timeline.Account.Credential.Username;
        public string Icon => _timeline.TimelineType.ToIcon();

        public TimelineViewModel(Timeline timeline)
        {
            _timeline = timeline;
        }
    }
}