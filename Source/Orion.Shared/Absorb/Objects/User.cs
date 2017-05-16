using System;

namespace Orion.Shared.Absorb.Objects
{
    /// <summary>
    ///     User object
    /// </summary>
    public class User
    {
        private readonly Service.Croudia.Models.User _croudiaUser;
        private readonly Service.GnuSocial.Models.User _gnuSocialUser;
        private readonly Service.Mastodon.Models.Account _mastodonUser;
        private readonly CoreTweet.User _twitterUser;

        /// <summary>
        ///     ScreenName with instance host.
        /// </summary>
        public string ScreenName { get; }

        /// <summary>
        ///     ScreenName
        /// </summary>
        public string NormalizedScreenName { get; }

        /// <summary>
        ///     Username
        /// </summary>
        public string Username
            => _croudiaUser?.Name ??
               _gnuSocialUser?.Name ??
               (string.IsNullOrWhiteSpace(_mastodonUser?.DisplayName) ? _mastodonUser?.Username : _mastodonUser?.DisplayName) ??
               _twitterUser.Name;

        /// <summary>
        ///     Location (Mastodon don't have location)
        /// </summary>
        public string Location
            => _croudiaUser?.Location ?? _gnuSocialUser?.Location ?? _twitterUser?.Location ?? "";

        /// <summary>
        ///     Description
        /// </summary>
        public string Description
            => _croudiaUser?.Description ?? _gnuSocialUser?.Description ?? _mastodonUser?.Note ?? _twitterUser.Description;

        /// <summary>
        ///     Icon url
        /// </summary>
        public string Icon
            => _croudiaUser?.ProfileImageUrl ?? _gnuSocialUser?.ProfileImageUrlOriginal ?? _mastodonUser?.Avatar ?? _twitterUser.ProfileImageUrlHttps;

        /// <summary>
        ///     Website
        /// </summary>
        public string Url => _croudiaUser?.Url ?? _gnuSocialUser?.Url ?? _mastodonUser?.Url ?? _twitterUser.Url;

        /// <summary>
        ///     Followers count
        /// </summary>
        public int FollowersCount
            => _croudiaUser?.FollowersCount ?? _gnuSocialUser?.FollowersCount ?? _mastodonUser?.FollowersCount ?? _twitterUser.FollowersCount;

        /// <summary>
        ///     Friends count
        /// </summary>
        public int FriendsCount
            => _croudiaUser?.FriendsCount ?? _gnuSocialUser?.FriendsCount ?? _mastodonUser?.FollowingCount ?? _twitterUser.FriendsCount;

        /// <summary>
        ///     Statuses count
        /// </summary>
        public int StatusesCount
            => _croudiaUser?.StatusesCount ?? _gnuSocialUser?.StatusesCount ?? _mastodonUser?.StatuesCount ?? _twitterUser.StatusesCount;

        /// <summary>
        ///     Background image
        /// </summary>
        public string Cover
            => _croudiaUser?.CoverImageUrl ?? _gnuSocialUser?.CoverPhoto ?? _mastodonUser?.Header ?? _twitterUser.ProfileBackgroundImageUrlHttps;

        /// <summary>
        ///     Favorites count (Mastodon don't have favorites count)
        /// </summary>
        public int FavoritesCount
            => _croudiaUser?.FavoritesCount ?? _gnuSocialUser?.FavouritesCount ?? _twitterUser?.FavouritesCount ?? 0;

        /// <summary>
        ///     Initialize with Croudia.*.User
        /// </summary>
        /// <param name="user"></param>
        public User(Service.Croudia.Models.User user)
        {
            _croudiaUser = user;
            ScreenName = $"{user.ScreenName}@croudia.com";
            NormalizedScreenName = user.ScreenName;
        }

        /// <summary>
        ///     Initialize with GnuSocial.*.User
        /// </summary>
        /// <param name="user"></param>
        public User(Service.GnuSocial.Models.User user)
        {
            _gnuSocialUser = user;
            ScreenName = $"{user.ScreenName}@{user.OStatusUri.Host}";
            NormalizedScreenName = user.ScreenName;
        }

        /// <summary>
        ///     Initialize with Mastodon.*.Account
        /// </summary>
        /// <param name="user"></param>
        public User(Service.Mastodon.Models.Account user)
        {
            _mastodonUser = user;
            ScreenName = $"{user.Acct}";
            if (!user.Acct.Contains("@"))
                ScreenName += $"@{new Uri(user.Url).Host}";
            NormalizedScreenName = user.Acct;
        }

        /// <summary>
        ///     Initialize with CoreTweet.User
        /// </summary>
        /// <param name="user"></param>
        public User(CoreTweet.User user)
        {
            _twitterUser = user;
            ScreenName = $"{user.ScreenName}@twitter.com";
            NormalizedScreenName = user.ScreenName;
        }
    }
}