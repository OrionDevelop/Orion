using Orion.UWP.Models.Enum;

namespace Orion.UWP.Models
{
    public class Provider
    {
        public string Name { get; set; }

        public Service Service { get; set; }

        public string Host { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public bool RequireHost { get; set; } = true;

        public bool RequireApiKeys { get; set; } = true;
    }
}