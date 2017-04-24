using System.Collections.Generic;
using System.Threading.Tasks;

using Orion.Service.GnuSocial.Models;
using Orion.Service.Shared;

namespace Orion.Service.GnuSocial.Clients
{
    public class FavoritesClient : ApiClient<GnuSocialClient>
    {
        internal FavoritesClient(GnuSocialClient client) : base(client) { }

        public Task<List<Status>> ListAsync(string screenName = null, int? userId = null, int? count = null, int? sinceId = null, int? maxId = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            if (!string.IsNullOrWhiteSpace(screenName))
                parameters.Add(new KeyValuePair<string, object>("screen_name", screenName));
            else if (userId.HasValue)
                parameters.Add(new KeyValuePair<string, object>("user_id", userId.Value));
            if (count.HasValue)
                parameters.Add(new KeyValuePair<string, object>("count", count.Value));
            if (sinceId.HasValue)
                parameters.Add(new KeyValuePair<string, object>("since_id", sinceId.Value));
            if (maxId.HasValue)
                parameters.Add(new KeyValuePair<string, object>("max_id", maxId.Value));
            return AppClient.GetAsync<List<Status>>("favorites.json");
        }

        public Task<Status> CreateAsync(int id)
        {
            return AppClient.PostAsync<Status>($"favorites/create/{id}.json");
        }

        public Task<Status> DestroyAsync(int id)
        {
            return AppClient.PostAsync<Status>($"favorites/destroy/{id}.json");
        }
    }
}