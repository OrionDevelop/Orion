﻿using System;
using System.Collections.Generic;
using System.Linq;

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

        public StatusViewModel(long statusId) : base(null)
        {
            _id = statusId;
        }

        public StatusViewModel(GlobalNotifier globalNotifier, Status status) : base(status)
        {
            _id = status.Id;
            _status = status;
            Icon = Uri.TryCreate(status.User.IconUrl, UriKind.Absolute, out Uri _)
                ? status.User.IconUrl
                : $"https://{new Uri(status.User.Url).Host}{status.User.IconUrl}";
            ReplyCommand = new ReactiveCommand();
            ReplyCommand.Subscribe(() => globalNotifier.InReplyStatus = _status).AddTo(this);
            Attachments = _status.Attachments.Select(w => new AttachmentViewModel(w)).ToList();
        }

        public override bool Equals(object obj)
        {
            return _id == (obj as StatusViewModel)?._id;
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }
    }
}