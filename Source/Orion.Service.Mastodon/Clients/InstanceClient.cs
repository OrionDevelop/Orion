using System.Threading.Tasks;

using Orion.Service.Mastodon.Models;
using Orion.Service.Shared;

// ReSharper disable once CheckNamespace

namespace Orion.Service.Mastodon.Clients
{
    public class InstanceClient : ApiClient<MastodonClient>
    {
        internal InstanceClient(MastodonClient mastodonClent) : base(mastodonClent) { }

        public Task<Instance> ShowAsync()
        {
            return AppClient.GetAsync<Instance>("api/v1/instance");
        }
    }
}