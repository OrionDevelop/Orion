using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

using Orion.Shared.Enums;
using Orion.Shared.Models;

namespace Orion.Shared
{
    public static class SharedConstants
    {
        public static string OAuthCallback = "https://kokoiroworks.com/callback";

        #region Timeline presets

        #region Twitter

        public static TimelinePreset TwitterHomeTimelinePreset = new TimelinePreset
        {
            Name = "Home",
            ProviderType = ProviderType.Twitter,
            Query = "FROM *"
        };

        public static TimelinePreset TwitterMentionsTimelinePreset = new TimelinePreset
        {
            Name = "Mentions",
            ProviderType = ProviderType.Twitter,
            Query = "FROM mentions"
        };

        public static TimelinePreset TwitterDirectMessagesTimelinePreset = new TimelinePreset
        {
            Name = "Direct messages",
            ProviderType = ProviderType.Twitter,
            Query = "FROM messages"
        };

        public static TimelinePreset TwitterNotificationTimelinePreset = new TimelinePreset
        {
            Name = "Notifications",
            ProviderType = ProviderType.Twitter,
            Query = "FROM notifications"
        };

        #endregion

        #region Croudia

        public static TimelinePreset CroudiaHomeTimelinePreset = new TimelinePreset
        {
            Name = "Home",
            ProviderType = ProviderType.Croudia,
            Query = "FROM * WHERE User.IsFollowing"
        };

        public static TimelinePreset CroudiaPublicTimelinePreset = new TimelinePreset
        {
            Name = "Public",
            ProviderType = ProviderType.Croudia,
            Query = "FROM *"
        };

        public static TimelinePreset CroudiaMentionsTimelinePreset = new TimelinePreset
        {
            Name = "Mentions",
            ProviderType = ProviderType.Croudia,
            Query = "FROM mentions"
        };

        public static TimelinePreset CroudiaDirectMessagesTimelinePreset = new TimelinePreset
        {
            Name = "Direct messages",
            ProviderType = ProviderType.Twitter,
            Query = "FROM messages"
        };

        #endregion

        #region GNU social

        public static TimelinePreset GnuSocialHomeTimelinePreset = new TimelinePreset
        {
            Name = "Home",
            ProviderType = ProviderType.GnuSocial,
            Query = "FROM * WHERE User.IsFollowing"
        };

        public static TimelinePreset GnuSocialLocalTimelinePreset = new TimelinePreset
        {
            Name = "Local",
            ProviderType = ProviderType.GnuSocial,
            Query = "FROM * WHERE User.IsLocal"
        };

        public static TimelinePreset GnuSocialFederatedTimelinePreset = new TimelinePreset
        {
            Name = "Federated",
            ProviderType = ProviderType.GnuSocial,
            Query = "FROM *"
        };

        public static TimelinePreset GnuSocialMentionsTimelinePreset = new TimelinePreset
        {
            Name = "Mentions",
            ProviderType = ProviderType.Croudia,
            Query = "FROM mentions"
        };

        #endregion

        #region Mastodon

        public static TimelinePreset MastodonHomeTimelinePreset = new TimelinePreset
        {
            Name = "Home",
            ProviderType = ProviderType.Mastodon,
            Query = "FROM * WHERE User.IsFollowing"
        };

        public static TimelinePreset MastodonLocalTimelinePreset = new TimelinePreset
        {
            Name = "Local",
            ProviderType = ProviderType.Mastodon,
            Query = "FROM * WHERE User.IsLocal"
        };

        public static TimelinePreset MastodonFederatedTimelinePreset = new TimelinePreset
        {
            Name = "Federated",
            ProviderType = ProviderType.Mastodon,
            Query = "FROM *"
        };

        public static TimelinePreset MastodonMentionsTimelinePreset = new TimelinePreset
        {
            Name = "Mentions",
            ProviderType = ProviderType.Mastodon,
            Query = "FROM mentions"
        };

        public static TimelinePreset MastodonNotificationsTimelinePreset = new TimelinePreset
        {
            Name = "Notifications",
            ProviderType = ProviderType.Mastodon,
            Query = "FROM notifications"
        };

        #endregion

        public static ReadOnlyCollection<TimelinePreset> TimelinePresets { get; } = new List<TimelinePreset>
        {
            TwitterHomeTimelinePreset,
            TwitterMentionsTimelinePreset,
            TwitterDirectMessagesTimelinePreset,
            TwitterNotificationTimelinePreset,
            CroudiaHomeTimelinePreset,
            CroudiaPublicTimelinePreset,
            CroudiaMentionsTimelinePreset,
            CroudiaDirectMessagesTimelinePreset,
            GnuSocialHomeTimelinePreset,
            GnuSocialLocalTimelinePreset,
            GnuSocialFederatedTimelinePreset,
            GnuSocialMentionsTimelinePreset,
            MastodonHomeTimelinePreset,
            MastodonLocalTimelinePreset,
            MastodonFederatedTimelinePreset,
            MastodonMentionsTimelinePreset,
            MastodonNotificationsTimelinePreset
        }.AsReadOnly();

        #endregion

        #region Providers

        public static Provider TwitterProvider { get; } = new Provider
        {
            Name = "Twitter",
            Host = "api.twitter.com",
            ProviderType = ProviderType.Twitter,
            IsRequireHost = false,
            IsRequireApiKeys = false,
            ConsumerKey = "IUWEAzTZJLcmfB7RFVErvVyLM",
            ConsumerSecret = "bgJDN2WfJwzMZUhWK5lVHp8NklqIOKZ6f5ZscrlrzxPz87BbBf",
            UrlParseRegex = new Regex(@"oauth_verifier=(?<verifier>[A-Za-z0-9]+)", RegexOptions.Compiled)
        };

        public static Provider CroudiaProvider { get; } = new Provider
        {
            Name = "Croudia",
            Host = "api.croudia.com",
            ProviderType = ProviderType.Croudia,
            IsRequireHost = false,
            IsRequireApiKeys = false,
            ClientId = "a278d96eb670a7008c057191a915e4b8b23532427f229eafa925612ad574bd4f",
            ClientSecret = "df93f525140c43dbe701b29e7819877bf59fa11f66f49295f5be4fdbe03317e3",
            UrlParseRegex = new Regex(@"\?code=(?<verifier>[0-9a-z]+)", RegexOptions.Compiled)
        };

        public static Provider FreezePeachProvider { get; } = new Provider
        {
            Name = "FreezePeach",
            Host = "freezepeach.xyz",
            ProviderType = ProviderType.GnuSocial,
            IsRequireHost = false,
            IsRequireApiKeys = false,
            ConsumerKey = "4625863928048353df5cf80df5880ca5",
            ConsumerSecret = "d9a98e70a14751ab2315549b4b493e94",
            UrlParseRegex = new Regex(@"oauth_verifier=(?<verifier>[a-z0-9]+)", RegexOptions.Compiled)
        };

        public static Provider GnuSmugProvider { get; } = new Provider
        {
            Name = "GNU/Smug",
            Host = "gs.smuglo.li",
            ProviderType = ProviderType.GnuSocial,
            IsRequireHost = false,
            IsRequireApiKeys = false,
            ConsumerKey = "96ad60799296c03da82f63e584507b2d",
            ConsumerSecret = "1729201fa07fad023fed97926241f212",
            UrlParseRegex = new Regex(@"oauth_verifier=(?<verifier>[a-z0-9]+)", RegexOptions.Compiled)
        };

        public static ReadOnlyCollection<Provider> Providers { get; } = new List<Provider>
        {
            TwitterProvider,
            CroudiaProvider,
            FreezePeachProvider,
            GnuSmugProvider,
            new Provider
            {
                Name = "GNU social",
                ProviderType = ProviderType.GnuSocial,
                IsRequireHost = true,
                IsRequireApiKeys = true,
                UrlParseRegex = new Regex(@"oauth_verifier=(?<verifier>[a-z0-9]+)", RegexOptions.Compiled)
            },
            new Provider
            {
                Name = "Mastodon",
                ProviderType = ProviderType.Mastodon,
                IsRequireHost = true,
                IsRequireApiKeys = false,
                UrlParseRegex = new Regex(@"code=(?<verifier>[0-9a-z]+)", RegexOptions.Compiled)
            }
        }.AsReadOnly();

        #endregion
    }
}