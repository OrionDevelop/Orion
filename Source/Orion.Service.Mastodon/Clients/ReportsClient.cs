using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Orion.Service.Mastodon.Helpers;
using Orion.Service.Mastodon.Models;
using Orion.Service.Shared;

// ReSharper disable once CheckNamespace

namespace Orion.Service.Mastodon.Clients
{
    public class ReportsClient : ApiClient<MastodonClient>
    {
        internal ReportsClient(MastodonClient mastodonClent) : base(mastodonClent) { }

        public Task<IEnumerable<Report>> ListAsync(int? maxId = null, int? sinceId = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            PaginateHelper.ApplyParams(parameters, maxId, sinceId);

            return AppClient.GetAsync<IEnumerable<Report>>("api/v1/reports");
        }

        public Task<Report> CreateAsync(int accountId, IEnumerable<int> statusIds, string comment)
        {
            var parameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("account_id", accountId),
                new KeyValuePair<string, object>("comment", comment)
            };
            parameters.AddRange(statusIds.Select(statusId => new KeyValuePair<string, object>("status_ids[]", statusId)));

            return AppClient.PostAsync<Report>("api/v1/reports", parameters);
        }
    }
}