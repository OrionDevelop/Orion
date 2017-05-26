using Orion.Shared.Models;

namespace Orion.UWP.Extensions
{
    public static class TimelineEx
    {
        public static string ToIcon(this TimelineBase obj)
        {
            switch (obj.Name)
            {
                case "Home":
                    return "\uE80F"; // Home

                case "Mentions":
                    return "\uE910"; // Accounts

                case "Direct messages":
                    return "\uE8BD"; // Message

                case "Notifications":
                    return "\uEA8F"; // Ringer

                case "Local":
                case "Public":
                    return "\uEC27"; // MyNetwotk

                case "Federated":
                    return "\uE909"; // World

                default:
                    return "\uE7BC"; // ReadingList
            }
        }
    }
}