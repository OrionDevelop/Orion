using System.Threading.Tasks;

using CoreTweet;

namespace Orion.UWP.Models.Clients
{
    internal class TwitterClientWrapper : BaseClientWrapper
    {
        private OAuth.OAuthSession _session;
        private Tokens _twitterClient;

        public TwitterClientWrapper(Provider provider) : base(provider) { }

        public TwitterClientWrapper(Account account) : base(account)
        {
            _twitterClient = Tokens.Create(Provider.ClientId, Provider.ClientSecret, account.Credential.AccessToken, account.Credential.AccessTokenSecret);
        }

        public override async Task<string> GetAuthorizeUrlAsync()
        {
            _session = await OAuth.AuthorizeAsync(Provider.ClientId, Provider.ClientSecret);
            return _session.AuthorizeUri.ToString();
        }

        public override async Task<bool> AuthorizeAsync(string verifier)
        {
            try
            {
                _twitterClient = await _session.GetTokensAsync(verifier);
                Account.Credential.AccessToken = _twitterClient.AccessToken;
                Account.Credential.AccessTokenSecret = _twitterClient.AccessTokenSecret;
                Account.Credential.Username = $"{_twitterClient.ScreenName}@twitter.com";

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
                var user = await _twitterClient.Account.VerifyCredentialsAsync();
                Account.Credential.Username = $"{user.ScreenName}@twitter.com";
                return true;
            }
            catch
            {
                // Revoke access permission or invalid credentials.
                return false;
            }
        }
    }
}