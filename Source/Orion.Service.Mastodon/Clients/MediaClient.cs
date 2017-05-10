using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Orion.Service.Mastodon.Models;
using Orion.Service.Shared;

// ReSharper disable once CheckNamespace

namespace Orion.Service.Mastodon.Clients
{
    public class MediaClient : ApiClient<MastodonClient>
    {
        internal MediaClient(MastodonClient mastodonClent) : base(mastodonClent) { }

        public Task<Attachment> CreateAsync(string filePath)
        {
            var content = new StreamContent(File.OpenRead(filePath));
            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("file")
            {
                FileName = Path.GetFileName(filePath)
            };
            var parameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("file", content)
            };
            return AppClient.PostAsync<Attachment>("api/v1/media", parameters);
        }
    }
}