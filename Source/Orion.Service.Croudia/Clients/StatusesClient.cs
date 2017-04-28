using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Orion.Service.Croudia.Models;
using Orion.Service.Shared;

namespace Orion.Service.Croudia.Clients
{
    public class StatusesClient : ApiClient<CroudiaClient>
    {
        internal StatusesClient(CroudiaClient client) : base(client) { }

        public Task<IEnumerable<Status>> PublicTimelineAsync(bool? trimUser = null, bool? includeEntities = null, int? sinceId = null, int? maxId = null,
                                                             int? count = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            ApplyParams(parameters, trimUser, includeEntities, sinceId, maxId, count);

            return AppClient.GetAsync<IEnumerable<Status>>("2/statuses/public_timeline.json", parameters, true);
        }

        public Task<IEnumerable<Status>> HomeTimelineAsync(bool? trimUser = null, bool? includeEntities = null, int? sinceId = null, int? maxId = null,
                                                           int? count = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            ApplyParams(parameters, trimUser, includeEntities, sinceId, maxId, count);

            return AppClient.GetAsync<IEnumerable<Status>>("2/statuses/home_timeline.json", parameters, true);
        }

        public Task<IEnumerable<Status>> UserTimelineAsync(string screenName = null, int? userId = null, bool? trimUser = null, bool? includeEntities = null,
                                                           int? sinceId = null, int? maxId = null, int? count = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            if (!string.IsNullOrWhiteSpace(screenName))
                parameters.Add(new KeyValuePair<string, object>("screen_name", screenName));
            else if (userId.HasValue)
                parameters.Add(new KeyValuePair<string, object>("user_id", userId.Value));
            else
                throw new ArgumentNullException();

            ApplyParams(parameters, trimUser, includeEntities, sinceId, maxId, count);

            return AppClient.GetAsync<IEnumerable<Status>>("2/statuses/user_timeline.json", parameters, true);
        }

        public Task<IEnumerable<Status>> MentionsAsync(bool? trimUser = null, bool? includeEntities = null, int? sinceId = null, int? maxId = null,
                                                       int? count = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            ApplyParams(parameters, trimUser, includeEntities, sinceId, maxId, count);

            return AppClient.GetAsync<IEnumerable<Status>>("2/statuses/mentions.json", parameters, true);
        }

        public Task<Status> UpdateAsync(string status, int? inReplyToStatus = null, bool? timer = null, bool? trimUser = null, bool? includeEntities = null)
        {
            var parameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("status", status)
            };
            ApplyParams(parameters, trimUser, includeEntities);
            if (inReplyToStatus.HasValue)
                parameters.Add(new KeyValuePair<string, object>("in_reply_to_status_id", inReplyToStatus.Value));
            if (timer.HasValue)
                parameters.Add(new KeyValuePair<string, object>("timer", timer.Value.ToString().ToLower()));

            return AppClient.PostAsync<Status>("2/statuses/update.json", parameters, true);
        }

        public Task<Status> UpdateWithMediaAsync(string status, string media, int? inReplyToStatus = null, bool? timer = null, bool? trimUser = null,
                                                 bool? includeEntities = null)
        {
            var parameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("status", status),
                new KeyValuePair<string, object>("media", media)
            };
            ApplyParams(parameters, trimUser, includeEntities);
            if (inReplyToStatus.HasValue)
                parameters.Add(new KeyValuePair<string, object>("in_reply_to_status_id", inReplyToStatus.Value));
            if (timer.HasValue)
                parameters.Add(new KeyValuePair<string, object>("timer", timer.Value.ToString().ToLower()));

            return AppClient.PostAsync<Status>("2/statuses/update_with_media.json", parameters, true);
        }

        public Task<Status> DestroyAsync(int id, bool? trimUser = null, bool? includeEntities = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            ApplyParams(parameters, trimUser, includeEntities);

            return AppClient.PostAsync<Status>($"2/statuses/destroy/{id}.json", parameters, true);
        }

        public Task<Status> ShowAsync(int id, bool? trimUser = null, bool? includeEntities = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            ApplyParams(parameters, trimUser, includeEntities);

            return AppClient.GetAsync<Status>($"2/statuses/show/{id}.json", parameters, true);
        }

        public Task<Status> SpreadAsync(int id, bool? trimUser = null, bool? includeEntities = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            ApplyParams(parameters, trimUser, includeEntities);

            return AppClient.GetAsync<Status>($"2/statuses/spread/{id}.json", parameters, true);
        }

        public Task<Status> CommentAsync(int id, string status, bool? trimUser = null, bool? includeEntities = null)
        {
            var parameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("id", id),
                new KeyValuePair<string, object>("status", status)
            };
            ApplyParams(parameters, trimUser, includeEntities);

            return AppClient.GetAsync<Status>($"2/statuses/comment.json", parameters, true);
        }

        public Task<Status> CommentWithMediaAsync(int id, string status, string media, bool? trimUser = null, bool? includeEntities = null)
        {
            var parameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("id", id),
                new KeyValuePair<string, object>("status", status),
                new KeyValuePair<string, object>("media", media)
            };
            ApplyParams(parameters, trimUser, includeEntities);

            return AppClient.GetAsync<Status>($"2/statuses/comment_with_media.json", parameters, true);
        }

        private void ApplyParams(List<KeyValuePair<string, object>> parameters, bool? trimUser, bool? includeEntities)
        {
            if (trimUser.HasValue)
                parameters.Add(new KeyValuePair<string, object>("trim_user", trimUser.Value.ToString().ToLower()));
            if (includeEntities.HasValue)
                parameters.Add(new KeyValuePair<string, object>("include_entities", includeEntities.Value.ToString().ToLower()));
        }

        private void ApplyParams(List<KeyValuePair<string, object>> parameters, bool? trimUser, bool? includeEntities, int? sinceId, int? maxId, int? count)
        {
            ApplyParams(parameters, trimUser, includeEntities);
            if (sinceId.HasValue)
                parameters.Add(new KeyValuePair<string, object>("since_id", sinceId.Value));
            if (maxId.HasValue)
                parameters.Add(new KeyValuePair<string, object>("max_id", maxId.Value));
            if (count.HasValue)
                parameters.Add(new KeyValuePair<string, object>("count", count.Value));
        }
    }
}