using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Orion.Service.Croudia.Models;
using Orion.Service.Shared;

namespace Orion.Service.Croudia.Clients
{
    public class FriendsClient : ApiClient<CroudiaClient>
    {
        internal FriendsClient(CroudiaClient client) : base(client) { }

        public Task<IdsWithCursor> IdsAsync(string screenName = null, int? userId = null, int? cursor = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            if (!string.IsNullOrWhiteSpace(screenName))
                parameters.Add(new KeyValuePair<string, object>("screen_name", screenName));
            else if (userId.HasValue)
                parameters.Add(new KeyValuePair<string, object>("user_id", userId.Value));
            else
                throw new ArgumentNullException();
            if (cursor.HasValue)
                parameters.Add(new KeyValuePair<string, object>("cursor", cursor.Value));

            return AppClient.GetAsync<IdsWithCursor>("friends/ids.json", parameters, true);
        }

        public Task<UsersWithCursor> ListAsync(string screenName = null, int? userId = null, int? cursor = null, bool? trimUser = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            if (!string.IsNullOrWhiteSpace(screenName))
                parameters.Add(new KeyValuePair<string, object>("screen_name", screenName));
            else if (userId.HasValue)
                parameters.Add(new KeyValuePair<string, object>("user_id", userId.Value));
            else
                throw new ArgumentNullException();
            if (cursor.HasValue)
                parameters.Add(new KeyValuePair<string, object>("cursor", cursor.Value));
            if (trimUser.HasValue)
                parameters.Add(new KeyValuePair<string, object>("trim_user", trimUser.Value.ToString().ToLower()));

            return AppClient.GetAsync<UsersWithCursor>("friends/list.json", parameters, true);
        }
    }
}