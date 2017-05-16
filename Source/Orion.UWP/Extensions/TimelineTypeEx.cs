using System;

using Orion.Shared.Enums;

namespace Orion.UWP.Extensions
{
    public static class TimelineTypeEx
    {
        public static string ToIcon(this TimelineType obj)
        {
            switch (obj)
            {
                case TimelineType.HomeTimeline:
                    return "\uE80F"; // Home

                case TimelineType.Mentions:
                    return "\uE910"; // Accounts

                case TimelineType.DirectMessages:
                    return "\uE8BD"; // Message

                case TimelineType.Notifications:
                    return "\uEA8F"; // Ringer

                case TimelineType.PublicTimeline:
                    return "\uEC27"; // MyNetwotk

                case TimelineType.FederatedTimeline:
                    return "\uE909"; // World

                default:
                    throw new ArgumentOutOfRangeException(nameof(obj), obj, null);
            }
        }
    }
}