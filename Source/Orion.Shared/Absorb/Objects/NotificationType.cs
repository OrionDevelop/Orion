using System;

namespace Orion.UWP.Models.Absorb
{
    public enum NotificationType
    {
        /// <summary>
        ///     フォローされた
        /// </summary>
        Followed,

        /// <summary>
        ///     お気に入り登録された
        /// </summary>
        Favorited,

        /// <summary>
        ///     リブログされた
        /// </summary>
        Reblogged,

        /// <summary>
        ///     返信がきた
        /// </summary>
        Mention
    }

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

        public static string ToMessage(this NotificationType obj)
        {
            switch (obj)
            {
                case NotificationType.Followed:
                    return "{0} followed you";

                case NotificationType.Favorited:
                    return "{0} favorited your status";

                case NotificationType.Reblogged:
                    return "{0} reblogged your status";

                case NotificationType.Mention:
                    throw new NotSupportedException();
                default:
                    throw new ArgumentOutOfRangeException(nameof(obj), obj, null);
            }
        }
    }
}