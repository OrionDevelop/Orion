using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Orion.Service.Croudia.Models;
using Orion.Service.Shared;

namespace Orion.Service.Croudia.Clients
{
    public class UsersClient : ApiClient<CroudiaClient>
    {
        internal UsersClient(CroudiaClient client) : base(client) { }

        public Task<User> ShowAsync(string screenName = null, int? userId = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            if (!string.IsNullOrWhiteSpace(screenName))
                parameters.Add(new KeyValuePair<string, object>("screen_name", screenName));
            else if (userId.HasValue)
                parameters.Add(new KeyValuePair<string, object>("user_id", userId.Value));
            else
                throw new ArgumentNullException();

            return AppClient.GetAsync<User>("users/show.json", parameters, true);
        }

        public Task<IEnumerable<User>> LookupAsync(List<string> screenNames = null, List<int> userIds = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            if (screenNames != null && screenNames.Any())
                parameters.Add(new KeyValuePair<string, object>("screen_name", string.Join(",", screenNames)));
            if (userIds != null && userIds.Any())
                parameters.Add(new KeyValuePair<string, object>("user_id", string.Join(",", userIds)));

            return AppClient.GetAsync<IEnumerable<User>>("users/lookup.json", parameters, true);
        }
    }
}