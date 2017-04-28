using System.Collections.Generic;
using System.Threading.Tasks;

using Orion.Service.Croudia.Models;
using Orion.Service.Shared;

namespace Orion.Service.Croudia.Clients
{
    public class AccountClient : ApiClient<CroudiaClient>
    {
        internal AccountClient(CroudiaClient client) : base(client) { }

        public Task<User> VerifyCredentialsAsync()
        {
            return AppClient.GetAsync<User>("account/verify_credentials.json");
        }

        public Task<User> UpdateProfleImageAsync(string image)
        {
            var parameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("image", image)
            };
            return AppClient.PostAsync<User>("account/update_profile_image.json", parameters, true);
        }

        public Task<User> UpdateCoverImageAsync(string image)
        {
            var parameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("image", image)
            };
            return AppClient.PostAsync<User>("account/update_cover_image.json", parameters, true);
        }

        public Task<User> UpdateProfileAsync(string name = null, string url = null, string location = null, string description = null, int? timersec = null,
                                             bool? @protected = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            if (!string.IsNullOrWhiteSpace(name))
                parameters.Add(new KeyValuePair<string, object>("name", name));
            if (!string.IsNullOrWhiteSpace(url))
                parameters.Add(new KeyValuePair<string, object>("url", url));
            if (!string.IsNullOrWhiteSpace(location))
                parameters.Add(new KeyValuePair<string, object>("location", location));
            if (!string.IsNullOrWhiteSpace(description))
                parameters.Add(new KeyValuePair<string, object>("description", description));
            if (timersec.HasValue)
                parameters.Add(new KeyValuePair<string, object>("timersec", timersec.Value));
            if (@protected.HasValue)
                parameters.Add(new KeyValuePair<string, object>("protected", @protected.Value.ToString().ToLower()));

            return AppClient.PostAsync<User>("account/update_profile.json", parameters, true);
        }
    }
}