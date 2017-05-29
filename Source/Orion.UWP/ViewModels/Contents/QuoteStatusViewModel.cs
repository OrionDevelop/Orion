using Orion.Shared.Absorb.Objects;
using Orion.Shared.Models;
using Orion.UWP.Models;
using Orion.UWP.Services.Interfaces;

namespace Orion.UWP.ViewModels.Contents
{
    public class QuoteStatusViewModel : StatusViewModel
    {
        public StatusViewModel StatusViewModel { get; }

        public QuoteStatusViewModel(GlobalNotifier globalNotifier, IDialogService dialogService, Status status, TimelineBase timeline) : base(globalNotifier, dialogService, status, timeline)
        {
            StatusViewModel = new StatusViewModel(globalNotifier, dialogService, status.QuotedStatus, timeline);
        }
    }
}