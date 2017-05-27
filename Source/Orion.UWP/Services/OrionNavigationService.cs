using System;

using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using Orion.UWP.Mvvm;
using Orion.UWP.Services.Interfaces;

using Prism.Windows.AppModel;
using Prism.Windows.Navigation;

namespace Orion.UWP.Services
{
    // https://github.com/PrismLibrary/Prism/blob/master/Source/Windows10/Prism.Windows/Navigation/FrameNavigationService.cs
    internal class OrionNavigationService : IOrionNavigationService
    {
        private const string LastNavigationParameterKey = "LastNavigationParameter";
        private const string LastNavigationPageKey = "LastNavigationPageKey";
        private readonly ISessionStateService _sessionStateService;
        private Frame _rootFrame;

        public OrionNavigationService(ISessionStateService sessionStateService)
        {
            _sessionStateService = sessionStateService;
            SystemNavigationManager.GetForCurrentView().BackRequested += (sender, args) =>
            {
                if (!CanGoBack())
                    return;

                GoBack();
                args.Handled = true;
            };
        }

        public bool Navigate(string pageToken, object parameter = null)
        {
            var type = Type.GetType($"{typeof(App).Namespace}.Views.Partials.{pageToken}Page");
            return Navigate(type, parameter);
        }

        public bool Navigate(Type pageType, object parameter = null)
        {
            var lastNavigationParameter = _sessionStateService.SessionState.ContainsKey(LastNavigationParameterKey)
                ? _sessionStateService.SessionState[LastNavigationParameterKey]
                : null;
            var lastPageTypeFullName = _sessionStateService.SessionState.ContainsKey(LastNavigationPageKey)
                ? _sessionStateService.SessionState[LastNavigationPageKey] as string
                : string.Empty;

            if (lastPageTypeFullName != pageType.FullName || lastNavigationParameter != null && !lastNavigationParameter.Equals(parameter))
                return _rootFrame.Navigate(pageType, parameter);
            return false;
        }

        public void GoBack()
        {
            if (CanGoBack())
                _rootFrame.GoBack();
        }

        public bool CanGoBack()
        {
            return _rootFrame.CanGoBack;
        }

        public void ConfigureRootFrame(Frame rootFrame)
        {
            _rootFrame = rootFrame;
            _rootFrame.Navigated += OnNavigatedTo;
            _rootFrame.Navigating += OnNavigatingFrom;
        }

        private void OnNavigatingFrom(object sender, NavigatingCancelEventArgs e)
        {
            var departingView = _rootFrame.Content as FrameworkElement;
            var departingViewModel = departingView?.DataContext as INavigationAware;

            departingViewModel?.OnNavigatingFrom(new NavigatingFromEventArgs(e), null, false);
        }

        private void OnNavigatedTo(object sender, NavigationEventArgs e)
        {
            if (_rootFrame?.Content == null)
                return;
            _sessionStateService.SessionState[LastNavigationPageKey] = _rootFrame.Content.GetType().FullName;
            _sessionStateService.SessionState[LastNavigationParameterKey] = e.Parameter;

            var frame = _rootFrame.Content as FrameworkElement;
            var viewModel = frame?.DataContext as ViewModel;
            viewModel?.OnNavigatedTo(new NavigatedToEventArgs
            {
                NavigationMode = e.NavigationMode,
                Parameter = _sessionStateService.SessionState[LastNavigationParameterKey]
            }, null);
        }
    }
}