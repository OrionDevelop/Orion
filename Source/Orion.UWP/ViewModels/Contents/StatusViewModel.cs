using System;

using Microsoft.Practices.Unity;

using Orion.UWP.Models.Absorb;

namespace Orion.UWP.ViewModels.Contents
{
    public class StatusViewModel : StatusBaseViewModel
    {
        private readonly Status _status;

        public string ScreenName => $"@{_status.User.ScreenName}";
        public string Username => _status.User.Username;
        public string Icon { get; }
        public string Body => _status.Body;
        public string Via => _status.Source;

        public StatusViewModel() : base(null)
        {
            // Design instance
        }

        [InjectionConstructor]
        public StatusViewModel(Status status) : base(status)
        {
            _status = status;
            Icon = Uri.TryCreate(status.User.Icon, UriKind.Absolute, out Uri _)
                ? status.User.Icon
                : $"https://{new Uri(status.User.Url).Host}{status.User.Icon}";
        }
    }
}