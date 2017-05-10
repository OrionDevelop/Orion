using System.Collections.Generic;
using System.Threading.Tasks;

using Orion.Service.Croudia.Models;
using Orion.Service.Shared;

namespace Orion.Service.Croudia.Clients
{
    public class SearchClient : ApiClient<CroudiaClient>
    {
        internal SearchClient(CroudiaClient client) : base(client) { }

        public Task<StatusResults> VoicesAsync(string q, bool? trimUser = null, bool? includeEntities = null, int? sinceId = null, int? maxId = null,
                                               int? count = null)
        {
            var parameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("q", q)
            };
            if (trimUser.HasValue)
                parameters.Add(new KeyValuePair<string, object>("trim_user", trimUser.Value.ToString().ToLower()));
            if (includeEntities.HasValue)
                parameters.Add(new KeyValuePair<string, object>("include_entities", includeEntities.Value.ToString().ToLower()));
            if (sinceId.HasValue)
                parameters.Add(new KeyValuePair<string, object>("since_id", sinceId.Value));
            if (maxId.HasValue)
                parameters.Add(new KeyValuePair<string, object>("max_id", maxId.Value));
            if (count.HasValue)
                parameters.Add(new KeyValuePair<string, object>("count", count.Value));

            return AppClient.GetAsync<StatusResults>("2/search/voices.json", parameters, true);
        }

        public Task<IEnumerable<User>> UsersAsync(string q, int? count = null, int? page = null, bool? trimUser = null)
        {
            var parameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("q", q)
            };
            if (count.HasValue)
                parameters.Add(new KeyValuePair<string, object>("count", count.Value));
            if (page.HasValue)
                parameters.Add(new KeyValuePair<string, object>("page", page.Value));
            if (trimUser.HasValue)
                parameters.Add(new KeyValuePair<string, object>("trim_user", trimUser.Value.ToString().ToLower()));

            return AppClient.GetAsync<IEnumerable<User>>("users/search.json", parameters, true);
        }

        public Task<IEnumerable<User>> ProfileAsync(string q, int? count = null, int? page = null, bool? trimUser = null)
        {
            var parameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("q", q)
            };
            if (count.HasValue)
                parameters.Add(new KeyValuePair<string, object>("count", count.Value));
            if (page.HasValue)
                parameters.Add(new KeyValuePair<string, object>("page", page.Value));
            if (trimUser.HasValue)
                parameters.Add(new KeyValuePair<string, object>("trim_user", trimUser.Value.ToString().ToLower()));

            return AppClient.GetAsync<IEnumerable<User>>("profile/search.json", parameters, true);
        }

        public Task<StatusResults> FavoritessAsync(string q, bool? trimUser = null, bool? includeEntities = null, int? sinceId = null, int? maxId = null,
                                                   int? count = null)
        {
            var parameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("q", q)
            };
            if (trimUser.HasValue)
                parameters.Add(new KeyValuePair<string, object>("trim_user", trimUser.Value.ToString().ToLower()));
            if (includeEntities.HasValue)
                parameters.Add(new KeyValuePair<string, object>("include_entities", includeEntities.Value.ToString().ToLower()));
            if (sinceId.HasValue)
                parameters.Add(new KeyValuePair<string, object>("since_id", sinceId.Value));
            if (maxId.HasValue)
                parameters.Add(new KeyValuePair<string, object>("max_id", maxId.Value));
            if (count.HasValue)
                parameters.Add(new KeyValuePair<string, object>("count", count.Value));

            return AppClient.GetAsync<StatusResults>("2/search/favorites.json", parameters, true);
        }
    }
}