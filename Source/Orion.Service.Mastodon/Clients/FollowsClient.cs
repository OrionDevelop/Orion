using System.Collections.Generic;
using System.Threading.Tasks;

using Orion.Service.Mastodon.Models;
using Orion.Service.Shared;

// ReSharper disable once CheckNamespace

namespace Orion.Service.Mastodon.Clients
{
    public class FollowsClient : ApiClient<MastodonClient>
    {
        internal FollowsClient(MastodonClient mastodonClent) : base(mastodonClent) { }

        public Task<Account> ListAsync(string url)
        {
            var parameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("uri", url)
            };
            return AppClient.PostAsync<Account>("api/v1/follows", parameters);
        }
    }
}