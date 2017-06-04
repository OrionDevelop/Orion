using System.Linq;

using Orion.Service.Mastodon.Enum;
using Orion.Shared.Absorb.Enums;

using CroudiaAttachment = Orion.Service.Croudia.Models.Media;
using MastodonAttachment = Orion.Service.Mastodon.Models.Attachment;
using TwitterAttachment = CoreTweet.MediaEntity;

namespace Orion.Shared.Absorb.Objects
{
    public class Attachment
    {
        private readonly CroudiaAttachment _croudiaAttachment;
        private readonly MastodonAttachment _mastodonAttachment;
        private readonly TwitterAttachment _twitterAttachment;

        /// <summary>
        ///     URL
        /// </summary>
        public string Url { get; }

        /// <summary>
        ///     Preview url
        /// </summary>
        public string PreviewUrl => _croudiaAttachment?.MediaUrl ?? _mastodonAttachment?.PreviewUrl ?? _twitterAttachment.MediaUrlHttps;

        public MediaType MediaType { get; }

        public Attachment(CroudiaAttachment attachment)
        {
            _croudiaAttachment = attachment;
            Url = attachment.MediaUrl;
            MediaType = MediaType.Image;
        }

        public Attachment(MastodonAttachment attachment)
        {
            _mastodonAttachment = attachment;
            Url = attachment.Url;
            MediaType = attachment.Type == AttachmentType.Video ? MediaType.Video : MediaType.Image;
        }

        public Attachment(TwitterAttachment attachment)
        {
            _twitterAttachment = attachment;
            Url = attachment.Type == "video" ? attachment.VideoInfo.Variants.First(w => w.Bitrate != null).Url : attachment.MediaUrlHttps;
            MediaType = attachment.Type == "photo" ? MediaType.Image : MediaType.Video; // ??
        }
    }
}