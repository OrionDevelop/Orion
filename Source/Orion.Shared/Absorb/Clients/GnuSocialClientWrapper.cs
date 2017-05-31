using System.Threading.Tasks;

using Orion.Service.GnuSocial;
using Orion.Shared.Absorb.DataSources;
using Orion.Shared.Absorb.Objects;
using Orion.Shared.Models;

namespace Orion.Shared.Absorb.Clients
{
    public class GnuSocialClientWrapper : BaseClientWrapper
    {
        private readonly GnuSocialClient _gnuSocialClient;

        public GnuSocialClientWrapper(Provider provider, Credential credential) : base(provider, credential)
        {
            _gnuSocialClient = new GnuSocialClient(Provider.Host, Provider.ConsumerKey, Provider.ConsumerSecret);
            DataSource = new GnuSocialDataSource(this);
            if (string.IsNullOrWhiteSpace(credential.AccessToken))
                return;

            _gnuSocialClient.AccessToken = Credential.AccessToken;
            _gnuSocialClient.AccessTokenSecret = Credential.AccessTokenSecret;
        }

        public override async Task<string> GetAuthorizedUrlAsync()
        {
            await _gnuSocialClient.OAuth.RequestTokenAsync(SharedConstants.OAuthCallback);
            return _gnuSocialClient.OAuth.GetAuthorizeUrl();
        }

        public override async Task<bool> AuthorizeAsync(string verifier)
        {
            try
            {
                await _gnuSocialClient.OAuth.AccessTokenAsync(verifier);
                Credential.AccessToken = _gnuSocialClient.AccessToken;
                Credential.AccessTokenSecret = _gnuSocialClient.AccessTokenSecret;

                var credential = await _gnuSocialClient.Account.VerifyCredentialsAsync();
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
                var credential = await _gnuSocialClient.Account.VerifyCredentialsAsync();
                Credential.User = new User(credential);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public override async Task UpdateAsync(string body, long? inReplyToStatusId = null)
        {
            await _gnuSocialClient.Statuses.UpdateAsync(body, (int?) inReplyToStatusId);
        }

        public override async Task DestroyAsync(long id)
        {
            await _gnuSocialClient.Statuses.DestroyAsync((int) id);
        }

        public override async Task FavoriteAsync(long id)
        {
            await _gnuSocialClient.Favorites.CreateAsync((int) id);
        }

        public override async Task UnfavoriteAsync(long id)
        {
            await _gnuSocialClient.Favorites.DestroyAsync((int) id);
        }

        public override async Task ReblogAsync(long id)
        {
            await _gnuSocialClient.Statuses.RetweetAsync((int) id);
        }

        public override async Task UnreblogAsync(long id)
        {
            await DestroyAsync(id);
        }
    }
}