using Orion.Shared.Absorb.Objects;
using Orion.Shared.Models;
using Orion.UWP.Models;
using Orion.UWP.Services.Interfaces;

namespace Orion.UWP.ViewModels.Contents
{
    public class ReblogStatusViewModel : StatusBaseViewModel
    {
        public string Message { get; }
        public StatusViewModel StatusViewModel { get; }

        public ReblogStatusViewModel(GlobalNotifier globalNotifier, IDialogService dialogService, Status status, TimelineBase timeline) : base(status)
        {
            Message = $"{status.User.Name.Trim()} reblogged";
            StatusViewModel = new StatusViewModel(globalNotifier, dialogService, status.RebloggedStatus, timeline);
        }
    }
}