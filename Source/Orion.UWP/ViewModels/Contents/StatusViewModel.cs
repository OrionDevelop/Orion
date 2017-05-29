using System;
using System.Collections.Generic;
using System.Linq;

using Orion.Shared.Absorb.Objects;
using Orion.Shared.Emoji;
using Orion.Shared.Models;
using Orion.UWP.Models;
using Orion.UWP.Mvvm;

using Reactive.Bindings;

namespace Orion.UWP.ViewModels.Contents
{
    public class StatusViewModel : StatusBaseViewModel
    {
        private readonly Status _status;

        public string ScreenName => $"@{_status.User.ScreenName}";
        public string Username => EmojiConverter.Convert(_status.User.Name).Trim();
        public string Icon { get; }
        public string Body => EmojiConverter.Convert(_status.Text).Trim();
        public bool HasMedia => _status.Attachments.Count > 0;
        public List<AttachmentViewModel> Attachments { get; }

        public ReactiveCommand ReplyCommand { get; }

        public StatusViewModel() : base(null)
        {
            // Design instance
        }

        public StatusViewModel(Status status) : this(null, status, null) { }

        public StatusViewModel(GlobalNotifier globalNotifier, Status status, TimelineBase timeline) : base(status)
        {
            _status = status;
            Icon = Uri.TryCreate(status.User.IconUrl, UriKind.Absolute, out Uri _)
                ? status.User.IconUrl
                : $"https://{new Uri(status.User.Url).Host}{status.User.IconUrl}";
            ReplyCommand = new ReactiveCommand();
            ReplyCommand.Subscribe(() =>
            {
                globalNotifier.InReplyStatus = _status;
                globalNotifier.InReplyTimeline = timeline;
            }).AddTo(this);
            Attachments = _status.Attachments.Select(w => new AttachmentViewModel(w)).ToList();
        }
    }
}