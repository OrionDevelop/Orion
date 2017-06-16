using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

using Windows.UI.Xaml.Input;

using Orion.Shared.Absorb.Objects;
using Orion.Shared.Emoji;
using Orion.Shared.Models;
using Orion.UWP.Models;
using Orion.UWP.Mvvm;
using Orion.UWP.Services.Interfaces;
using Orion.UWP.ViewModels.Dialogs;
using Orion.UWP.Views.Dialogs;

using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace Orion.UWP.ViewModels.Contents
{
    public class StatusViewModel : StatusBaseViewModel
    {
        private readonly IDialogService _dialogService;
        private readonly Status _status;
        public string ScreenName => $"@{_status.User.ScreenName}";
        public string Username => EmojiConverter.Convert(_status.User.Name).Trim();
        public string Icon { get; }
        public string Body => EmojiConverter.Convert(_status.Text).Trim();
        public bool IsVerified => _status.User.IsVerified;
        public bool HasMedia => _status.Attachments.Count > 0;
        public bool IsSensitive => _status.IsSensitiveContent;
        public List<AttachmentViewModel> Attachments { get; }
        public ReactiveProperty<bool> IsIconRounded { get; }
        public ReactiveCommand ReplyCommand { get; }
        public AsyncReactiveCommand ReblogCommand { get; }
        public AsyncReactiveCommand FavoriteCommand { get; }
        public AsyncReactiveCommand DeleteCommand { get; }

        public StatusViewModel() : base(null)
        {
            // Design instance
        }

        public StatusViewModel(Status status) : this(null, null, status, null)
        {
        }

        public StatusViewModel(GlobalNotifier globalNotifier, IDialogService dialogService, Status status, TimelineBase timeline) : base(status)
        {
            _dialogService = dialogService;
            _status = status;
            Icon = Uri.TryCreate(status.User.IconUrl, UriKind.Absolute, out Uri _)
                ? status.User.IconUrl
                : $"https://{new Uri(status.User.Url).Host}{status.User.IconUrl}";
            IsIconRounded = globalNotifier.ObserveProperty(w => w.IsIconRounded).ToReactiveProperty().AddTo(this);
            ReplyCommand = new ReactiveCommand();
            ReplyCommand.Subscribe(() =>
            {
                globalNotifier.InReplyStatus = _status;
                globalNotifier.InReplyTimeline = timeline;
            }).AddTo(this);
            ReblogCommand = new AsyncReactiveCommand(Observable.Return(!status.User.IsProtected));
            ReblogCommand.Subscribe(async () => { await timeline.Account.ClientWrapper.ReblogAsync(status.Id); });
            FavoriteCommand = new AsyncReactiveCommand();
            FavoriteCommand.Subscribe(async () => { await timeline.Account.ClientWrapper.FavoriteAsync(status.Id); });
            DeleteCommand = new AsyncReactiveCommand(Observable.Return(status.User.Id == timeline.Account.Credential.UserId));
            DeleteCommand.Subscribe(async () => await timeline.Account.ClientWrapper.DestroyAsync(status.Id));
            Attachments = _status.Attachments.Select(w => new AttachmentViewModel(w)).ToList();
        }

        public void OnTappedImageEvent(object sender, TappedRoutedEventArgs e)
        {
            _dialogService.ShowDialogAsync(new ImageViewerDialog {DataContext = new ImageViewerDialogViewModel(_status)});
        }
    }
}