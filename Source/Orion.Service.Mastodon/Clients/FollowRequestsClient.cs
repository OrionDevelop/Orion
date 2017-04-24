using System.Collections.Generic;
using System.Threading.Tasks;

using Orion.Service.Mastodon.Helpers;
using Orion.Service.Mastodon.Models;
using Orion.Service.Shared;

namespace Orion.Service.Mastodon.Clients
{
    public class FollowRequestsClient : ApiClient<MastodonClient>
    {
        public FollowRequestsClient(MastodonClient mastodonClent) : base(mastodonClent) { }

        public Task<List<Account>> ListAsync(int? maxId = null, int? sinceId = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            PaginateHelper.ApplyParams(parameters, maxId, sinceId);

            return AppClient.GetAsync<List<Account>>("api/v1/follow_requests", parameters);
        }

        public async Task AuthorizeAsync(int id)
        {
            await AppClient.PostAsync<string>($"api/v1/follow_requests/{id}/authorize").ConfigureAwait(false);
        }

        public async Task RejectAsync(int id)
        {
            await AppClient.PostAsync<string>($"api/v1/follow_requests/{id}/reject").ConfigureAwait(false);
        }
    }
}