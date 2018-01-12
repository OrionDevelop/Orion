using System;

using CoreTweet;

using GnuSocialUser = Orion.Service.GnuSocial.Models.User;
using MastodonUser = Orion.Service.Mastodon.Models.Account;
using TwitterUser = CoreTweet.User;

namespace Orion.Shared.Absorb.Objects
{
    /// <summary>
    ///     User
    /// </summary>
    public class User
    {
        private readonly GnuSocialUser _gnuSocialUser;
        private readonly MastodonUser _mastodonUser;
        private readonly TwitterUser _twitterUser;

        /// <summary>
        ///     ID
        /// </summary>
        public long? Id => _gnuSocialUser?.Id ?? _mastodonUser?.Id ?? _twitterUser.Id;

        /// <summary>
        ///     Name
        /// </summary>
        public string Name =>  _gnuSocialUser?.Name ??
                              (!string.IsNullOrWhiteSpace(_mastodonUser?.DisplayName) ? _mastodonUser.DisplayName : null) ??
                              (!string.IsNullOrWhiteSpace(_mastodonUser?.Username) ? _mastodonUser.Username : null) ?? _twitterUser.Name;

        /// <summary>
        ///     Screen name
        /// </summary>
        public string ScreenName =>_gnuSocialUser?.ScreenName ?? _mastodonUser?.Acct ?? _twitterUser.ScreenName;

        /// <summary>
        ///     Screen name with hostname
        /// </summary>
        public string ScreenNameWithHost { get; }

        /// <summary>
        ///     Icon url
        /// </summary>
        public string IconUrl => _gnuSocialUser?.ProfileImageUrlOriginal ?? _mastodonUser?.Avatar ?? _twitterUser.GetProfileImageUrlHttps("bigger").ToString();

        /// <summary>
        ///     Background image url
        /// </summary>
        public string BackgroundUrl =>  _gnuSocialUser?.ProfileBannerUrl ?? _mastodonUser?.HeaderStatic ?? _twitterUser.ProfileBannerUrl;

        /// <summary>
        ///     Description
        /// </summary>
        public string Description =>  _gnuSocialUser?.Description ?? _mastodonUser?.Note ?? _twitterUser.Description;

        /// <summary>
        ///     Location
        /// </summary>
        public string Location => _gnuSocialUser?.Location ?? _twitterUser.Location;

        /// <summary>
        ///     Url
        /// </summary>
        public string Url =>  _gnuSocialUser?.Url ?? _mastodonUser?.Url ?? _twitterUser.Url;

        /// <summary>
        ///     Statuses count
        /// </summary>
        public long StatusesCount => _gnuSocialUser?.StatusesCount ?? _mastodonUser?.StatuesCount ?? _twitterUser.StatusesCount;

        /// <summary>
        ///     Favorites count
        /// </summary>
        public long FavoritesCount => _gnuSocialUser?.FavouritesCount ?? _twitterUser?.FavouritesCount ?? 0;

        /// <summary>
        ///     Followers count
        /// </summary>
        public long FollowersCount =>  _gnuSocialUser?.FollowersCount ?? _mastodonUser?.FollowersCount ?? _twitterUser.FollowersCount;

        /// <summary>
        ///     Following count
        /// </summary>
        public long FriendsCount => _gnuSocialUser?.FriendsCount ?? _mastodonUser?.FollowingCount ?? _twitterUser.FriendsCount;

        /// <summary>
        ///     Protected or No
        /// </summary>
        public bool IsProtected =>  _gnuSocialUser?.IsProtected ?? _mastodonUser?.IsLocked ?? _twitterUser.IsProtected;

        /// <summary>
        ///     Local user
        /// </summary>
        public bool IsLocal =>
            _gnuSocialUser?.IsLocal ?? _mastodonUser?.Acct?.Contains("@").Equals(false) ?? true;

        /// <summary>
        ///     Following?
        /// </summary>
        public bool IsFollowing =>  _gnuSocialUser?.IsFollowing ?? false;

        /// <summary>
        ///     Is verified user?
        /// </summary>
        public bool IsVerified => _twitterUser?.IsVerified ?? false;

        public User(GnuSocialUser user)
        {
            _gnuSocialUser = user;
            ScreenNameWithHost = $"{user.ScreenName}@{new Uri(user.StatusnetProfileUrl).Host}";
        }

        public User(MastodonUser user)
        {
            _mastodonUser = user;
            ScreenNameWithHost = ScreenName.Contains("@") ? ScreenName : $"{user.Acct}@{new Uri(user.Url).Host}";
        }

        public User(TwitterUser user)
        {
            _twitterUser = user;
            ScreenNameWithHost = $"{user.ScreenName}@twitter.com";
        }
    }
}