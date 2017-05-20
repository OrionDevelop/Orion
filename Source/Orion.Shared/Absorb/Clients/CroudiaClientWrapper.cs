using System.Threading.Tasks;

using Orion.Service.Croudia;
using Orion.Shared.Absorb.DataSources;
using Orion.Shared.Absorb.Objects;
using Orion.Shared.Models;

namespace Orion.Shared.Absorb.Clients
{
    public class CroudiaClientWrapper : BaseClientWrapper
    {
        private readonly CroudiaClient _croudiaClient;

        public CroudiaClientWrapper(Provider provider, Credential credential) : base(provider, credential)
        {
            _croudiaClient = new CroudiaClient(Provider.ClientId, Provider.ClientSecret);
            DataSource = new CroudiaDataSource(this);
            if (string.IsNullOrWhiteSpace(credential.AccessToken))
                return;

            _croudiaClient.AccessToken = Credential.AccessToken;
            _croudiaClient.RefreshToken = Credential.RefreshToken;
        }

        public override Task<string> GetAuthorizedUrlAsync()
        {
            return Task.FromResult(_croudiaClient.OAuth.GetAuthorizeUrl());
        }

        public override async Task<bool> AuthorizeAsync(string verifier)
        {
            try
            {
                await _croudiaClient.OAuth.AccessTokenAsync(verifier);
                Credential.AccessToken = _croudiaClient.AccessToken;
                Credential.RefreshToken = _croudiaClient.RefreshToken;

                var credentials = await _croudiaClient.Account.VerifyCredentialsAsync();
                Credential.UserId = credentials.Id;
                Credential.User = new User(await _croudiaClient.Account.VerifyCredentialsAsync());

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
                Credential.AccessToken = _croudiaClient.AccessToken;
                Credential.AccessTokenSecret = _croudiaClient.AccessTokenSecret;
                Credential.RefreshToken = _croudiaClient.RefreshToken;
                Credential.User = new User(await _croudiaClient.Account.VerifyCredentialsAsync());

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
                await _croudiaClient.Statuses.UpdateAsync(body, (int?) inReplyToStatusId);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}