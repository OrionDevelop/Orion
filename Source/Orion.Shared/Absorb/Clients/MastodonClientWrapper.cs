using System.Threading.Tasks;

using Orion.Service.Mastodon;
using Orion.Service.Mastodon.Enum;
using Orion.Shared.Absorb.DataSources;
using Orion.Shared.Absorb.Objects;
using Orion.Shared.Models;

namespace Orion.Shared.Absorb.Clients
{
    public class MastodonClientWrapper : BaseClientWrapper
    {
        private const string RedirectUri = "https://kokoiroworks.com/callback";
        private readonly MastodonClient _mastodonClient;

        public MastodonClientWrapper(Provider provider, Credential credential) : base(provider, credential)
        {
            _mastodonClient = new MastodonClient(Provider.Host);
            DataSource = new MastodonDataSource(_mastodonClient);
            if (string.IsNullOrWhiteSpace(credential.AccessToken))
                return;

            _mastodonClient.ClientId = Provider.ClientId;
            _mastodonClient.ClientSecret = Provider.ClientSecret;
            _mastodonClient.AccessToken = Credential.AccessToken;
        }

        public override async Task<string> GetAuthorizedUrlAsync()
        {
            const Scope scopes = Scope.Read | Scope.Write | Scope.Follow;
            await _mastodonClient.Apps.RegisterAsync("Orion", RedirectUri, scopes, "https://ori.kokoiroworks.com");
            return _mastodonClient.OAuth.GetAuthorizeUrl(scopes, RedirectUri);
        }

        public override async Task<bool> AuthorizeAsync(string verifier)
        {
            try
            {
                await _mastodonClient.OAuth.TokenAsync(verifier, RedirectUri);
                Provider.ClientId = _mastodonClient.ClientId;
                Credential.AccessToken = _mastodonClient.AccessToken;
                Credential.AccessTokenSecret = _mastodonClient.AccessTokenSecret;

                var credential = await _mastodonClient.Account.VerifyCredentialsAsync();
                Credential.UserId = credential.Id;
                Credential.User = new User(credential);

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
                var credential = await _mastodonClient.Account.VerifyCredentialsAsync();
                Credential.User = new User(credential);
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
                await _mastodonClient.Statuses.CreateAsync(body, (int?) inReplyToStatusId);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}