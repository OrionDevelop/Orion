using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using CoreTweet;

using Orion.Shared.Enums;

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
        private readonly Regex _mediaLinkRegex = new Regex("<a href=\"(?<url>.*?)\" .*?>.*?</a>");

        /// <summary>
        ///     Status body.
        /// </summary>
        public string Text { get; }

        /// <summary>
        ///     In reply to status ID.
        /// </summary>
        public long? InReplyToStatusId { get; }

        /// <summary>
        ///     In reply to user ID.
        /// </summary>
        public long? InReplyToUserId { get; }

        /// <summary>
        ///     Reblogs count.
        /// </summary>
        public long? ReblogsCount { get; }

        /// <summary>
        ///     Favorites count.
        /// </summary>
        public long? FavoritesCount { get; }

        /// <summary>
        ///     Attachments (as Images, Video and others...).
        /// </summary>
        public List<Attachment> Attachments { get; } = new List<Attachment>();

        /// <summary>
        ///     Reblogged status.
        /// </summary>
        public Status RebloggedStatus { get; }

        /// <summary>
        ///     Quoted status.
        /// </summary>
        public Status QuotedStatus { get; }

        /// <summary>
        ///     via
        /// </summary>
        public string Source { get; }

        /// <summary>
        ///     INTERNAL
        /// </summary>
        public Dictionary<string, string> Hyperlinks { get; } = new Dictionary<string, string>();

        public Status(CroudiaStatus status)
        {
            Id = status.Id;
            CreatedAt = status.CreatedAt;
            User = new User(status.User);
            Type = nameof(Status);
            Provider = ProviderType.Croudia.ToString();
            Text = status.Text;
            InReplyToStatusId = status.InReplyToStatusId;
            InReplyToUserId = status.InReplyToUserId;
            ReblogsCount = status.SpreadCount;
            FavoritesCount = status.FavoritedCount;
            if (status.Entities != null)
                Attachments = new List<Attachment> {new Attachment(status.Entities.Media)};
            if (status.SpreadStatus != null)
                RebloggedStatus = new Status(status.SpreadStatus);
            if (status.QuoteStatus != null)
                QuotedStatus = new Status(status.QuoteStatus);
            Source = status.Source.Name;
            IsReblogged = status.Spread;
            IsFavorited = status.IsFavorited;
        }

        public Status(GnuSocialStatus status)
        {
            Id = status.Id;
            CreatedAt = status.CreatedAt;
            User = new User(status.User);
            Type = nameof(Status);
            Provider = ProviderType.GnuSocial.ToString();
            Text = status.Text;
            InReplyToStatusId = status.InReplyToStatusId;
            InReplyToUserId = status.InReplyToUserId;
            ReblogsCount = status.RepeatNum;
            FavoritesCount = status.FavNum;
            // Attachments is None
            if (status.Source == "share")
                RebloggedStatus = new Status(status);
            // QuotedStatus is None
            Source = status.Source;
            IsReblogged = status.IsRepeated;
            IsFavorited = status.IsFavorited;
            IsLocal = status.IsLocal;
        }

        public Status(MastodonStatus status)
        {
            Id = status.Id;
            CreatedAt = status.CreatedAt;
            User = new User(status.Account);
            Type = nameof(Status);
            Provider = ProviderType.Mastodon.ToString();
            InReplyToStatusId = status.InReplyToId;
            InReplyToUserId = status.InReplyToAccountId;
            ReblogsCount = status.ReblogsCount;
            FavoritesCount = status.FavouritesCount;
            Attachments = status.MediaAttachments.Select(w => new Attachment(w)).ToList();
            if (status.Reblog != null)
                RebloggedStatus = new Status(status.Reblog);
            Source = status.Application?.Name ?? "";
            IsReblogged = status.IsReblogged ?? false;
            IsFavorited = status.IsFavourited ?? false;
            IsLocal = status.Account.Acct.Contains("@");
            IsSensitiveContent = status.IsSensitive ?? false;

            var text = $"{status.SpoilerText}<br>{status.Content}";
            status.MediaAttachments.ForEach(w =>
            {
                if (!_mediaLinkRegex.IsMatch(text))
                    return;
                foreach (Match match in _mediaLinkRegex.Matches(text))
                    if (match.Groups["url"].Value == w.TextUrl)
                    {
                        text = text.Replace(match.Value, "");
                        break;
                    }
            });
            Text = text;
        }

        public Status(TwitterStatus status)
        {
            Id = status.Id;
            CreatedAt = status.CreatedAt.ToLocalTime().LocalDateTime;
            User = new User(status.User);
            Type = nameof(Status);
            Provider = ProviderType.Twitter.ToString();
            InReplyToStatusId = status.InReplyToStatusId;
            InReplyToUserId = status.InReplyToUserId;
            ReblogsCount = status.RetweetCount;
            FavoritesCount = status.FavoriteCount;
            if (status.ExtendedEntities?.Media != null)
                Attachments = status.ExtendedEntities.Media.Select(w => new Attachment(w)).ToList();
            else if (status.ExtendedTweet?.Entities?.Media != null)
                Attachments = status.ExtendedTweet.Entities.Media.Select(w => new Attachment(w)).ToList();
            if (status.RetweetedStatus != null)
                RebloggedStatus = new Status(status.RetweetedStatus);
            if (status.QuotedStatus != null)
                QuotedStatus = new Status(status.QuotedStatus);
            Source = status.Source;
            IsSensitiveContent = status.PossiblySensitive ?? false;

            var text = status.ExtendedTweet?.FullText ?? status.Text;
            ParseTcoHyperlinks(status.ExtendedTweet?.Entities?.Urls);
            ParseTcoHyperlinks(status.ExtendedEntities?.Urls);
            ParseTcoHyperlinks(status.Entities?.Urls);
            text = RemoveMediaLinks(text, status.ExtendedTweet?.Entities?.Media);
            text = RemoveMediaLinks(text, status.ExtendedEntities?.Media);
            text = RemoveMediaLinks(text, status.Entities?.Media);

            Text = text;
        }

        private string RemoveMediaLinks(string text, MediaEntity[] entities)
        {
            return entities == null
                ? text
                : entities.Where(entity => !string.IsNullOrWhiteSpace(entity.Url)).Aggregate(text, (current, entity) => current.Replace(entity.Url, ""));
        }

        private void ParseTcoHyperlinks(UrlEntity[] entities)
        {
            if (entities == null)
                return;

            foreach (var entity in entities)
            {
                if (string.IsNullOrWhiteSpace(entity.DisplayUrl))
                    continue;
                if (entity.ExpandedUrl.StartsWith("https://twitter.com/i/web/status/"))
                    continue;
                if (!Hyperlinks.ContainsKey(entity.Url))
                    Hyperlinks.Add(entity.Url, entity.DisplayUrl);
            }
        }

        #region Extended for filters

        /// <summary>
        ///     Has attachment files?
        /// </summary>
        public bool HasAttachments => Attachments.Count > 0;

        /// <summary>
        ///     Has sensitive content flag?
        /// </summary>
        public bool IsSensitiveContent { get; }

        /// <summary>
        ///     Is local status? (Note: Croudia and Twitter always 'true').
        /// </summary>
        public bool IsLocal { get; } = true;

        #endregion

        #region IsReblogged

        private bool _isReblogged;

        /// <summary>
        ///     Is reblogged?
        /// </summary>
        public bool IsReblogged
        {
            get => _isReblogged;
            set => SetProperty(ref _isReblogged, value);
        }

        #endregion

        #region IsFavorited

        private bool _isFavorited;

        /// <summary>
        ///     Is favorited?
        /// </summary>
        public bool IsFavorited
        {
            get => _isFavorited;
            set => SetProperty(ref _isFavorited, value);
        }

        #endregion
    }
}