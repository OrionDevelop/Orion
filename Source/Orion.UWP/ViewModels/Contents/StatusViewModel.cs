using System;

using Microsoft.Practices.Unity;

using Orion.Shared.Absorb.Objects;
using Orion.Shared.Emoji;
using Orion.UWP.Models;
using Orion.UWP.Mvvm;

using Reactive.Bindings;

namespace Orion.UWP.ViewModels.Contents
{
    public class StatusViewModel : StatusBaseViewModel
    {
        private readonly long _id;
        private readonly Status _status;

        public string ScreenName => $"@{_status.User.ScreenName}";
        public string Username => EmojiConverter.Convert(_status.User.Name);
        public string Icon { get; }
        public string Body => EmojiConverter.Convert(_status.Text);
        public string Via => _status.Source;

        public ReactiveCommand ReplyCommand { get; }

        public StatusViewModel() : base(null)
        {
            // Design instance
        }

        public StatusViewModel(long statusId) : base(null)
        {
            _id = statusId;
        }

        [InjectionConstructor]
        public StatusViewModel(GlobalNotifier globalNotifier, Status status) : base(status)
        {
            _id = status.Id;
            _status = status;
            Icon = Uri.TryCreate(status.User.IconUrl, UriKind.Absolute, out Uri _)
                ? status.User.IconUrl
                : $"https://{new Uri(status.User.Url).Host}{status.User.IconUrl}";
            ReplyCommand = new ReactiveCommand();
            ReplyCommand.Subscribe(() => globalNotifier.InReplyStatus = _status).AddTo(this);
        }

#pragma warning disable 659

        public override bool Equals(object obj)
#pragma warning restore 659
        {
            return _id == (obj as StatusViewModel)?._id;
        }
    }
}