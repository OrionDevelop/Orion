using Orion.Shared.Absorb.Objects;
using Orion.UWP.Models;

namespace Orion.UWP.ViewModels.Contents
{
    public class QuoteStatusViewModel : StatusViewModel
    {
        public StatusViewModel StatusViewModel { get; }

        public QuoteStatusViewModel(GlobalNotifier globalNotifier, Status status) : base(globalNotifier, status)
        {
            StatusViewModel = new StatusViewModel(globalNotifier, status.QuotedStatus);
        }
    }
}