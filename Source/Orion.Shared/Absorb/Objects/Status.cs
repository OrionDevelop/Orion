using System.Collections.Generic;
using System.Linq;

using CroudiaStatus = Orion.Service.Croudia.Models.Status;
using GnuSocialStatus = Orion.Service.GnuSocial.Models.Status;
using MastodonStatus = Orion.Service.Mastodon.Models.Status;
using TwitterStatus = CoreTweet.Status;

namespace Orion.Shared.Absorb.Objects
{
    /// <summary>
    ///     Status (a.k.a Tweet, Whisper, Quip and others.)
    /// </summary>
    public class Status : StatusBase
    {
        private readonly CroudiaStatus _croudiaStatus;
        private readonly GnuSocialStatus _gnuSocialStatus;
        private readonly MastodonStatus _mastodonStatus;
        private readonly TwitterStatus _twitterStatus;

        /// <summary>
        ///     Status body
        /// </summary>
        public string Text =>
            _croudiaStatus?.Text ?? _gnuSocialStatus?.Text ?? _mastodonStatus?.Content ?? _twitterStatus.Text;

        /// <summary>
        ///     In reply to status ID
        /// </summary>
        public long? InReplyToStatusId =>
            _croudiaStatus?.InReplyToStatusId ?? _gnuSocialStatus?.InReplyToStatusId ?? _mastodonStatus?.InReplyToId ?? _twitterStatus?.InReplyToStatusId ?? 0;

        /// <summary>
        ///     In reply to user ID
        /// </summary>
        public long? InReplyToUserId =>
            _croudiaStatus?.InReplyToUserId ?? _gnuSocialStatus?.InReplyToUserId ?? _mastodonStatus?.InReplyToAccountId ?? _twitterStatus?.InReplyToUserId ?? 0;

        /// <summary>
        ///     Reblogs count
        /// </summary>
        public long? ReblogsCount =>
            _croudiaStatus?.SpreadCount ?? _gnuSocialStatus?.RepeatNum ?? _mastodonStatus?.ReblogsCount ?? _twitterStatus?.RetweetCount ?? 0;

        /// <summary>
        ///     Favorites count
        /// </summary>
        public long? FavoritesCount =>
            _croudiaStatus?.FavoritedCount ?? _gnuSocialStatus?.FavNum ?? _mastodonStatus?.FavouritesCount ?? _twitterStatus?.FavoriteCount ?? 0;

        /// <summary>
        ///     Is reblogged?
        /// </summary>
        public bool? IsReblogged =>
            _croudiaStatus?.Spread ?? _gnuSocialStatus?.IsRepeated ?? _mastodonStatus?.IsReblogged ?? _twitterStatus?.IsRetweeted ?? false;

        /// <summary>
        ///     Is favorited?
        /// </summary>
        public bool? IsFavorited =>
            _croudiaStatus?.IsFavorited ?? _gnuSocialStatus?.IsFavorited ?? _mastodonStatus?.IsFavourited ?? _twitterStatus?.IsFavorited ?? false;

        /// <summary>
        ///     Attachments (Image, Video or ....)
        /// </summary>
        public List<Attachment> Attachments { get; }

        /// <summary>
        ///     Reblogged status
        /// </summary>
        public Status RebloggedStatus { get; }

        /// <summary>
        ///     Quoted status
        /// </summary>
        public Status QuotedStatus { get; }

        /// <summary>
        ///     via
        /// </summary>
        public string Source =>
            _croudiaStatus?.Source?.Name ?? _gnuSocialStatus?.Source ?? _mastodonStatus?.Application?.Name ?? _twitterStatus?.Source;

        /// <summary>
        ///     ローカルステータスか (for OStatus)
        /// </summary>
        public bool IsLocal =>
            _gnuSocialStatus?.IsLocal ?? _mastodonStatus?.Account?.Acct?.Contains("@").Equals(false) ?? true;

        /// <summary>
        ///     for OStatus
        /// </summary>
        public string ExternalUrl =>
            _gnuSocialStatus?.ExternalUrl ?? _mastodonStatus?.Url ?? "";

        public Status(CroudiaStatus status)
        {
            Type = nameof(Status);
            Id = status.Id;
            CreatedAt = status.CreatedAt;
            User = new User(status.User);
            Attachments = status.Entities?.Media == null
                ? new List<Attachment>()
                : new List<Attachment>
                {
                    new Attachment(status.Entities.Media)
                };
            RebloggedStatus = status.SpreadStatus != null ? new Status(status.SpreadStatus) : null;
            QuotedStatus = status.QuoteStatus != null ? new Status(status.QuoteStatus) : null;
            _croudiaStatus = status;
        }

        public Status(GnuSocialStatus status)
        {
            Type = nameof(Status);
            Id = status.Id;
            CreatedAt = status.CreatedAt;
            User = new User(status.User);
            RebloggedStatus = status.Source == "share" ? new Status(status) : null;
            _gnuSocialStatus = status;
        }

        public Status(MastodonStatus status)
        {
            Type = nameof(Status);
            Id = status.Id;
            CreatedAt = status.CreatedAt;
            User = new User(status.Account);
            Attachments = status.MediaAttachments.Select(w => new Attachment(w)).ToList();
            RebloggedStatus = status.Reblog != null ? new Status(status.Reblog) : null;
            _mastodonStatus = status;
        }

        public Status(TwitterStatus status)
        {
            Type = nameof(Status);
            Id = status.Id;
            CreatedAt = status.CreatedAt.ToLocalTime().LocalDateTime;
            User = new User(status.User);
            Attachments = status.ExtendedEntities.Media != null ? status.ExtendedEntities.Media.Select(w => new Attachment(w)).ToList() : new List<Attachment>();
            RebloggedStatus = status.RetweetedStatus != null ? new Status(status.RetweetedStatus) : null;
            QuotedStatus = status.QuotedStatus != null ? new Status(status.QuotedStatus) : null;
            _twitterStatus = status;
        }
    }
}