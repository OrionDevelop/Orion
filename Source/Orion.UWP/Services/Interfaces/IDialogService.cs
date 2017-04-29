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
        /// <param name="type"></param>
        /// <returns></returns>
        Task ShowDialogAsync(Type type);

        /// <summary>
        ///     Show dialog.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        Task ShowDialogAsync(ContentDialog instance);
    }
}