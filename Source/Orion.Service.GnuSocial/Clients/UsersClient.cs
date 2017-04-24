using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Orion.Service.GnuSocial.Models;
using Orion.Service.Shared;

namespace Orion.Service.GnuSocial.Clients
{
    public class UsersClient : ApiClient<GnuSocialClient>
    {
        internal UsersClient(GnuSocialClient client) : base(client) { }

        public Task<User> ShowAsync(string screenName = null, int? userId = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            if (!string.IsNullOrWhiteSpace(screenName))
                parameters.Add(new KeyValuePair<string, object>("screen_name", screenName));
            else if (userId.HasValue)
                parameters.Add(new KeyValuePair<string, object>("user_id", userId.Value));
            else
                throw new NotImplementedException();

            return AppClient.GetAsync<User>("users/show.json");
        }
    }
}