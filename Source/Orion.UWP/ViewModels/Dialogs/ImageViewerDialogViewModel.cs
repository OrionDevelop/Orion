using System.Collections.Generic;
using System.Linq;

using Orion.Shared.Absorb.Objects;
using Orion.UWP.Mvvm;
using Orion.UWP.ViewModels.Contents;

namespace Orion.UWP.ViewModels.Dialogs
{
    public class ImageViewerDialogViewModel : ViewModel
    {
        public List<AttachmentViewModel> Attachments { get; }

        public ImageViewerDialogViewModel(Status status)
        {
            Attachments = status.Attachments.Select(w => new AttachmentViewModel(w).AddTo(this)).ToList();
        }
    }
}