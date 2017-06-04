using System.Threading.Tasks;

using CoreTweet;

using Orion.Shared.Absorb.DataSources;
using Orion.Shared.Models;

using User = Orion.Shared.Absorb.Objects.User;

namespace Orion.Shared.Absorb.Clients
{
    public class TwitterClientWrapper : BaseClientWrapper
    {
        private OAuth.OAuthSession _session;
        private Tokens _twitterClient;

        public TwitterClientWrapper(Provider provider, Credential credential) : base(provider, credential)
        {
            if (!string.IsNullOrWhiteSpace(credential.AccessToken))
            {
                _twitterClient = Tokens.Create(Provider.ConsumerKey, Provider.ConsumerSecret, Credential.AccessToken, Credential.AccessTokenSecret);
                DataSource = new TwitterDataSource(_twitterClient);
            }
        }

        public override async Task<string> GetAuthorizedUrlAsync()
        {
            _session = await OAuth.AuthorizeAsync(Provider.ConsumerKey, Provider.ConsumerSecret, SharedConstants.OAuthCallback);
            return _session.AuthorizeUri.ToString();
        }

        public override async Task<bool> AuthorizeAsync(string verifier)
        {
            try
            {
                _twitterClient = await _session.GetTokensAsync(verifier);
                DataSource = new TwitterDataSource(_twitterClient);
                Credential.AccessToken = _twitterClient.AccessToken;
                Credential.AccessTokenSecret = _twitterClient.AccessTokenSecret;
                Credential.UserId = _twitterClient.UserId;
                Credential.User = new User(await _twitterClient.Account.VerifyCredentialsAsync());
                return true;
            }
            catch
            {
                return false;
            }
        }

        public override async Task<bool> RefreshAccountAsync()
        {
            try
            {
                Credential.User = new User(await _twitterClient.Account.VerifyCredentialsAsync());
                return true;
            }
            catch
            {
                return false;
            }
        }

        public override async Task UpdateAsync(string body, long? inReplyToStatusId = null)
        {
            await _twitterClient.Statuses.UpdateAsync(body, inReplyToStatusId);
        }

        public override async Task DestroyAsync(long id)
        {
            await _twitterClient.Statuses.DestroyAsync(id);
        }

        public override async Task FavoriteAsync(long id)
        {
            await _twitterClient.Favorites.CreateAsync(id);
        }

        public override async Task UnfavoriteAsync(long id)
        {
            await _twitterClient.Favorites.DestroyAsync(id);
        }

        public override async Task ReblogAsync(long id)
        {
            await _twitterClient.Statuses.RetweetAsync(id);
        }

        public override async Task UnreblogAsync(long id)
        {
            await DestroyAsync(id);
        }
    }
}