using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Orion.Service.GnuSocial.Models;
using Orion.Service.Shared;
using Orion.Service.Shared.Helpers;

namespace Orion.Service.GnuSocial.Clients
{
    public class AccountClient : ApiClient<GnuSocialClient>
    {
        internal AccountClient(GnuSocialClient client) : base(client) { }

        public Task<User> VerifyCredentialsAsync()
        {
            return AppClient.GetAsync<User>("account/verify_credentials.json", requireAuth: true);
        }

        public Task EndSessionAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateDeliveryDeviceAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RateLimitStatus> RateLimitStatusAsync()
        {
            return AppClient.GetAsync<RateLimitStatus>("account/rate_limit_status.json");
        }

        public Task<User> UpdateProfileBackgroundImageAsync(string image)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            if (!string.IsNullOrWhiteSpace(image))
                parameters.Add(new KeyValuePair<string, object>("image", FileHelper.FileToBase64Strings(image)));

            return AppClient.PostAsync<User>("account/update_profile_background_image.json", parameters);
        }

        public Task<User> UpdateProfileImageAsync(string image)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            if (!string.IsNullOrWhiteSpace(image))
                parameters.Add(new KeyValuePair<string, object>("image", FileHelper.FileToBase64Strings(image)));

            return AppClient.PostAsync<User>("account/update_profile_image.json", parameters);
        }
    }
}