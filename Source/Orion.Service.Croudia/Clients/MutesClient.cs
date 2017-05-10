using Orion.Service.Shared;

namespace Orion.Service.Croudia.Clients
{
    public class MutesClient : ApiClient<CroudiaClient>
    {
        public Mutes.UsersClient Users { get; }

        internal MutesClient(CroudiaClient client) : base(client)
        {
            Users = new Mutes.UsersClient(client);
        }
    }
}