using System;
using System.Collections.Generic;
using System.Linq;

namespace Orion.Shared
{
    // F**king mstdn.jp
    internal static class ProviderRedirect
    {
        private static readonly List<Tuple<string, string>> RedirectHosts = new List<Tuple<string, string>>
        {
            new Tuple<string, string>("mstdn.jp", "streaming.mstdn.jp")
        };

        public static string Redirect(string url)
        {
            var host = new Uri(url).Host;
            return RedirectHosts.Any(w => w.Item1 == host) ? RedirectHosts.First(w => w.Item1 == host).Item2 : host;
        }
    }
}