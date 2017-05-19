using Windows.UI.Xaml.Media;

using Orion.Shared.Absorb.Objects.Events;
using Orion.UWP.Extensions;
using Orion.UWP.Models;

namespace Orion.UWP.ViewModels.Contents
{
    public class NotificationViewModel : StatusBaseViewModel
    {
        private readonly GlobalNotifier _globalNotifier;
        private readonly EventBase _notification;
        public string Icon => _notification.EventType.ToIcon();
        public SolidColorBrush Color => _notification.EventType.ToColor();
        public string Message => string.Format(_notification.EventType.ToFormatMessage(), _notification.Source.Name);
        public StatusViewModel StatusViewModel { get; }
        public bool IsShowStatus => false;

        public NotificationViewModel(GlobalNotifier globalNotifier, EventBase notification) : base(notification)
        {
            _globalNotifier = globalNotifier;
            _notification = notification;
            if (_notification.Target != null)
                StatusViewModel = new StatusViewModel(globalNotifier, _notification.Target);
        }
    }
}