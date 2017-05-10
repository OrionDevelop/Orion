using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Orion.Service.Croudia.Models;
using Orion.Service.Shared;

namespace Orion.Service.Croudia.Clients
{
    public class SecretMailsClient : ApiClient<CroudiaClient>
    {
        internal SecretMailsClient(CroudiaClient client) : base(client) { }

        public Task<IEnumerable<SecretMail>> ReceivedAsync(int? sinceId = null, int? maxId = null, int? count = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            ApplyParams(parameters, sinceId, maxId, count);

            return AppClient.GetAsync<IEnumerable<SecretMail>>("secret_mails.json", parameters, true);
        }

        public Task<IEnumerable<SecretMail>> SentAsync(int? sinceId = null, int? maxId = null, int? count = null)
        {
            var parameters = new List<KeyValuePair<string, object>>();
            ApplyParams(parameters, sinceId, maxId, count);

            return AppClient.GetAsync<IEnumerable<SecretMail>>("secret_mails/sent.json", parameters, true);
        }

        public Task<SecretMail> NewAsync(string text, string screenName = null, int? userId = null)
        {
            var parameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("text", text)
            };
            if (!string.IsNullOrWhiteSpace(screenName))
                parameters.Add(new KeyValuePair<string, object>("screen_name", screenName));
            else if (userId.HasValue)
                parameters.Add(new KeyValuePair<string, object>("user_id", userId.Value));
            else
                throw new ArgumentNullException();

            return AppClient.PostAsync<SecretMail>("secret_mails/new.json", parameters, true);
        }

        public Task<SecretMail> NewWithMediaAsync(string text, string media, string screenName = null, int? userId = null)
        {
            var parameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("text", text),
                new KeyValuePair<string, object>("media", media)
            };
            if (!string.IsNullOrWhiteSpace(screenName))
                parameters.Add(new KeyValuePair<string, object>("screen_name", screenName));
            else if (userId.HasValue)
                parameters.Add(new KeyValuePair<string, object>("user_id", userId.Value));
            else
                throw new ArgumentNullException();

            return AppClient.PostAsync<SecretMail>("secret_mails/new_with_media.json", parameters, true);
        }

        public Task<SecretMail> DestroyAsync(int id)
        {
            return AppClient.PostAsync<SecretMail>($"secret_mails/destroy/{id}.json", requireAuth: true);
        }

        public Task<SecretMail> ShowAsync(int id)
        {
            return AppClient.GetAsync<SecretMail>($"secret_mails/show/{id}.json", requireAuth: true);
        }

        public Task<Stream> GetSecretPhotoAsync(int id)
        {
            throw new NotImplementedException();
        }

        private void ApplyParams(List<KeyValuePair<string, object>> parameters, int? sinceId, int? maxId, int? count)
        {
            if (sinceId.HasValue)
                parameters.Add(new KeyValuePair<string, object>("since_id", sinceId.Value));
            if (maxId.HasValue)
                parameters.Add(new KeyValuePair<string, object>("max_id", maxId.Value));
            if (count.HasValue)
                parameters.Add(new KeyValuePair<string, object>("count", count.Value));
        }
    }
}