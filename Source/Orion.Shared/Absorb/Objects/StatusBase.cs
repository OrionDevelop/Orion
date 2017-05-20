using System;

namespace Orion.Shared.Absorb.Objects
{
    public class StatusBase
    {
        /// <summary>
        ///     ステータス ID (numeric)
        /// </summary>
        public long Id { get; protected set; }

        /// <summary>
        ///     投稿日時
        /// </summary>
        public DateTime CreatedAt { get; protected set; }

        /// <summary>
        ///     投稿ユーザー (User)
        /// </summary>
        public User User { get; protected  set; }

        /// <summary>
        ///     型
        /// </summary>
        public string Type { get; protected set; }
    }
}