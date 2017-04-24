using System.Collections.Generic;

using Orion.Service.GnuSocial.Clients;
using Orion.Service.Shared;

namespace Orion.Service.GnuSocial
{
    public class GnuSocialClient : ApplicationClient
    {
        public AccountClient Account { get; }
        public BlocksClient Blocks { get; }
        public DirectMessagesClient DirectMessages { get; }
        public FavoritesClient Favorites { get; }
        public FollowersClient Followers { get; }
        public FriendsClient Friends { get; }
        public FriendshipsClient Friendships { get; }
        public NotificationsClient Notifications { get; }
        public OAuthClient OAuth { get; }
        public StatusesClient Statuses { get; }
        public StatusNetClient StatusNet { get; }
        public UsersClient Users { get; }

        public GnuSocialClient(string domain, string consumerKey, string consumerSecret) : base($"{domain}/api", AuthenticateType.OAuth10A)
        {
            BinaryParameters = new List<string> {"media"};
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;

            Account = new AccountClient(this);
            Blocks = new BlocksClient(this);
            DirectMessages = new DirectMessagesClient(this);
            Favorites = new FavoritesClient(this);
            Followers = new FollowersClient(this);
            Friends = new FriendsClient(this);
            Friendships = new FriendshipsClient(this);
            Notifications = new NotificationsClient(this);
            OAuth = new OAuthClient(this);
            Statuses = new StatusesClient(this);
            StatusNet = new StatusNetClient(this);
            Users = new UsersClient(this);
        }
    }
}