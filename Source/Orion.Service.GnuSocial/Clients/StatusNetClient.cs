using System.Threading.Tasks;

using Orion.Service.GnuSocial.Models.StatusNet;
using Orion.Service.Shared;

namespace Orion.Service.GnuSocial.Clients
{
    // Extended APIs for GNU social (StatusNet)
    public class StatusNetClient : ApiClient<GnuSocialClient>
    {
        internal StatusNetClient(GnuSocialClient client) : base(client) { }

        public Task<Config> ConfigAsync()
        {
            return AppClient.GetAsync<Config>("statusnet/config.json");
        }
    }
}