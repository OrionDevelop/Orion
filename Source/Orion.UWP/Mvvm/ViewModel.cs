using System;
using System.Collections.Generic;
using System.Reactive.Disposables;

using Windows.UI.Core;

using Microsoft.Practices.Unity;

using Orion.UWP.Services.Interfaces;

using Prism.Unity.Windows;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;

namespace Orion.UWP.Mvvm
{
    public class ViewModel : ViewModelBase, IDisposable
    {
        protected IOrionNavigationService NavigationService { get; }
        public CompositeDisposable CompositeDisposable { get; }

        protected ViewModel()
        {
            CompositeDisposable = new CompositeDisposable();
            NavigationService = PrismUnityApplication.Current.Container.Resolve<IOrionNavigationService>();
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            CompositeDisposable.Dispose();
        }

        #endregion

        #region Overrides of ViewModelBase

        public override void OnNavigatingFrom(NavigatingFromEventArgs e, Dictionary<string, object> viewModelState, bool suspending)
        {
            base.OnNavigatingFrom(e, viewModelState, suspending);
            CompositeDisposable.Dispose();
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);

            // Back Button
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = NavigationService.CanGoBack()
                ? AppViewBackButtonVisibility.Visible
                : AppViewBackButtonVisibility.Collapsed;
        }

        #endregion
    }
}