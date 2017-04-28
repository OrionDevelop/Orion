using System.Collections.Generic;

using Orion.Service.Croudia.Clients;
using Orion.Service.Shared;

namespace Orion.Service.Croudia
{
    public class CroudiaClient : ApplicationClient
    {
        public AccountClient Account { get; }
        public BlocksClient Blocks { get; }
        public FavoritesClient Favorites { get; }
        public FollowersClient Followers { get; }
        public FriendsClient Friends { get; }
        public FriendshipsClient Friendships { get; }
        public MutesClient Mutes { get; }
        public OAuthClient OAuth { get; }
        public SearchClient Search { get; }
        public SecretMailsClient SecretMails { get; }
        public StatusesClient Statuses { get; }
        public TrendsClient Trends { get; }
        public UsersClient Users { get; }

        public CroudiaClient(string clientId, string clientSecret) : base("api.croudia.com", AuthenticateType.OAuth2)
        {
            BinaryParameters = new List<string> {"image", "media"};
            ClientId = clientId;
            ClientSecret = clientSecret;

            Account = new AccountClient(this);
            Blocks = new BlocksClient(this);
            Favorites = new FavoritesClient(this);
            Followers = new FollowersClient(this);
            Friends = new FriendsClient(this);
            Friendships = new FriendshipsClient(this);
            Mutes = new MutesClient(this);
            OAuth = new OAuthClient(this);
            Search = new SearchClient(this);
            SecretMails = new SecretMailsClient(this);
            Statuses = new StatusesClient(this);
            Trends = new TrendsClient(this);
            Users = new UsersClient(this);
        }
    }
}