using System;

namespace Orion.UWP.Models.Enum
{
    public enum TimelineType
    {
        /// <summary>
        ///     Home timeline
        /// </summary>
        HomeTimeline,

        /// <summary>
        ///     Mentions
        /// </summary>
        Mentions,

        /// <summary>
        ///     Direct messages
        /// </summary>
        DirectMessages,

        /// <summary>
        ///     Notifications (Not support: GNU social, Croudia)
        /// </summary>
        Notifications,

        /// <summary>
        ///     Public timeline (Not support: Twitter)
        /// </summary>
        PublicTimeline,

        /// <summary>
        ///     Federated timeline (Not support: Twitter, Croudia)
        /// </summary>
        FederatedTimeline
    }

    public static class TimelineTypeEx
    {
        public static string ToName(this TimelineType obj)
        {
            switch (obj)
            {
                case TimelineType.HomeTimeline:
                    return "Home";

                case TimelineType.Mentions:
                    return "Mentions";

                case TimelineType.DirectMessages:
                    return "Direct messages";

                case TimelineType.Notifications:
                    return "Notifications";

                case TimelineType.PublicTimeline:
                    return "Local";

                case TimelineType.FederatedTimeline:
                    return "Federated";

                default:
                    throw new ArgumentOutOfRangeException(nameof(obj), obj, null);
            }
        }

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