using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Orion.Service.GnuSocial.Models;
using Orion.Service.Shared;

namespace Orion.Service.GnuSocial.Clients
{
    public class StatusesClient : ApiClient<GnuSocialClient>
    {
        internal StatusesClient(GnuSocialClient client) : base(client) { }

        public Task<List<Status>> PublicTimelineAsync(int? count = null, int? sinceId = null, int? maxId = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            ApplyTimelineParams(parameters, count, sinceId, maxId);

            return AppClient.GetAsync<List<Status>>("statuses/public_timeline.json", parameters);
        }

        public Task<List<Status>> HomeTimelineAsync(int? count = null, int? sinceId = null, int? maxId = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            ApplyTimelineParams(parameters, count, sinceId, maxId);

            return AppClient.GetAsync<List<Status>>("statuses/home_timeline.json", parameters, true);
        }

        public Task<List<Status>> PublicNetworkTimelineAsync(int? count = null, int? sinceId = null, int? maxId = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            ApplyTimelineParams(parameters, count, sinceId, maxId);

            return AppClient.GetAsync<List<Status>>("statuses/public_and_external_timeline.json", parameters, true);
        }

        // alias of home timeline.
        public Task<List<Status>> FriendsTimelineAsync(int? count = null, int? sinceId = null, int? maxId = null)
        {
            return HomeTimelineAsync(count, sinceId, maxId);
        }

        public Task<List<Status>> FriendsTimelineAsync(string username, int? count = null, int? sinceId = null, int? maxId = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            ApplyTimelineParams(parameters, count, sinceId, maxId);

            return AppClient.GetAsync<List<Status>>($"statuses/friends_timeline/{username}.json", parameters);
        }

        public Task<List<Status>> MentionsAsync(int? count = null, int? sinceId = null, int? maxId = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            ApplyTimelineParams(parameters, count, sinceId, maxId);

            return AppClient.GetAsync<List<Status>>("statuses/mentions.json", parameters, true);
        }

        // alias of mentions
        public Task<List<Status>> RepliesAsync(int? count = null, int? sinceId = null, int? maxId = null)
        {
            return MentionsAsync(count, sinceId, maxId);
        }

        public Task<List<Status>> RepliesAsync(string username, int? count = null, int? sinceId = null, int? maxId = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            ApplyTimelineParams(parameters, count, sinceId, maxId);

            return AppClient.GetAsync<List<Status>>($"statuses/replies/{username}.json", parameters);
        }

        public Task<List<Status>> UserTimelineAsync(int? count = null, int? sinceId = null, int? maxId = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            ApplyTimelineParams(parameters, count, sinceId, maxId);

            return AppClient.GetAsync<List<Status>>("statuses/user_timeline.json");
        }

        public Task<List<Status>> RetweetedToMeAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Status>> RetweetedByMeAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Status>> RetweetsToMeAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Status> ShowAsync(int id)
        {
            return AppClient.GetAsync<Status>($"statuses/show/{id}.json");
        }

        public Task<Status> UpdateAsync(string status, int? inReplyToStatusId = null)
        {
            var parameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("status", status)
            };
            if (inReplyToStatusId.HasValue)
                parameters.Add(new KeyValuePair<string, object>("in_reply_to_status_id", inReplyToStatusId.Value));

            return AppClient.PostAsync<Status>("statuses/update.json", parameters, true);
        }

        public Task<Status> DestroyAsync(int id)
        {
            return AppClient.PostAsync<Status>($"statuses/destroy/{id}.json", requireAuth: true);
        }

        public Task<Status> RetweetAsync(int id)
        {
            return AppClient.PostAsync<Status>($"statuses/retweet/{id}.json", requireAuth: true);
        }

        public Task<List<User>> FriendsAsync()
        {
            throw new NotSupportedException();
            // return AppClient.GetAsync<List<User>>("statuses/friends.json");
        }

        public Task<List<User>> FollowersAsync()
        {
            throw new NotSupportedException();
            // return AppClient.GetAsync<List<User>>("statuses/followers.json");
        }

        private void ApplyTimelineParams(List<KeyValuePair<string, object>> parameters, int? count, int? sinceId, int? maxId)
        {
            if (count.HasValue)
                parameters.Add(new KeyValuePair<string, object>("count", count.Value));
            if (sinceId.HasValue)
                parameters.Add(new KeyValuePair<string, object>("since_id", sinceId.Value));
            if (maxId.HasValue)
                parameters.Add(new KeyValuePair<string, object>("max_id", maxId.Value));
        }
    }
}