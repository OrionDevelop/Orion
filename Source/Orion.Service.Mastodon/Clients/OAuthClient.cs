using System.Collections.Generic;
using System.Threading.Tasks;

using Orion.Service.Mastodon.Models;

namespace Orion.Service.Mastodon.Clients
{
    public class OAuthClient : ApiClient
    {
        internal OAuthClient(MastodonClient mastodonClent) : base(mastodonClent) { }

        public async Task<Token> TokenAsync(string username, string password, string[] scopes = null)
        {
            var parameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("client_id", MastodonClient.ClientId),
                new KeyValuePair<string, object>("client_secret", MastodonClient.ClientSecret),
                new KeyValuePair<string, object>("grant_type", "password"),
                new KeyValuePair<string, object>("username", username),
                new KeyValuePair<string, object>("password", password)
            };
            if (scopes != null)
                parameters.Add(new KeyValuePair<string, object>("scope", string.Join(" ", scopes)));

            var tokens = await MastodonClient.PostAsync<Token>("oauth/token", parameters).ConfigureAwait(false);
            MastodonClient.AccessTokn = tokens.AccessToken;
            return tokens;
        }
    }
}