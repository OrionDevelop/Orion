using Orion.Shared.Absorb.Objects;
using Orion.Shared.Absorb.Objects.Events;
using Orion.Shared.Models;
using Orion.UWP.ViewModels.Contents;

namespace Orion.UWP.Models
{
    public static class Attacher
    {
        public static StatusBaseViewModel Attach(GlobalNotifier globalNotifier, StatusBase @base, TimelineBase timeline)
        {
            if (@base is Status status)
                if (status.RebloggedStatus != null)
                    return new ReblogStatusViewModel(globalNotifier, status, timeline);
                else if (status.QuotedStatus != null)
                    return new QuoteStatusViewModel(globalNotifier, status, timeline);
                else
                    return new StatusViewModel(globalNotifier, status, timeline);
            if (@base is EventBase @event)
                return new NotificationViewModel(globalNotifier, @event);
            return null;
        }
    }
}