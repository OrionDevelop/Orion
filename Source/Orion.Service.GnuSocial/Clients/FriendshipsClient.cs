using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Orion.Service.GnuSocial.Models;
using Orion.Service.Shared;

namespace Orion.Service.GnuSocial.Clients
{
    public class FriendshipsClient : ApiClient<GnuSocialClient>
    {
        internal FriendshipsClient(GnuSocialClient client) : base(client) { }

        public Task<User> CreateAsync(string screenName = null, int? userId = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            if (!string.IsNullOrWhiteSpace(screenName))
                parameters.Add(new KeyValuePair<string, object>("screen_name", screenName));
            else if (userId.HasValue)
                parameters.Add(new KeyValuePair<string, object>("user_id", userId.Value));
            else
                throw new ArgumentNullException();

            return AppClient.PostAsync<User>("friendships/create.json", parameters);
        }

        public Task<User> DestroyAsync(string screenName = null, int? userId = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            if (!string.IsNullOrWhiteSpace(screenName))
                parameters.Add(new KeyValuePair<string, object>("screen_name", screenName));
            else if (userId.HasValue)
                parameters.Add(new KeyValuePair<string, object>("user_id", userId.Value));
            else
                throw new ArgumentNullException();

            return AppClient.PostAsync<User>("friendships/destroy.json", parameters);
        }

        public Task<User> ExistsAsync(string screenName = null, int? userId = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            if (!string.IsNullOrWhiteSpace(screenName))
                parameters.Add(new KeyValuePair<string, object>("screen_name", screenName));
            else if (userId.HasValue)
                parameters.Add(new KeyValuePair<string, object>("user_id", userId.Value));
            else
                throw new ArgumentNullException();

            return AppClient.GetAsync<User>("friendships/exists.json", parameters);
        }

        public Task<User> ShowAsync(string sourceScreenName = null, int? sourceUserId = null, string targetScreenName = null, int? targetUserId = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            if (!string.IsNullOrWhiteSpace(sourceScreenName))
                parameters.Add(new KeyValuePair<string, object>("source_screen_name", sourceScreenName));
            else if (sourceUserId.HasValue)
                parameters.Add(new KeyValuePair<string, object>("source_user_id", sourceUserId.Value));
            else
                throw new ArgumentNullException();

            if (!string.IsNullOrWhiteSpace(targetScreenName))
                parameters.Add(new KeyValuePair<string, object>("target_screen_name", targetScreenName));
            else if (targetUserId.HasValue)
                parameters.Add(new KeyValuePair<string, object>("target_user_id", targetUserId.Value));
            else
                throw new ArgumentNullException();

            return AppClient.GetAsync<User>("friendships/show.json", parameters);
        }
    }
}