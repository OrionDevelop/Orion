using Orion.Shared.Absorb.Objects;
using Orion.Shared.Absorb.Objects.Events;
using Orion.Shared.Models;
using Orion.UWP.Services.Interfaces;
using Orion.UWP.ViewModels.Contents;

namespace Orion.UWP.Models
{
    public static class Attacher
    {
        public static StatusBaseViewModel Attach(GlobalNotifier globalNotifier, IDialogService dialogService, StatusBase @base, TimelineBase timeline)
        {
            if (@base is Status status)
                if (status.RebloggedStatus != null)
                    return new ReblogStatusViewModel(globalNotifier, dialogService, status, timeline);
                else if (status.QuotedStatus != null)
                    return new QuoteStatusViewModel(globalNotifier, dialogService, status, timeline);
                else
                    return new StatusViewModel(globalNotifier, dialogService, status, timeline);
            if (@base is EventBase @event)
                return new NotificationViewModel(globalNotifier, @event);
            return null;
        }
    }
}