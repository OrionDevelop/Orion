using System;
using System.Threading.Tasks;

using Windows.UI.Xaml.Controls;

using Orion.UWP.Services.Interfaces;

namespace Orion.UWP.Services
{
    internal class DialogService : IDialogService
    {
        public async Task ShowDialogAsync<T>()
        {
            var dialog = Activator.CreateInstance(typeof(T)) as ContentDialog;
            if (dialog != null)
                await dialog.ShowAsync();
        }

        public async Task ShowDialogAsync(Type t)
        {
            var dialog = Activator.CreateInstance(t) as ContentDialog;
            if (dialog != null)
                await dialog.ShowAsync();
        }

        public async Task ShowDialogAsync(ContentDialog instance)
        {
            await instance.ShowAsync();
        }
    }
}