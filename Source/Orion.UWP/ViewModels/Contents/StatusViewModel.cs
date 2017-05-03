using Orion.UWP.Models.Absorb;
using Orion.UWP.Mvvm;

namespace Orion.UWP.ViewModels.Contents
{
    public class StatusViewModel : ViewModel
    {
        private readonly Status _status;

        public string ScreenName => _status.User.ScreenName;
        public string Username => _status.User.Username;
        public string Icon => _status.User.Icon;
        public string Body => _status.Body;
        public string CreatedAt => _status.CreatedAt.ToString("g");
        public string Via => _status.Source;

        public StatusViewModel(Status status)
        {
            _status = status;
        }
    }
}