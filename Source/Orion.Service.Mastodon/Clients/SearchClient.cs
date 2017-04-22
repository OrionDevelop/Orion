using System.Collections.Generic;
using System.Threading.Tasks;

using Orion.Service.Mastodon.Models;

// ReSharper disable once CheckNamespace

namespace Orion.Service.Mastodon.Clients
{
    public class SearchClient : ApiClient
    {
        internal SearchClient(MastodonClient mastodonClent) : base(mastodonClent) { }

        public Task<Results> QueryAsync(string q, bool resolve)
        {
            var parameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("q", q),
                new KeyValuePair<string, object>("resolve", resolve.ToString().ToLower())
            };
            return MastodonClient.GetAsync<Results>("api/v1/search", parameters);
        }
    }
}