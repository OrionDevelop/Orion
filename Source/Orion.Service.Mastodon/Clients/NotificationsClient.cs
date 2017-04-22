using System.Collections.Generic;
using System.Threading.Tasks;

using Orion.Service.Mastodon.Helpers;
using Orion.Service.Mastodon.Models;

namespace Orion.Service.Mastodon.Clients
{
    public class NotificationsClient : ApiClient
    {
        internal NotificationsClient(MastodonClient mastodonClent) : base(mastodonClent) { }

        public Task<List<Notification>> ShowAsync(int? maxId = null, int? sinceId = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            PaginateHelper.ApplyParams(parameters, maxId, sinceId);

            return MastodonClient.GetAsync<List<Notification>>("api/v1/notifications", parameters);
        }

        public Task<Notification> SingleAsync(int id)
        {
            return MastodonClient.GetAsync<Notification>($"api/v1/notifications/{id}");
        }

        public async Task ClearAsync()
        {
            await MastodonClient.PostAsync<string>("api/v1/notifications/clear").ConfigureAwait(false);
        }
    }
}