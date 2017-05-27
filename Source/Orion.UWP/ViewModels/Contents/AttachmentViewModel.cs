using Orion.Shared.Absorb.Objects;
using Orion.UWP.Mvvm;

namespace Orion.UWP.ViewModels.Contents
{
    public class AttachmentViewModel : ViewModel
    {
        private readonly Attachment _attachment;
        public string LinkUrl => _attachment.Url;
        public string PreviewUrl => _attachment.PreviewUrl;

        public AttachmentViewModel(Attachment attachment)
        {
            _attachment = attachment;
        }
    }
}