using System.Linq;
using System.Text.RegularExpressions;

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
        private readonly Regex _sizeRegex = new Regex(@"\/vid\/(?<width>[0-9]+)x(?<height>[0-9]+)\/.*");
        private readonly TwitterAttachment _twitterAttachment;

        /// <summary>
        ///     URL
        /// </summary>
        public string Url { get; }

        /// <summary>
        ///     Preview url
        /// </summary>
        public string PreviewUrl => _croudiaAttachment?.MediaUrl ?? _mastodonAttachment?.PreviewUrl ?? _twitterAttachment.MediaUrlHttps;

        /// <summary>
        ///     Video height
        /// </summary>
        public double Height { get; } = double.NaN;

        /// <summary>
        ///     Video width
        /// </summary>
        public double Width { get; } = double.NaN;

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
            if (attachment.Type == "video")
            {
                Url = attachment.VideoInfo.Variants.Where(w => w.Bitrate != null).OrderBy(w => w.Bitrate).Last().Url;
                MediaType = MediaType.Video;
                if (_sizeRegex.IsMatch(Url))
                {
                    var match = _sizeRegex.Match(Url);
                    Height = double.Parse(match.Groups["height"].Value);
                    Width = double.Parse(match.Groups["width"].Value);
                }
            }
            else if (attachment.Type == "animated_gif")
            {
                Url = attachment.VideoInfo.Variants.Where(w => w.Bitrate != null).OrderBy(w => w.Bitrate).Last().Url;
                MediaType = MediaType.Video;
                Height = attachment.Sizes.Large.Height;
                Width = attachment.Sizes.Large.Width;
            }
            else
            {
                Url = attachment.MediaUrlHttps;
                MediaType = MediaType.Image;
            }
        }
    }
}