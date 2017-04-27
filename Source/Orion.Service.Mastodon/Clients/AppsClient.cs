using System.Collections.Generic;
using System.Threading.Tasks;

using Orion.Service.Mastodon.Enum;
using Orion.Service.Mastodon.Models;
using Orion.Service.Shared;

// ReSharper disable once CheckNamespace

namespace Orion.Service.Mastodon.Clients
{
    public class AppsClient : ApiClient<MastodonClient>
    {
        internal AppsClient(MastodonClient mastodonClent) : base(mastodonClent) { }

        public async Task<RegistApp> RegisterAsync(string clientName, string redirectUris, Scope scopes, string website = null)
        {
            var parameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("client_name", clientName),
                new KeyValuePair<string, object>("redirect_uris", redirectUris),
                new KeyValuePair<string, object>("scopes", string.Join(" ", scopes.ToStrings()))
            };
            if (!string.IsNullOrWhiteSpace(website))
                parameters.Add(new KeyValuePair<string, object>("website", website));

            var apps = await AppClient.PostAsync<RegistApp>("api/v1/apps", parameters).ConfigureAwait(false);
            AppClient.ClientId = apps.ClientId;
            AppClient.ClientSecret = apps.ClientSecret;
            return apps;
        }
    }
}