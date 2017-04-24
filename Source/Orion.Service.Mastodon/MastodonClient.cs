using System.Collections.Generic;

using Orion.Service.Mastodon.Clients;
using Orion.Service.Shared;

// ReSharper disable PossibleMultipleEnumeration

namespace Orion.Service.Mastodon
{
    /// <summary>
    ///     Mastodon API Wrapper
    ///     https://github.com/tootsuite/mastodon
    /// </summary>
    public class MastodonClient : ApplicationClient
    {
        public AccountClient Account { get; }
        public AppsClient Apps { get; }
        public BlocksClient Blocks { get; }
        public FavouritesClient Favourites { get; }
        public FollowRequestsClient FollowRequests { get; }
        public FollowsClient Follows { get; }
        public InstanceClient Instance { get; }
        public MediaClient Media { get; }
        public MutesClient Mutes { get; }
        public NotificationsClient Notifications { get; }
        public OAuthClient OAuth { get; }
        public ReportsClient Reports { get; }
        public SearchClient Search { get; }
        public StatusesClient Statuses { get; }
        public TimelinesClient Timelines { get; }

        public MastodonClient(string domain) : base(domain, AuthenticateType.OAuth2)
        {
            BinaryParameters = new List<string> {"file"};

            Account = new AccountClient(this);
            Apps = new AppsClient(this);
            Blocks = new BlocksClient(this);
            Favourites = new FavouritesClient(this);
            FollowRequests = new FollowRequestsClient(this);
            Follows = new FollowsClient(this);
            Instance = new InstanceClient(this);
            Media = new MediaClient(this);
            Mutes = new MutesClient(this);
            Notifications = new NotificationsClient(this);
            OAuth = new OAuthClient(this);
            Reports = new ReportsClient(this);
            Search = new SearchClient(this);
            Statuses = new StatusesClient(this);
            Timelines = new TimelinesClient(this);
        }
    }
}