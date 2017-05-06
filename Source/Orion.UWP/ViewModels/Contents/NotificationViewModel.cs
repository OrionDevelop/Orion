using System;

using Windows.UI;
using Windows.UI.Xaml.Media;

using Orion.UWP.Models.Absorb;
using Orion.UWP.Models.Emoji;

namespace Orion.UWP.ViewModels.Contents
{
    public class NotificationViewModel : StatusBaseViewModel
    {
        private readonly Notification _notification;

        public string Icon => _notification.NotificationType.ToIcon();
        public SolidColorBrush Color { get; }
        public string Message => string.Format(_notification.NotificationType.ToMessage(), EmojiConverter.Convert(_notification.User.Username));
        public bool IsShowStatus => _notification.NotificationType != NotificationType.Followed;
        public StatusViewModel StatusViewModel => IsShowStatus ? new StatusViewModel(_notification.Status) : null;

        public NotificationViewModel(Notification notification) : base(notification)
        {
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