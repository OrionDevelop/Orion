using Orion.UWP.Models.Absorb;

namespace Orion.Shared.Absorb.Objects
{
    public class Status : StatusBase
    {
        private readonly Service.Croudia.Models.Status _croudiaStatus;
        private readonly Service.GnuSocial.Models.Status _gnuSocialStatus;
        private readonly Service.Mastodon.Models.Status _mastodonStatus;
        private readonly CoreTweet.Status _twitterStatus;

        /// <summary>
        ///     Body (Show as HTML parsed content)
        /// </summary>
        public string Body => _croudiaStatus?.Text ?? _gnuSocialStatus?.Text ?? _mastodonStatus?.Content ?? _twitterStatus.Text;

        /// <summary>
        ///     In reply to status id
        /// </summary>
        public long? InReplyToStatusId =>
            _croudiaStatus?.InReplyToStatusId ?? _gnuSocialStatus?.InReplyToStatusId ?? _mastodonStatus?.InReplyToId ?? _twitterStatus?.InReplyToStatusId;

        /// <summary>
        ///     Reblog count
        /// </summary>
        public int ReblogsCount =>
            _croudiaStatus?.SpreadCount ?? _gnuSocialStatus?.RepeatNum ?? _mastodonStatus?.ReblogsCount ?? _twitterStatus.RetweetCount ?? 0;

        /// <summary>
        ///     Favorite count
        /// </summary>
        public int FavoritesCount =>
            _croudiaStatus?.FavoritedCount ?? _gnuSocialStatus?.FavNum ?? _mastodonStatus?.FavouritesCount ?? _twitterStatus.FavoriteCount ?? 0;

        /// <summary>
        ///     Is relobbed?
        /// </summary>
        public bool IsReblogged =>
            _croudiaStatus?.Spread ?? _gnuSocialStatus?.IsRepeated ?? _mastodonStatus?.IsReblogged ?? _twitterStatus.IsRetweeted ?? false;

        /// <summary>
        ///     Is favorited?
        /// </summary>
        public bool IsFavorited =>
            _croudiaStatus?.IsFavorited ?? _gnuSocialStatus?.IsFavorited ?? _mastodonStatus?.IsFavourited ?? _twitterStatus.IsFavorited ?? false;

        /// <summary>
        ///     Source
        /// </summary>
        public string Source => _croudiaStatus?.Source?.Name ?? _gnuSocialStatus?.Source ?? _mastodonStatus?.Application?.Name ?? _twitterStatus?.Source;

        #region Initialize from Status

        public Status(Service.Croudia.Models.Status status)
        {
            _croudiaStatus = status;
            Type = StatusType.Status;
            Id = status.Id;
            CreatedAt = status.CreatedAt;
            User = new User(_croudiaStatus.User);
        }

        public Status(Service.GnuSocial.Models.Status status)
        {
            _gnuSocialStatus = status;
            Type = StatusType.Status;
            Id = status.Id;
            CreatedAt = status.CreatedAt;
            User = new User(_gnuSocialStatus.User);
        }

        public Status(Service.Mastodon.Models.Status status)
        {
            _mastodonStatus = status;
            Type = StatusType.Status;
            Id = status.Id;
            CreatedAt = status.CreatedAt;
            User = new User(_mastodonStatus.Account);
        }

        public Status(CoreTweet.Status status)
        {
            _twitterStatus = status;
            Type = StatusType.Status;
            Id = status.Id;
            CreatedAt = status.CreatedAt.DateTime;
            User = new User(_twitterStatus.User);
        }

        #endregion
    }
}