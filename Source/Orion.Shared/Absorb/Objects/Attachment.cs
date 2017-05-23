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
        public string Url => _croudiaAttachment?.MediaUrl ?? _mastodonAttachment?.Url ?? _twitterAttachment.MediaUrlHttps;

        /// <summary>
        ///     Preview url
        /// </summary>
        public string PreviewUrl => _croudiaAttachment?.MediaUrl ?? _mastodonAttachment?.PreviewUrl ?? _twitterAttachment.MediaUrlHttps;

        public MediaType MediaType { get; }

        public Attachment(CroudiaAttachment attachment)
        {
            _croudiaAttachment = attachment;
            MediaType = MediaType.Image;
        }

        public Attachment(MastodonAttachment attachment)
        {
            _mastodonAttachment = attachment;
            MediaType = MediaType.Image;
        }

        public Attachment(TwitterAttachment attachment)
        {
            _twitterAttachment = attachment;
            MediaType = MediaType.Image;
        }
    }
}