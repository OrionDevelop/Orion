using System.Collections.Generic;
using System.Threading.Tasks;

using Orion.Service.Mastodon.Helpers;
using Orion.Service.Mastodon.Models;
using Orion.Service.Shared;
using Orion.Service.Shared.Extensions;

namespace Orion.Service.Mastodon.Clients
{
    public class NotificationsClient : ApiClient<MastodonClient>
    {
        internal NotificationsClient(MastodonClient mastodonClent) : base(mastodonClent) { }

        public Task<IEnumerable<Notification>> ShowAsync(int? maxId = null, int? sinceId = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            PaginateHelper.ApplyParams(parameters, maxId, sinceId);

            return AppClient.GetAsync<IEnumerable<Notification>>("api/v1/notifications", parameters);
        }

        public Task<Notification> SingleAsync(int id)
        {
            return AppClient.GetAsync<Notification>($"api/v1/notifications/{id}");
        }

        public async Task ClearAsync()
        {
            await AppClient.PostRawAsync("api/v1/notifications/clear").Stay();
        }

        public async Task DismissAsync(int id)
        {
            await AppClient.PostRawAsync($"api/v1/notifications/dismiss/{id}").Stay();
        }
    }
}