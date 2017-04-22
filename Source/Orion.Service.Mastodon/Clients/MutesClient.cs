using System.Collections.Generic;
using System.Threading.Tasks;

using Orion.Service.Mastodon.Helpers;
using Orion.Service.Mastodon.Models;

// ReSharper disable once CheckNamespace

namespace Orion.Service.Mastodon.Clients
{
    public class MutesClient : ApiClient
    {
        internal MutesClient(MastodonClient mastodonClent) : base(mastodonClent) { }

        public Task<List<Account>> ShowAsync(int? maxId = null, int? sinceId = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            PaginateHelper.ApplyParams(parameters, maxId, sinceId);

            return MastodonClient.GetAsync<List<Account>>("api/v1/mutes", parameters);
        }
    }
}