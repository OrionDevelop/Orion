using Windows.UI.Xaml.Media;

using Orion.Shared.Absorb.Objects.Events;
using Orion.Shared.Emoji;
using Orion.UWP.Extensions;
using Orion.UWP.Models;

namespace Orion.UWP.ViewModels.Contents
{
    public class NotificationViewModel : StatusBaseViewModel
    {
        private readonly EventBase _notification;
        public string Icon => _notification.EventType.ToIcon();
        public SolidColorBrush Color => _notification.EventType.ToColor();
        public string UserIcon => _notification.Source.IconUrl;
        public string Message => string.Format(_notification.EventType.ToFormatMessage(), EmojiConverter.Convert(_notification.Source.Name));
        public StatusViewModel StatusViewModel { get; }
        public bool IsShowStatus { get; }

        public NotificationViewModel(GlobalNotifier globalNotifier, EventBase notification) : base(notification)
        {
            _notification = notification;
            if (_notification.Target != null)
            {
                StatusViewModel = new StatusViewModel(globalNotifier, null, _notification.Target, null);
                IsShowStatus = true;
            }
        }
    }
}