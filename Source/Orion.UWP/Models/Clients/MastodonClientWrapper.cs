using System.Threading.Tasks;

using Orion.Service.Mastodon;
using Orion.Service.Mastodon.Enum;

namespace Orion.UWP.Models.Clients
{
    internal class MastodonClientWrapper : BaseClientWrapper
    {
        private readonly MastodonClient _mastodonClient;

        public MastodonClientWrapper(Provider provider) : base(provider)
        {
            _mastodonClient = new MastodonClient(provider.Host);
        }

        public override async Task<string> GetAuthorizeUrlAsync()
        {
            const Scope scopes = Scope.Read | Scope.Write | Scope.Follow;
            await _mastodonClient.Apps.RegisterAsync("Orion", "urn:ietf:wg:oauth:2.0:oob", scopes);
            return _mastodonClient.OAuth.GetAuthorizeUrl(scopes);
        }

        public override async Task<bool> AuthorizeAsync(string verifier)
        {
            try
            {
                await _mastodonClient.OAuth.TokenAsync(verifier);
                Account.Credential.AccessToken = _mastodonClient.AccessToken;
                Account.Credential.AccessTokenSecret = _mastodonClient.AccessTokenSecret;

                var user = await _mastodonClient.Account.VerifyCredentialsAsync();
                Account.Credential.Username = user.Acct;

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}