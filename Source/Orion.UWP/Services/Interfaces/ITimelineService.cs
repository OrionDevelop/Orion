using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Orion.Shared.Models;

namespace Orion.UWP.Services.Interfaces
{
    public interface ITimelineService
    {
        ReadOnlyObservableCollection<TimelineBase> Timelines { get; }

        /// <summary>
        ///     初期タイムラインを構成します。
        /// </summary>
        /// <returns></returns>
        Task InitializeAsync();

        /// <summary>
        ///     タイムラインを全て削除します。
        /// </summary>
        /// <returns></returns>
        Task ClearAsync();

        /// <summary>
        ///     タイムラインを復元します。
        /// </summary>
        /// <returns></returns>
        Task RestoreAsync();

        /// <summary>
        ///     タイムラインを保存します。
        /// </summary>
        /// <returns></returns>
        Task SaveAsync();

        /// <summary>
        ///     タイムラインを追加します。
        /// </summary>
        /// <returns></returns>
        Task AddAsync(TimelineBase timeline);

        /// <summary>
        ///     タイムラインを削除します。
        /// </summary>
        /// <returns></returns>
        Task RemoveAsync(TimelineBase timeline);

        /// <summary>
        ///     タイムラインを並び替えます。
        /// </summary>
        /// <returns></returns>
        Task OrderAsync(TimelineBase timeline, int index);
    }
}