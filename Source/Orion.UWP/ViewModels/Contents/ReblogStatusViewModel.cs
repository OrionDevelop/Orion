using Orion.Shared.Absorb.Objects;
using Orion.UWP.Models;

namespace Orion.UWP.ViewModels.Contents
{
    public class ReblogStatusViewModel : StatusBaseViewModel
    {
        public string Message { get; }
        public StatusViewModel StatusViewModel { get; }

        public ReblogStatusViewModel(GlobalNotifier globalNotifier, Status status) : base(status)
        {
            Message = $"{status.User.Name.Trim()} reblogged";
            StatusViewModel = new StatusViewModel(globalNotifier, status.RebloggedStatus);
        }
    }
}