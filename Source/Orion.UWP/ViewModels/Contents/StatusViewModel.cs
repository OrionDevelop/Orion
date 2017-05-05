using System;

using Orion.UWP.Models.Absorb;
using Orion.UWP.Mvvm;

namespace Orion.UWP.ViewModels.Contents
{
    public class StatusViewModel : ViewModel
    {
        private readonly Status _status;

        public string ScreenName => $"@{_status.User.ScreenName}";
        public string Username => _status.User.Username;
        public string Icon { get; }
        public string Body => _status.Body;
        public string CreatedAt => _status.CreatedAt.ToLocalTime().ToString("g");
        public string Via => _status.Source;

        public StatusViewModel(Status status)
        {
            _status = status;
            Icon = Uri.TryCreate(status.User.Icon, UriKind.Absolute, out Uri _)
                ? status.User.Icon
                : $"https://{new Uri(status.User.Url).Host}{status.User.Icon}";
        }
    }
}