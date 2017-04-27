using System.Collections.Generic;
using System.Collections.ObjectModel;

using Orion.UWP.Models.Enum;

namespace Orion.UWP.Models
{
    internal static class Constants
    {
        public static ReadOnlyCollection<Provider> Providers { get; } = new List<Provider>
        {
            new Provider {Name = "Twitter", Host = "twitter.com", Service = Service.Twitter, RequireHost = false, RequireApiKeys = false},
            new Provider {Name = "FreezePeach", Host = "freezepeach.xyz", Service = Service.GnuSocial, RequireHost = false, RequireApiKeys = false},
            new Provider {Name = "GNU/Smug", Host = "gs.smuglo.li", Service = Service.GnuSocial, RequireHost = false, RequireApiKeys = false},
            new Provider {Name = "GNU social", Service = Service.GnuSocial, RequireHost = true, RequireApiKeys = true},
            new Provider {Name = "Mastodon", Service = Service.Mastodon, RequireHost = true, RequireApiKeys = false}
        }.AsReadOnly();
    }
}