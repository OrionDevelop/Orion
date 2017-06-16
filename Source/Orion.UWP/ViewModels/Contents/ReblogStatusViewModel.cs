using Orion.Shared.Absorb.Objects;
using Orion.Shared.Models;
using Orion.UWP.Models;
using Orion.UWP.Mvvm;
using Orion.UWP.Services.Interfaces;

using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace Orion.UWP.ViewModels.Contents
{
    public class ReblogStatusViewModel : StatusBaseViewModel
    {
        public string Message { get; }
        public StatusViewModel StatusViewModel { get; }
        public ReactiveProperty<bool> IsIconRounded { get; }

        public ReblogStatusViewModel(GlobalNotifier globalNotifier, IDialogService dialogService, Status status, TimelineBase timeline) : base(status)
        {
            Message = $"{status.User.Name.Trim()} reblogged";
            IsIconRounded = globalNotifier.ObserveProperty(w => w.IsIconRounded).ToReactiveProperty().AddTo(this);
            StatusViewModel = new StatusViewModel(globalNotifier, dialogService, status.RebloggedStatus, timeline);
        }
    }
}