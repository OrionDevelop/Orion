using System.Threading.Tasks;

using CoreTweet;

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
            if (credential != null)
                _twitterClient = Tokens.Create(Provider.ConsumerKey, Provider.ConsumerSecret, Credential.AccessToken, Credential.AccessTokenSecret);
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

        public override async Task<bool> UpdateAsync(string body, long? inReplyToStatusId = null)
        {
            try
            {
                await _twitterClient.Statuses.UpdateAsync(body, inReplyToStatusId);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}