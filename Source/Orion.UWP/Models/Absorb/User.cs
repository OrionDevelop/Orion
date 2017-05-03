using System;

namespace Orion.UWP.Models.Absorb
{
    /// <summary>
    ///     User object
    /// </summary>
    public class User
    {
        /// <summary>
        ///     ScreenName with instance host.
        /// </summary>
        public string ScreenName { get; }

        /// <summary>
        ///     Icon url
        /// </summary>
        public string Icon { get; }

        /// <summary>
        ///     Initialize with CoreTweet.User
        /// </summary>
        /// <param name="user"></param>
        public User(CoreTweet.User user)
        {
            ScreenName = $"{user.ScreenName}@twitter.com";
            Icon = user.ProfileImageUrlHttps;
        }

        /// <summary>
        ///     Initialize with Croudia.*.User
        /// </summary>
        /// <param name="user"></param>
        public User(Service.Croudia.Models.User user)
        {
            ScreenName = $"{user.ScreenName}@croudia.com";
            Icon = user.ProfileImageUrl;
        }

        /// <summary>
        ///     Initialize with GnuSocial.*.User
        /// </summary>
        /// <param name="user"></param>
        public User(Service.GnuSocial.Models.User user)
        {
            ScreenName = $"{user.ScreenName}@{user.OStatusUri.Host}";
            Icon = user.ProfileImageUrl;
        }

        /// <summary>
        ///     Initialize with Mastodon.*.Account
        /// </summary>
        /// <param name="user"></param>
        public User(Service.Mastodon.Models.Account user)
        {
            ScreenName = $"{user.Acct}";
            if (!user.Acct.Contains("@"))
                ScreenName += $"@{new Uri(user.Url).Host}";
            Icon = user.Avatar;
        }
    }
}