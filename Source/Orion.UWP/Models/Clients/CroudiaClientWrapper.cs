using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

                var user = await _croudiaClient.Account.VerifyCredentialsAsync();
                Account.Credential.Username = $"{user.ScreenName}@croudia.com";

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}