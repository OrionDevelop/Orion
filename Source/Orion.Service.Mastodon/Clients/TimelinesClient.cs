using System.Collections.Generic;
using System.Threading.Tasks;

using Orion.Service.Mastodon.Helpers;
using Orion.Service.Mastodon.Models;
using Orion.Service.Shared;

namespace Orion.Service.Mastodon.Clients
{
    public class TimelinesClient : ApiClient<MastodonClient>
    {
        internal TimelinesClient(MastodonClient mastodonClent) : base(mastodonClent) { }

        public Task<IEnumerable<Status>> HomeAsync(int? maxId = null, int? sinceId = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            PaginateHelper.ApplyParams(parameters, maxId, sinceId);

            return AppClient.GetAsync<IEnumerable<Status>>("api/v1/timelines/home", parameters);
        }

        public Task<IEnumerable<Status>> PublicAsync(bool? local = null, int? maxId = null, int? sinceId = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            if (local.HasValue)
                parameters.Add(new KeyValuePair<string, object>("local", local.Value.ToString().ToLower()));
            PaginateHelper.ApplyParams(parameters, maxId, sinceId);

            return AppClient.GetAsync<IEnumerable<Status>>("api/v1/timelines/public", parameters);
        }

        public Task<IEnumerable<Status>> TagAsync(string hashtag, bool? local = null, int? maxId = null, int? sinceId = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            if (local.HasValue)
                parameters.Add(new KeyValuePair<string, object>("local", local.Value.ToString().ToLower()));
            PaginateHelper.ApplyParams(parameters, maxId, sinceId);

            return AppClient.GetAsync<IEnumerable<Status>>($"api/v1/timelines/tag/{hashtag}", parameters);
        }
    }
}