using System;
using System.Threading.Tasks;

namespace Orion.UWP.Services.Interfaces
{
    public interface IDialogService
    {
        Task ShowDialogAsync(Type type);
    }
}