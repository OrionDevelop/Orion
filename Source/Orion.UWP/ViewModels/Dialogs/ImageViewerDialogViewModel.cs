using System.Collections.Generic;

using Orion.Shared.Absorb.Objects;
using Orion.UWP.Mvvm;

namespace Orion.UWP.ViewModels.Dialogs
{
    public class ImageViewerDialogViewModel : ViewModel
    {
        public List<Attachment> Attachments { get; }

        public ImageViewerDialogViewModel(Status status)
        {
            Attachments = status.Attachments;
        }
    }
}