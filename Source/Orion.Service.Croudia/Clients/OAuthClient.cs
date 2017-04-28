using System.Collections.Generic;
using System.Threading.Tasks;

using Orion.Service.Croudia.Models;
using Orion.Service.Shared;
using Orion.Service.Shared.Extensions;

namespace Orion.Service.Croudia.Clients
{
    public class OAuthClient : ApiClient<CroudiaClient>
    {
        internal OAuthClient(CroudiaClient client) : base(client) { }

        public string GetAuthorizeUrl(string state = null)
        {
            var url = $"{AppClient.BaseUrl}oauth/authorize?response_type=code&client_id={AppClient.ClientId}";
            if (!string.IsNullOrWhiteSpace(state))
                url += $"&state={state}";
            return url;
        }

        public async Task<Token> AccessTokenAsync(string verifier)
        {
            var parameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("grant_type", "authorization_code"),
                new KeyValuePair<string, object>("client_id", AppClient.ClientId),
                new KeyValuePair<string, object>("client_secret", AppClient.ClientSecret),
                new KeyValuePair<string, object>("code", verifier)
            };

            var token = await AppClient.PostAsync<Token>("oauth/token", parameters).Stay();
            AppClient.AccessToken = token.AccessToken;
            AppClient.RefreshToken = token.RefreshToken;

            return token;
        }

        public async Task<Token> RefreshTokenAsync()
        {
            var parameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("grant_type", "refresh_token"),
                new KeyValuePair<string, object>("client_id", AppClient.ClientId),
                new KeyValuePair<string, object>("client_secret", AppClient.ClientSecret),
                new KeyValuePair<string, object>("refresh_token", AppClient.RefreshToken)
            };

            var token = await AppClient.PostAsync<Token>("oauth/token", parameters, true).Stay();
            AppClient.AccessToken = token.AccessToken;
            AppClient.RefreshToken = token.RefreshToken;

            return token;
        }
    }
}