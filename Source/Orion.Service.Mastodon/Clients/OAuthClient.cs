using System.Collections.Generic;
using System.Threading.Tasks;

using Orion.Service.Mastodon.Enum;
using Orion.Service.Mastodon.Models;
using Orion.Service.Shared;

namespace Orion.Service.Mastodon.Clients
{
    public class OAuthClient : ApiClient<MastodonClient>
    {
        internal OAuthClient(MastodonClient mastodonClent) : base(mastodonClent) { }

        /// <summary>
        ///     Login with email and password.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="scopes"></param>
        /// <returns></returns>
        // ReSharper disable once MethodOverloadWithOptionalParameter
        public async Task<Token> TokenAsync(string username, string password, Scope scopes = Scope.Read)
        {
            var parameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("client_id", AppClient.ClientId),
                new KeyValuePair<string, object>("client_secret", AppClient.ClientSecret),
                new KeyValuePair<string, object>("grant_type", "password"),
                new KeyValuePair<string, object>("username", username),
                new KeyValuePair<string, object>("password", password),
                new KeyValuePair<string, object>("scope", string.Join(" ", scopes.ToStrings()))
            };

            var tokens = await AppClient.PostAsync<Token>("oauth/token", parameters).ConfigureAwait(false);
            AppClient.AccessToken = tokens.AccessToken;
            return tokens;
        }

        public async Task<Token> TokenAsync(string verify, string redirectUri = "urn:ietf:wg:oauth:2.0:oob")
        {
            var parameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("client_id", AppClient.ClientId),
                new KeyValuePair<string, object>("client_secret", AppClient.ClientSecret),
                new KeyValuePair<string, object>("grant_type", "authorization_code"),
                new KeyValuePair<string, object>("code", verify),
                new KeyValuePair<string, object>("redirect_uri", redirectUri)
            };

            var tokens = await AppClient.PostAsync<Token>("oauth/token", parameters).ConfigureAwait(false);
            AppClient.AccessToken = tokens.AccessToken;
            return tokens;
        }

        public string GetAuthorizeUrl(Scope scopes = Scope.Read, string redirectUri = "urn:ietf:wg:oauth:2.0:oob")
        {
            return
                $"{AppClient.BaseUrl}/oauth/authorize?response_type=code&client_id={AppClient.ClientId}&scope={string.Join(" ", scopes.ToStrings())}&redirect_uri={redirectUri}";
        }
    }
}