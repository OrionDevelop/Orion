using System;

namespace Orion.Service.Mastodon.Enum
{
    public enum VisibilityType
    {
        Public,

        Unlisted,

        Private,

        Direct
    }

    public static class VisibilityTypeExt
    {
        public static string ToParameter(this VisibilityType obj)
        {
            switch (obj)
            {
                case VisibilityType.Public:
                    return "public";

                case VisibilityType.Unlisted:
                    return "unlisted";

                case VisibilityType.Private:
                    return "private";

                case VisibilityType.Direct:
                    return "direct";

                default:
                    throw new ArgumentOutOfRangeException(nameof(obj), obj, null);
            }
        }
    }
}