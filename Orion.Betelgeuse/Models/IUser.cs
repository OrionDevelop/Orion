using System;

namespace Orion.Betelgeuse.Models
{
    public interface IUser
    {
        long Id { get; set; }

        DateTime CreatedAt { get; set; }

        string Name { get; set; }

        string ScreenName { get; set; }

        string Description { get; set; }

        string Location { get; set; }

        string Url { get; set; }

        string ProfileImageUrl { get; set; }

        string ProfileBannerUrl { get; set; }

        bool IsFollowing { get; set; }

        bool IsProtected { get; set; }

        long FriendsCount { get; set; }

        long FollowersCount { get; set; }

        long StatusesCount { get; set; }

        long FavoritesCount { get; set; }
    }
}