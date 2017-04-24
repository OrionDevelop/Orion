using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Orion.Service.GnuSocial.Models;
using Orion.Service.Shared;

namespace Orion.Service.GnuSocial.Clients
{
    public class BlocksClient : ApiClient<GnuSocialClient>
    {
        internal BlocksClient(GnuSocialClient client) : base(client) { }

        public Task<User> CreateAsync(string screenName = null, int? userId = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            if (!string.IsNullOrWhiteSpace(screenName))
                parameters.Add(new KeyValuePair<string, object>("screen_name", screenName));
            else if (userId.HasValue)
                parameters.Add(new KeyValuePair<string, object>("user_id", userId.Value));
            else
                throw new ArgumentNullException();

            return AppClient.PostAsync<User>("blocks/create.json", parameters);
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

            return AppClient.PostAsync<User>("blocks/destroy.json", parameters);
        }

        public Task ExistsAsync()
        {
            throw new NotImplementedException();
        }

        public Task BlockingAsync()
        {
            throw new NotImplementedException();
        }
    }
}