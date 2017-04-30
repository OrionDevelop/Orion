using System.Threading.Tasks;

using Orion.Service.Croudia;

namespace Orion.UWP.Models.Clients
{
    internal class CroudiaClientWrapper : BaseClientWrapper
    {
        private readonly CroudiaClient _croudiaClient;

        public CroudiaClientWrapper(Provider provider) : base(provider)
        {
            _croudiaClient = new CroudiaClient(provider.ClientId, provider.ClientSecret);
        }

        public CroudiaClientWrapper(Account account) : base(account)
        {
            _croudiaClient = new CroudiaClient(Provider.ClientId, Provider.ClientSecret)
            {
                AccessToken = account.Credential.AccessToken,
                AccessTokenSecret = account.Credential.AccessTokenSecret,
                RefreshToken = account.Credential.RefreshToken
            };
        }

        public override Task<string> GetAuthorizeUrlAsync()
        {
            return Task.FromResult(_croudiaClient.OAuth.GetAuthorizeUrl());
        }

        public override async Task<bool> AuthorizeAsync(string verifier)
        {
            try
            {
                await _croudiaClient.OAuth.AccessTokenAsync(verifier);
                Account.Credential.AccessToken = _croudiaClient.AccessToken;
                Account.Credential.AccessTokenSecret = _croudiaClient.AccessTokenSecret;
                Account.Credential.RefreshToken = _croudiaClient.RefreshToken;

                var user = await _croudiaClient.Account.VerifyCredentialsAsync();
                Account.Credential.Username = $"{user.ScreenName}@croudia.com";

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
                await _croudiaClient.OAuth.RefreshTokenAsync();
                await _croudiaClient.Account.VerifyCredentialsAsync();
                return true;
            }
            catch
            {
                // Revoke access permission, invalid credentials or service unavailable (Croudia API don't trusted).
                return false;
            }
        }
    }
}