using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

using Orion.UWP.Models.Enum;

namespace Orion.UWP.Models
{
    internal static class Constants
    {
        public static string OAuthCallback = "https://kokoiroworks.com/callback";

        public static Provider TwitterProvider { get; } = new Provider
        {
            Name = "Twitter",
            Host = "api.twitter.com",
            Service = ServiceType.Twitter,
            RequireHost = false,
            RequireApiKeys = false,
            ClientId = "IUWEAzTZJLcmfB7RFVErvVyLM",
            ClientSecret = "bgJDN2WfJwzMZUhWK5lVHp8NklqIOKZ6f5ZscrlrzxPz87BbBf",
            ParseRegex = new Regex(@"oauth_verifier=(?<verifier>[A-Za-z0-9]+)", RegexOptions.Compiled)
        };

        public static Provider CroudiaProvider { get; } = new Provider
        {
            Name = "Croudia",
            Host = "api.croudia.com",
            Service = ServiceType.Croudia,
            RequireHost = false,
            RequireApiKeys = false,
            ClientId = "a278d96eb670a7008c057191a915e4b8b23532427f229eafa925612ad574bd4f",
            ClientSecret = "df93f525140c43dbe701b29e7819877bf59fa11f66f49295f5be4fdbe03317e3",
            ParseRegex = new Regex(@"\?code=(?<verifier>[0-9a-z]+)", RegexOptions.Compiled)
        };

        public static Provider FreezePeachProvider { get; } = new Provider
        {
            Name = "FreezePeach",
            Host = "freezepeach.xyz",
            Service = ServiceType.GnuSocial,
            RequireHost = false,
            RequireApiKeys = false,
            ClientId = "4625863928048353df5cf80df5880ca5",
            ClientSecret = "d9a98e70a14751ab2315549b4b493e94",
            ParseRegex = new Regex(@"oauth_verifier=(?<verifier>[a-z0-9]+)", RegexOptions.Compiled)
        };

        public static Provider GnuSmugProvider { get; } = new Provider
        {
            Name = "GNU/Smug",
            Host = "gs.smuglo.li",
            Service = ServiceType.GnuSocial,
            RequireHost = false,
            RequireApiKeys = false,
            ClientId = "96ad60799296c03da82f63e584507b2d",
            ClientSecret = "1729201fa07fad023fed97926241f212",
            ParseRegex = new Regex(@"oauth_verifier=(?<verifier>[a-z0-9]+)", RegexOptions.Compiled)
        };

        public static ReadOnlyCollection<Provider> Providers { get; } = new List<Provider>
        {
            TwitterProvider,
            CroudiaProvider,
            FreezePeachProvider,
            GnuSmugProvider,
            new Provider
            {
                Name = "GNU social",
                Service = ServiceType.GnuSocial,
                RequireHost = true,
                RequireApiKeys = true,
                ParseRegex = new Regex(@"oauth_verifier=(?<verifier>[a-z0-9]+)", RegexOptions.Compiled)
            },
            new Provider
            {
                Name = "Mastodon",
                Service = ServiceType.Mastodon,
                RequireHost = true,
                RequireApiKeys = false,
                ParseRegex = new Regex(@"code=(?<verifier>[0-9a-z]+)", RegexOptions.Compiled)
            }
        }.AsReadOnly();
    }
}