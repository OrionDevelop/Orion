using System;

using Windows.UI;
using Windows.UI.Xaml.Media;

using Orion.Shared.Absorb.Objects;
using Orion.Shared.Emoji;
using Orion.UWP.Models;
using Orion.UWP.Models.Absorb;

namespace Orion.UWP.ViewModels.Contents
{
    public class NotificationViewModel : StatusBaseViewModel
    {
        private readonly GlobalNotifier _globalNotifier;
        private readonly Notification _notification;

        public string Icon => _notification.NotificationType.ToIcon();
        public SolidColorBrush Color { get; }
        public string Message => string.Format(_notification.NotificationType.ToMessage(), EmojiConverter.Convert(_notification.User.Username));
        public bool IsShowStatus => _notification.NotificationType != NotificationType.Followed;
        public StatusViewModel StatusViewModel => IsShowStatus ? new StatusViewModel(_globalNotifier, _notification.Status) : null;

        public NotificationViewModel(GlobalNotifier globalNotifier, Notification notification) : base(notification)
        {
            _globalNotifier = globalNotifier;
            _notification = notification;
            switch (_notification.NotificationType)
            {
                case NotificationType.Followed:
                    Color = new SolidColorBrush(Colors.DeepSkyBlue);
                    break;

                case NotificationType.Favorited:
                    Color = new SolidColorBrush(Colors.Yellow);
                    break;

                case NotificationType.Reblogged:
                    Color = new SolidColorBrush(Colors.LightGreen);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}