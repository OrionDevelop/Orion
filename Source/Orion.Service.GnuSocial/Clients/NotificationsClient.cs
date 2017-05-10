using System;
using System.Threading.Tasks;

using Orion.Service.Shared;

namespace Orion.Service.GnuSocial.Clients
{
    public class NotificationsClient : ApiClient<GnuSocialClient>
    {
        internal NotificationsClient(GnuSocialClient client) : base(client) { }

        public Task FollowAsync()
        {
            throw new NotImplementedException();
        }

        public Task LeaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}