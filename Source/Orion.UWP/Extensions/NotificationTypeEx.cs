using System;

using Orion.Shared.Absorb.Objects;

namespace Orion.UWP.Extensions
{
    public static class NotificationTypeEx
    {
        public static string ToIcon(this NotificationType obj)
        {
            switch (obj)
            {
                case NotificationType.Followed:
                    return "\uE8FA"; // AddFriend

                case NotificationType.Favorited:
                    return "\uE735"; // FavoriteStarFill

                case NotificationType.Reblogged:
                    return "\uE8EE"; // RepeatAll

                case NotificationType.Mention:
                    throw new NotSupportedException(); // As status.

                default:
                    throw new ArgumentOutOfRangeException(nameof(obj), obj, null);
            }
        }
    }
}