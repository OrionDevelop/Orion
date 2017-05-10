using Newtonsoft.Json;

namespace Orion.Service.Croudia.Models
{
    public class RelationShip
    {
        [JsonProperty("source")]
        public Relation Source { get; set; }

        [JsonProperty("target")]
        public Relation Target { get; set; }
    }
}