using System.Threading.Tasks;

using Orion.Service.GnuSocial;

namespace Orion.UWP.Models.Clients
{
    internal class GnuSocialClientWrapper : BaseClientWrapper
    {
        private readonly GnuSocialClient _gnuSocialClient;

        public GnuSocialClientWrapper(Provider provider) : base(provider)
        {
            _gnuSocialClient = new GnuSocialClient(provider.Host, provider.ClientId, provider.ClientSecret);
        }

        public override async Task<string> GetAuthorizeUrlAsync()
        {
            await _gnuSocialClient.OAuth.RequestTokenAsync("oob");
            return _gnuSocialClient.OAuth.GetAuthorizeUrl();
        }

        public override async Task<bool> AuthorizeAsync(string verifier)
        {
            try
            {
                await _gnuSocialClient.OAuth.AccessTokenAsync(verifier);
                Account.Credential.AccessToken = _gnuSocialClient.AccessToken;
                Account.Credential.AccessTokenSecret = _gnuSocialClient.AccessTokenSecret;

                var user = await _gnuSocialClient.Account.VerifyCredentialsAsync();
                Account.Credential.Username = $"{user.ScreenName}@{Provider.Host}";

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}