using System;

using Windows.Media.Core;

using Orion.Shared.Absorb.Enums;
using Orion.Shared.Absorb.Objects;
using Orion.UWP.Mvvm;

namespace Orion.UWP.ViewModels.Contents
{
    public class AttachmentViewModel : ViewModel
    {
        private readonly Attachment _attachment;
        public string LinkUrl => _attachment.Url;
        public MediaSource VideoSource { get; }
        public string PreviewUrl => _attachment.Url;
        public bool IsVideoMode => _attachment.MediaType == MediaType.Video;
        public double Height => _attachment.Height;
        public double Width => _attachment.Width;

        public AttachmentViewModel(Attachment attachment)
        {
            _attachment = attachment;
            if (IsVideoMode)
                VideoSource = MediaSource.CreateFromUri(new Uri(LinkUrl));
        }
    }
}