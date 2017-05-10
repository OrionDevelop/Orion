using System;
using System.Threading.Tasks;

using Windows.UI.Xaml.Controls;

namespace Orion.UWP.Services.Interfaces
{
    public interface IDialogService
    {
        /// <summary>
        ///     Show dialog.
        /// </summary>
        /// <returns></returns>
        Task ShowDialogAsync<T>();

        /// <summary>
        ///     Show dialog.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task ShowDialogAsync(Type t);

        /// <summary>
        ///     Show dialog.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        Task ShowDialogAsync(ContentDialog instance);
    }
}