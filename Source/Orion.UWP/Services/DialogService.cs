using System;
using System.Threading.Tasks;

using Windows.UI.Xaml.Controls;

using Orion.UWP.Services.Interfaces;

namespace Orion.UWP.Services
{
    internal class DialogService : IDialogService
    {
        public async Task ShowDialogAsync(Type type)
        {
            var dialog = Activator.CreateInstance(type) as ContentDialog;
            if (dialog != null)
                await dialog.ShowAsync();
        }

        public async Task ShowDialogAsync(ContentDialog instance)
        {
            await instance.ShowAsync();
        }
    }
}