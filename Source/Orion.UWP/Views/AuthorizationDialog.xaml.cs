using System;
using System.Reactive.Linq;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Orion.UWP.ViewModels;

using Reactive.Bindings.Extensions;

// コンテンツ ダイアログの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace Orion.UWP.Views
{
    public sealed partial class AuthorizationDialog : ContentDialog
    {
        public AuthorizationDialogViewModel ViewModel => DataContext as AuthorizationDialogViewModel;

        public AuthorizationDialog()
        {
            InitializeComponent();
        }

        // Note: Why don't notify property changed?
        private void WebView_OnNavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            if (ViewModel?.Source == null)
                return;

            // Avoid double notify
            if(ViewModel?.SelectedProvider?.Value?.ParseRegex == null)
                return;

            if (ViewModel.SelectedProvider.Value.ParseRegex.IsMatch(args.Uri.ToString()))
                ViewModel.Source.Value = args.Uri;
        }

        private void AuthorizationDialog_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (ViewModel?.CanClose != null)
                ViewModel.ObserveProperty(w => w.CanClose).Where(w => w).Subscribe(_ => Hide());
        }
    }
}