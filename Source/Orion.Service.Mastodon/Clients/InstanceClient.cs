using System.Threading.Tasks;

using Orion.Service.Mastodon.Models;

// ReSharper disable once CheckNamespace

namespace Orion.Service.Mastodon.Clients
{
    public class InstanceClient : ApiClient
    {
        internal InstanceClient(MastodonClient mastodonClent) : base(mastodonClent) { }

        public Task<Instance> ShowAsync()
        {
            return MastodonClient.GetAsync<Instance>("api/v1/instance");
        }
    }
}