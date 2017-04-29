using System;
using System.Threading.Tasks;

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
    }
}