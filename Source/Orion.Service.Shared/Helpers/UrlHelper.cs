using System.Collections.Generic;

namespace Orion.Service.Shared.Helpers
{
    public static class UrlHelper
    {
        public static Dictionary<string, string> ParseUrlQuery(string query)
        {
            var dictionary = new Dictionary<string, string>();
            foreach (var s in query.Split('&'))
            {
                var kvp = s.Split('=');
                dictionary[kvp[0]] = kvp[1];
            }
            return dictionary;
        }
    }
}