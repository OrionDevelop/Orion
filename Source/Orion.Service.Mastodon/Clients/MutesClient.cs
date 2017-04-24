using System.Collections.Generic;
using System.Threading.Tasks;

using Orion.Service.Mastodon.Helpers;
using Orion.Service.Mastodon.Models;
using Orion.Service.Shared;

// ReSharper disable once CheckNamespace

namespace Orion.Service.Mastodon.Clients
{
    public class MutesClient : ApiClient<MastodonClient>
    {
        internal MutesClient(MastodonClient mastodonClent) : base(mastodonClent) { }

        public Task<List<Account>> ShowAsync(int? maxId = null, int? sinceId = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            PaginateHelper.ApplyParams(parameters, maxId, sinceId);

            return AppClient.GetAsync<List<Account>>("api/v1/mutes", parameters);
        }
    }
}