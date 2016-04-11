using System;

namespace Orion.Betelgeuse.Models
{
    public interface IStatus
    {
        long Id { get; set; }

        long InReplyToStatusId { get; set; }

        long InReplyToUserId { get; set; }

        string InReplyToScreenName { get; set; }

        DateTime CreatedAt { get; set; }

        string Text { get; set; }

        string Source { get; set; }

        long RetweetedCount { get; set; }

        long FavoritedCount { get; set; }

        bool IsRetweeted { get; set; }

        bool IsFavorited { get; set; }

        IUser User { get; set; }
    }
}