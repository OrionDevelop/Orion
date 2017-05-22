using System;

using Windows.UI.Xaml.Controls;

namespace Orion.UWP.Services.Interfaces
{
    public interface IOrionNavigationService
    {
        bool Navigate(string pageToken, object parameter = null);

        bool Navigate(Type pageType, object parameter = null);

        void GoBack();

        bool CanGoBack();

        void ConfigureRootFrame(Frame rootFrame);
    }
}