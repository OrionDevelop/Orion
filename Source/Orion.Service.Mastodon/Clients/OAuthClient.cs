using System.Collections.Generic;
using System.Threading.Tasks;

using Orion.Service.Mastodon.Models;
using Orion.Service.Shared;

namespace Orion.Service.Mastodon.Clients
{
    public class OAuthClient : ApiClient<MastodonClient>
    {
        internal OAuthClient(MastodonClient mastodonClent) : base(mastodonClent) { }

        public async Task<Token> TokenAsync(string username, string password, string[] scopes = null)
        {
            var parameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("client_id", AppClient.ClientId),
                new KeyValuePair<string, object>("client_secret", AppClient.ClientSecret),
                new KeyValuePair<string, object>("grant_type", "password"),
                new KeyValuePair<string, object>("username", username),
                new KeyValuePair<string, object>("password", password)
            };
            if (scopes != null)
                parameters.Add(new KeyValuePair<string, object>("scope", string.Join(" ", scopes)));

            var tokens = await AppClient.PostAsync<Token>("oauth/token", parameters).ConfigureAwait(false);
            AppClient.AccessToken = tokens.AccessToken;
            return tokens;
        }
    }
}