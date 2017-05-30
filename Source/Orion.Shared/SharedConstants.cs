using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

using Orion.Shared.Enums;
using Orion.Shared.Models;

namespace Orion.Shared
{
    public static class SharedConstants
    {
        public static readonly string OAuthCallback = "https://kokoiroworks.com/callback";

        #region Timeline presets

        #region Twitter

        public static readonly TimelinePreset TwitterHomeTimelinePreset = new TimelinePreset
        {
            Name = "Home",
            ProviderType = ProviderType.Twitter,
            Query = "FROM *"
        };

        public static readonly TimelinePreset TwitterMentionsTimelinePreset = new TimelinePreset
        {
            Name = "Mentions",
            ProviderType = ProviderType.Twitter,
            Query = "FROM mentions"
        };

        public static readonly TimelinePreset TwitterDirectMessagesTimelinePreset = new TimelinePreset
        {
            Name = "Direct messages",
            ProviderType = ProviderType.Twitter,
            Query = "FROM messages"
        };

        public static readonly TimelinePreset TwitterNotificationTimelinePreset = new TimelinePreset
        {
            Name = "Notifications",
            ProviderType = ProviderType.Twitter,
            Query = "FROM notifications"
        };

        public static readonly TimelinePreset TwitterCustomTimelinePreset = new TimelinePreset
        {
            Name = "Custom",
            ProviderType = ProviderType.Twitter,
            Query = "",
            IsEditable = true
        };

        #endregion

        #region Croudia

        public static readonly TimelinePreset CroudiaHomeTimelinePreset = new TimelinePreset
        {
            Name = "Home",
            ProviderType = ProviderType.Croudia,
            Query = "FROM * WHERE User.IsFollowing"
        };

        public static readonly TimelinePreset CroudiaPublicTimelinePreset = new TimelinePreset
        {
            Name = "Public",
            ProviderType = ProviderType.Croudia,
            Query = "FROM *"
        };

        public static readonly TimelinePreset CroudiaMentionsTimelinePreset = new TimelinePreset
        {
            Name = "Mentions",
            ProviderType = ProviderType.Croudia,
            Query = "FROM mentions"
        };

        public static readonly TimelinePreset CroudiaDirectMessagesTimelinePreset = new TimelinePreset
        {
            Name = "Direct messages",
            ProviderType = ProviderType.Twitter,
            Query = "FROM messages"
        };

        public static readonly TimelinePreset CroudiaCustomTimelinePreset = new TimelinePreset
        {
            Name = "Custom",
            ProviderType = ProviderType.Croudia,
            Query = "",
            IsEditable = true
        };

        #endregion

        #region GNU social

        public static readonly TimelinePreset GnuSocialHomeTimelinePreset = new TimelinePreset
        {
            Name = "Home",
            ProviderType = ProviderType.GnuSocial,
            Query = "FROM * WHERE User.IsFollowing"
        };

        public static readonly TimelinePreset GnuSocialLocalTimelinePreset = new TimelinePreset
        {
            Name = "Local",
            ProviderType = ProviderType.GnuSocial,
            Query = "FROM * WHERE User.IsLocal"
        };

        public static readonly TimelinePreset GnuSocialFederatedTimelinePreset = new TimelinePreset
        {
            Name = "Federated",
            ProviderType = ProviderType.GnuSocial,
            Query = "FROM *"
        };

        public static readonly TimelinePreset GnuSocialMentionsTimelinePreset = new TimelinePreset
        {
            Name = "Mentions",
            ProviderType = ProviderType.Croudia,
            Query = "FROM mentions"
        };

        public static readonly TimelinePreset GnuSocialCustomTimelinePreset = new TimelinePreset
        {
            Name = "Custom",
            ProviderType = ProviderType.GnuSocial,
            Query = "",
            IsEditable = true
        };

        #endregion

        #region Mastodon

        public static readonly TimelinePreset MastodonHomeTimelinePreset = new TimelinePreset
        {
            Name = "Home",
            ProviderType = ProviderType.Mastodon,
            Query = "FROM home"
        };

        public static readonly TimelinePreset MastodonLocalTimelinePreset = new TimelinePreset
        {
            Name = "Local",
            ProviderType = ProviderType.Mastodon,
            Query = "FROM public"
        };

        public static readonly TimelinePreset MastodonFederatedTimelinePreset = new TimelinePreset
        {
            Name = "Federated",
            ProviderType = ProviderType.Mastodon,
            Query = "FROM *"
        };

        public static readonly TimelinePreset MastodonMentionsTimelinePreset = new TimelinePreset
        {
            Name = "Mentions",
            ProviderType = ProviderType.Mastodon,
            Query = "FROM mentions"
        };

        public static readonly TimelinePreset MastodonNotificationsTimelinePreset = new TimelinePreset
        {
            Name = "Notifications",
            ProviderType = ProviderType.Mastodon,
            Query = "FROM notifications"
        };

        public static readonly TimelinePreset MastodonCustomTimelinePreset = new TimelinePreset
        {
            Name = "Custom",
            ProviderType = ProviderType.Mastodon,
            Query = "",
            IsEditable = true
        };

        #endregion

        public static ReadOnlyCollection<TimelinePreset> TimelinePresets { get; } = new List<TimelinePreset>
        {
            TwitterHomeTimelinePreset,
            TwitterMentionsTimelinePreset,
            TwitterDirectMessagesTimelinePreset,
            TwitterNotificationTimelinePreset,
            TwitterCustomTimelinePreset,
            CroudiaHomeTimelinePreset,
            CroudiaPublicTimelinePreset,
            CroudiaMentionsTimelinePreset,
            CroudiaDirectMessagesTimelinePreset,
            CroudiaCustomTimelinePreset,
            GnuSocialHomeTimelinePreset,
            GnuSocialLocalTimelinePreset,
            GnuSocialFederatedTimelinePreset,
            GnuSocialMentionsTimelinePreset,
            GnuSocialCustomTimelinePreset,
            MastodonHomeTimelinePreset,
            MastodonLocalTimelinePreset,
            MastodonFederatedTimelinePreset,
            MastodonMentionsTimelinePreset,
            MastodonNotificationsTimelinePreset,
            MastodonCustomTimelinePreset
        }.AsReadOnly();

        #endregion

        #region Providers

        public static Provider TwitterProvider { get; } = new Provider
        {
            Name = "Twitter",
            Host = "twitter.com",
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
            Host = "croudia.com",
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