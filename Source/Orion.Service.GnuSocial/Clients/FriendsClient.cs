using System.Collections.Generic;
using System.Threading.Tasks;

using Orion.Service.Shared;

namespace Orion.Service.GnuSocial.Clients
{
    public class FriendsClient : ApiClient<GnuSocialClient>
    {
        internal FriendsClient(GnuSocialClient client) : base(client) { }

        public Task<IEnumerable<int>> IdsAsync(string screenName = null, int? userId = null, int? cursor = null, int? count = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            if (!string.IsNullOrWhiteSpace(screenName))
                parameters.Add(new KeyValuePair<string, object>("screen_name", screenName));
            else if (userId.HasValue)
                parameters.Add(new KeyValuePair<string, object>("user_id", userId.Value));
            if (cursor.HasValue)
                parameters.Add(new KeyValuePair<string, object>("cursor", cursor.Value));
            if (count.HasValue)
                parameters.Add(new KeyValuePair<string, object>("count", count));

            return AppClient.GetAsync<IEnumerable<int>>("friends/ids.json", parameters);
        }
    }
}