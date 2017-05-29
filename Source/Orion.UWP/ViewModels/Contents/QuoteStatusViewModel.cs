using Orion.Shared.Absorb.Objects;
using Orion.Shared.Models;
using Orion.UWP.Models;

namespace Orion.UWP.ViewModels.Contents
{
    public class QuoteStatusViewModel : StatusViewModel
    {
        public StatusViewModel StatusViewModel { get; }

        public QuoteStatusViewModel(GlobalNotifier globalNotifier, Status status, TimelineBase timeline) : base(globalNotifier, status, timeline)
        {
            StatusViewModel = new StatusViewModel(globalNotifier, status.QuotedStatus, timeline);
        }
    }
}