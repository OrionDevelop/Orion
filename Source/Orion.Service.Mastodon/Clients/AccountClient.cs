using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Orion.Service.Mastodon.Helpers;
using Orion.Service.Mastodon.Models;
using Orion.Service.Shared;
using Orion.Service.Shared.Helpers;

namespace Orion.Service.Mastodon.Clients
{
    public class AccountClient : ApiClient<MastodonClient>
    {
        internal AccountClient(MastodonClient mastodonClent) : base(mastodonClent) { }

        public Task<Account> ShowAsync(int id)
        {
            return AppClient.GetAsync<Account>($"api/v1/accounts/{id}");
        }

        public Task<Account> VerifyCredentialsAsync()
        {
            return AppClient.GetAsync<Account>("api/v1/accounts/verify_credentials");
        }

        public Task<Account> UpdateCredentialsAsync(string displayName = null, string note = null, string avatarPath = null, string headerPath = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            if (!string.IsNullOrWhiteSpace(displayName))
                parameters.Add(new KeyValuePair<string, object>("display_name", displayName));
            if (!string.IsNullOrWhiteSpace(note))
                parameters.Add(new KeyValuePair<string, object>("note", note));
            if (!string.IsNullOrWhiteSpace(avatarPath))
                parameters.Add(new KeyValuePair<string, object>("avatar", FileHelper.FileToBase64Strings(avatarPath)));
            if (!string.IsNullOrWhiteSpace(headerPath))
                parameters.Add(new KeyValuePair<string, object>("header", FileHelper.FileToBase64Strings(headerPath)));

            return AppClient.PatchAsync<Account>("api/v1/accounts/update_credentials", parameters);
        }

        public Task<IEnumerable<Account>> FollowersAsync(int id, int? maxId = null, int? sinceId = null, int? limit = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            PaginateHelper.ApplyParams(parameters, maxId, sinceId);
            if (limit.HasValue)
                parameters.Add(new KeyValuePair<string, object>("limit", limit.Value));

            return AppClient.GetAsync<IEnumerable<Account>>($"api/v1/accounts/{id}/followers", parameters);
        }

        public Task<IEnumerable<Account>> FollowingAsync(int id, int? maxId = null, int? sinceId = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            PaginateHelper.ApplyParams(parameters, maxId, sinceId);

            return AppClient.GetAsync<IEnumerable<Account>>($"api/v1/accounts/{id}/following", parameters);
        }

        public Task<IEnumerable<Status>> StatusesAsync(int id, bool? onlyMedia = null, bool? excludeReplies = null, int? maxId = null, int? sinceId = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            if (onlyMedia.HasValue)
                parameters.Add(new KeyValuePair<string, object>("only_media", onlyMedia));
            if (excludeReplies.HasValue)
                parameters.Add(new KeyValuePair<string, object>("exclude_replies", excludeReplies));
            PaginateHelper.ApplyParams(parameters, maxId, sinceId);

            return AppClient.GetAsync<IEnumerable<Status>>($"api/v1/accounts/{id}/statuses", parameters);
        }

        public Task<Relationship> FollowAsync(int id)
        {
            return AppClient.PostAsync<Relationship>($"api/v1/accounts/{id}/follow");
        }

        public Task<Relationship> UnfollowAsync(int id)
        {
            return AppClient.PostAsync<Relationship>($"api/v1/accounts/{id}/unfollow");
        }

        public Task<Account> BlockAsync(int id)
        {
            return AppClient.PostAsync<Account>($"api/v1/accounts/{id}/block");
        }

        public Task<Account> UnblockAsync(int id)
        {
            return AppClient.PostAsync<Account>($"api/v1/accounts/{id}/unblock");
        }

        public Task<Account> MuteAsync(int id)
        {
            return AppClient.PostAsync<Account>($"api/v1/accounts/{id}/mute");
        }

        public Task<Account> UnmuteAsync(int id)
        {
            return AppClient.PostAsync<Account>($"api/v1/accounts/{id}/unmute");
        }

        public Task<IEnumerable<Relationship>> RelationShipsAsync(IEnumerable<int> ids, int? maxId = null, int? sinceId = null)
        {
            var parameters = ids.Select(id => new KeyValuePair<string, object>("id[]", id)).ToList();
            PaginateHelper.ApplyParams(parameters, maxId, sinceId);

            return AppClient.GetAsync<IEnumerable<Relationship>>("api/v1/accounts/relationships", parameters);
        }

        public Task<IEnumerable<Account>> SearchAsync(string q, int? limit = null)
        {
            var parameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("q", q)
            };
            if (limit.HasValue)
                parameters.Add(new KeyValuePair<string, object>("limit", limit));

            return AppClient.GetAsync<IEnumerable<Account>>("api/v1/accounts/search", parameters);
        }
    }
}