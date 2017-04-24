using Orion.Service.Shared;

namespace Orion.Service.GnuSocial.Clients
{
    public class DirectMessagesClient : ApiClient<GnuSocialClient>
    {
        internal DirectMessagesClient(GnuSocialClient client) : base(client) { }
    }
}