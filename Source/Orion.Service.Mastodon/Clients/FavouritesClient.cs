using System.Collections.Generic;
using System.Threading.Tasks;

using Orion.Service.Mastodon.Helpers;
using Orion.Service.Mastodon.Models;

// ReSharper disable once CheckNamespace

namespace Orion.Service.Mastodon.Clients
{
    public class FavouritesClient : ApiClient
    {
        internal FavouritesClient(MastodonClient mastodonClent) : base(mastodonClent) { }

        public Task<List<Status>> ListAsync(int? maxId = null, int? sinceId = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            PaginateHelper.ApplyParams(parameters, maxId, sinceId);

            return MastodonClient.GetAsync<List<Status>>("api/v1/favourites", parameters);
        }
    }
}