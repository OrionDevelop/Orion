using System;

using Microsoft.Practices.Unity;

using Orion.UWP.Models;
using Orion.UWP.Models.Absorb;
using Orion.UWP.Models.Emoji;
using Orion.UWP.Mvvm;

using Reactive.Bindings;

namespace Orion.UWP.ViewModels.Contents
{
    public class StatusViewModel : StatusBaseViewModel
    {
        private readonly Status _status;

        public string ScreenName => $"@{_status.User.ScreenName}";
        public string Username => EmojiConverter.Convert(_status.User.Username);
        public string Icon { get; }
        public string Body => EmojiConverter.Convert(_status.Body);
        public string Via => _status.Source;

        public ReactiveCommand ReplyCommand { get; }

        public StatusViewModel() : base(null)
        {
            // Design instance
        }

        [InjectionConstructor]
        public StatusViewModel(GlobalNotifier globalNotifier, Status status) : base(status)
        {
            _status = status;
            Icon = Uri.TryCreate(status.User.Icon, UriKind.Absolute, out Uri uri)
                ? status.User.Icon
                : $"https://{uri.Host}{status.User.Icon}";
            ReplyCommand = new ReactiveCommand();
            ReplyCommand.Subscribe(() => globalNotifier.InReplyStatus = _status).AddTo(this);
        }
    }
}