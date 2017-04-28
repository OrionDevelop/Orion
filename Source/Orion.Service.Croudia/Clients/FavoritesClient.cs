using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Orion.Service.Croudia.Models;
using Orion.Service.Shared;

namespace Orion.Service.Croudia.Clients
{
    public class FavoritesClient : ApiClient<CroudiaClient>
    {
        internal FavoritesClient(CroudiaClient client) : base(client) { }

        public Task<IEnumerable<Status>> ListAsync(bool? includeEntities = null, int? sinceId = null, int? maxId = null, int? count = null)
        {
            throw new NotSupportedException();
            /*
            var parameters = new List<KeyValuePair<string, object>>();
            if (includeEntities.HasValue)
                parameters.Add(new KeyValuePair<string, object>("include_entities", includeEntities.Value.ToString().ToLower()));
            if (sinceId.HasValue)
                parameters.Add(new KeyValuePair<string, object>("since_id", sinceId.Value));
            if (maxId.HasValue)
                parameters.Add(new KeyValuePair<string, object>("max_id", maxId.Value));
            if (count.HasValue)
                parameters.Add(new KeyValuePair<string, object>("count", count.Value));

            return AppClient.GetAsync<IEnumerable<Status>>("2/favorites.json", parameters, true);
            */
        }

        public Task<Status> CreateAsync(int id, bool? includeEntities = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            if (includeEntities.HasValue)
                parameters.Add(new KeyValuePair<string, object>("include_entities", includeEntities.Value.ToString().ToLower()));

            return AppClient.PostAsync<Status>($"2/favorites/create/{id}.json", parameters, true);
        }

        public Task<Status> DestroyAsync(int id, bool? includeEntities = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            if (includeEntities.HasValue)
                parameters.Add(new KeyValuePair<string, object>("include_entities", includeEntities.Value.ToString().ToLower()));

            return AppClient.PostAsync<Status>($"2/favorites/destroy/{id}.json", parameters, true);
        }
    }
}