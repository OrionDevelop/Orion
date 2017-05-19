using System;

using Windows.UI;
using Windows.UI.Xaml.Media;

using Orion.Shared.Absorb.Enums;

namespace Orion.UWP.Extensions
{
    public static class EventTypeEx
    {
        public static SolidColorBrush ToColor(this EventType obj)
        {
            switch (obj)
            {
                case EventType.Favorite:
                    return new SolidColorBrush(Colors.Yellow);

                case EventType.Follow:
                    return new SolidColorBrush(Colors.DeepSkyBlue);
                    ;

                case EventType.Reblog:
                    return new SolidColorBrush(Colors.LightGreen);

                default:
                    throw new ArgumentOutOfRangeException(nameof(obj), obj, null);
            }
        }

        public static string ToFormatMessage(this EventType obj)
        {
            switch (obj)
            {
                case EventType.Favorite:
                    return "{0} favorited your status.";

                case EventType.Follow:
                    return "{0} followed you.";

                case EventType.Reblog:
                    return "{0} reblogged your status.";

                default:
                    throw new ArgumentOutOfRangeException(nameof(obj), obj, null);
            }
        }

        public static string ToIcon(this EventType obj)
        {
            switch (obj)
            {
                case EventType.Favorite:
                    return "\uE735"; // FavoriteStarFill

                case EventType.Follow:
                    return "\uE8FA"; // AddFriend

                case EventType.Reblog:
                    return "\uE8EE"; // RepeatAll

                default:
                    throw new ArgumentOutOfRangeException(nameof(obj), obj, null);
            }
        }
    }
}