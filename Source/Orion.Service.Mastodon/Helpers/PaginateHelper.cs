using System.Collections.Generic;

namespace Orion.Service.Mastodon.Helpers
{
    public static class PaginateHelper
    {
        public static void ApplyParams(List<KeyValuePair<string, object>> parameters, int? maxId = null, int? sinceId = null)
        {
            if (maxId.HasValue)
                parameters.Add(new KeyValuePair<string, object>("max_id", maxId));
            if (sinceId.HasValue)
                parameters.Add(new KeyValuePair<string, object>("since_id", sinceId));
        }
    }
}