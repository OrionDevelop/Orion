using System.Collections.Generic;
using System.Threading.Tasks;

using Orion.Service.Croudia.Models;
using Orion.Service.Shared;

namespace Orion.Service.Croudia.Clients
{
    public class TrendsClient : ApiClient<CroudiaClient>
    {
        internal TrendsClient(CroudiaClient client) : base(client) { }

        public Task<PlaceTrend> PlaceAsync()
        {
            var parameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("id", 1)
            };

            return AppClient.GetAsync<PlaceTrend>("trends/place.json", parameters);
        }
    }
}