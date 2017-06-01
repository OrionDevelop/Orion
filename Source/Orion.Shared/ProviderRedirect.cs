using System;
using System.Collections.Generic;
using System.Linq;

namespace Orion.Shared
{
    internal static class ProviderRedirect
    {
        private static readonly Dictionary<string, string> RedirectHosts = new Dictionary<string, string>
        {
            ["mstdn.jp"] = "streaming.mstdn.jp",
            ["qiitadon.com"] = "streaming.qiitadon.com:4000",
        };

        public static string Redirect(string url)
        {
            var host = new Uri(url).Host;
            return RedirectHosts.Any(w => w.Key == host) ? RedirectHosts.First(w => w.Key == host).Value : host;
        }
    }
}