using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Orion.Service.Mastodon.Enum;
using Orion.Service.Mastodon.Helpers;
using Orion.Service.Mastodon.Models;

namespace Orion.Service.Mastodon.Clients
{
    public class StatusesClient : ApiClient
    {
        internal StatusesClient(MastodonClient mastodonClent) : base(mastodonClent) { }

        public Task<Status> ShowAsync(int id)
        {
            return MastodonClient.GetAsync<Status>($"api/v1/statuses/{id}");
        }

        public Task<Context> ContextAsync(int id)
        {
            return MastodonClient.GetAsync<Context>($"api/v1/statuses/{id}/context");
        }

        public Task<Card> CardAsync(int id)
        {
            return MastodonClient.GetAsync<Card>($"api/v1/statuses/{id}/card");
        }

        public Task<List<Account>> RebloggedByAsync(int id, int? maxId = null, int? sinceId = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            PaginateHelper.ApplyParams(parameters, maxId, sinceId);

            return MastodonClient.GetAsync<List<Account>>($"api/v1/statuses/{id}/reblogged_by", parameters);
        }

        public Task<List<Account>> FavouritedByAsync(int id, int? maxId = null, int? sinceId = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            PaginateHelper.ApplyParams(parameters, maxId, sinceId);

            return MastodonClient.GetAsync<List<Account>>($"api/v1/statuses/{id}/favourited_by", parameters);
        }

        public Task<Status> CreateAsync(string status, int? inReplyToId = null, List<int> mediaIds = null, bool? sensitive = null, string spoilerText = null,
                                        VisibilityType? visibility = null)
        {
            var parameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("status", status)
            };
            if (inReplyToId.HasValue)
                parameters.Add(new KeyValuePair<string, object>("in_reply_to_id", inReplyToId.Value));
            if (mediaIds != null)
                parameters.AddRange(mediaIds.Select(w => new KeyValuePair<string, object>("media_ids[]", w)));
            if (sensitive.HasValue)
                parameters.Add(new KeyValuePair<string, object>("sensitive", sensitive.Value.ToString().ToLower()));
            if (!string.IsNullOrWhiteSpace(spoilerText))
                parameters.Add(new KeyValuePair<string, object>("spoiler_text", spoilerText));
            if (visibility.HasValue)
                parameters.Add(new KeyValuePair<string, object>("visibility", visibility.Value.ToParameter()));

            return MastodonClient.PostAsync<Status>("api/v1/statuses", parameters);
        }

        public Task DeleteAsync(int id)
        {
            return MastodonClient.DeleteAsync<string>($"api/v1/statuses/{id}");
        }

        public Task<Status> ReblogAsync(int id)
        {
            return MastodonClient.PostAsync<Status>($"api/v1/statuses/{id}/reblog");
        }

        public Task<Status> UnreblogAsync(int id)
        {
            return MastodonClient.PostAsync<Status>($"api/v1/statuses/{id}/unreblog");
        }

        public Task<Status> FavouriteAsync(int id)
        {
            return MastodonClient.PostAsync<Status>($"api/v1/statuses/{id}/favourite");
        }

        public Task<Status> UnfavouriteAsync(int id)
        {
            return MastodonClient.PostAsync<Status>($"api/v1/statuses/{id}/unfavourite");
        }
    }
}