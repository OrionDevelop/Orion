using System.Collections.Generic;
using System.Threading.Tasks;

using Orion.Service.Mastodon.Helpers;
using Orion.Service.Mastodon.Models;
using Orion.Service.Shared;

// ReSharper disable once CheckNamespace

namespace Orion.Service.Mastodon.Clients
{
    public class BlocksClient : ApiClient<MastodonClient>
    {
        internal BlocksClient(MastodonClient mastodonClent) : base(mastodonClent) { }

        public Task<IEnumerable<Account>> ListAsync(int? maxId = null, int? sinceId = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            PaginateHelper.ApplyParams(parameters, maxId, sinceId);

            return AppClient.GetAsync<IEnumerable<Account>>("api/v1/blocks", parameters);
        }
    }
}