using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

using Orion.Service.Croudia.Models;
using Orion.Service.Shared;
using Orion.Service.Shared.Extensions;

namespace Orion.Service.Croudia.Clients
{
    public class FriendshipsClient : ApiClient<CroudiaClient>
    {
        internal FriendshipsClient(CroudiaClient client) : base(client) { }

        public Task<User> CreateAsync(string screenName = null, int? userId = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            if (!string.IsNullOrWhiteSpace(screenName))
                parameters.Add(new KeyValuePair<string, object>("screen_name", screenName));
            else if (userId.HasValue)
                parameters.Add(new KeyValuePair<string, object>("user_id", userId.Value));
            else
                throw new ArgumentNullException();

            return AppClient.PostAsync<User>("friendships/create.json", parameters, true);
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

            return AppClient.PostAsync<User>("friendships/destroy.json", parameters, true);
        }

        public async Task<RelationShip> ShowAsync(string sourceScreenName = null, int? sourceUserId = null, string targetScreenName = null,
                                                  int? targetUserId = null)
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

            var response = await AppClient.GetRawAsync("friendships/show.json", parameters, true).Stay();
            return JObject.Parse(response)["relationship"].ToObject<RelationShip>();
        }

        public Task<IEnumerable<RelationShip>> LookupAsync(List<string> screenNames = null, List<int> userIds = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            if (screenNames != null && screenNames.Any())
                parameters.Add(new KeyValuePair<string, object>("screen_name", string.Join(",", screenNames)));
            if (userIds != null && userIds.Any())
                parameters.Add(new KeyValuePair<string, object>("user_id", string.Join(",", userIds)));

            return AppClient.GetAsync<IEnumerable<RelationShip>>("friendships/lookup.json", parameters, true);
        }
    }
}