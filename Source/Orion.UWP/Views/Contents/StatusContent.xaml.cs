using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

using WinRTXamlToolkit.Controls.Extensions;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Orion.UWP.Views.Contents
{
    public sealed partial class StatusContent : UserControl
    {
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register(nameof(IsSelected), typeof(bool), typeof(StatusContent), new PropertyMetadata(false, OnIsSelectedChanged));

        public static readonly DependencyProperty IsMiniModeProperty =
            DependencyProperty.Register(nameof(IsMiniMode), typeof(bool), typeof(StatusContent), new PropertyMetadata(false, OnIsMiniModeChanged));

        public bool IsSelected
        {
            get => (bool) GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        public bool IsMiniMode
        {
            get => (bool) GetValue(IsMiniModeProperty);
            set => SetValue(IsMiniModeProperty, value);
        }

        public StatusContent()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            AppBar.Visibility = Visibility.Collapsed;
            AppBar.Height = 0;
            Body.IsTextSelectionEnabled = false;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var root = this.GetFirstAncestorOfType<ListViewItem>();
            if (root == null)
                return;

            if (IsMiniMode)
                return;

            SetBinding(IsSelectedProperty, new Binding
            {
                Source = root,
                Path = new PropertyPath(nameof(root.IsSelected)),
                Mode = BindingMode.TwoWay
            });
        }

        private static void OnIsSelectedChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if ((bool) args.NewValue)
                (dependencyObject as StatusContent)?.ExpandCommandBar();
            else
                (dependencyObject as StatusContent)?.ContractCommandBar();
        }

        private static void OnIsMiniModeChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is StatusContent status && (bool) args.NewValue)
            {
                status.Icon.Height = status.Icon.Width = 32;
                status.Username.FontSize = status.Body.FontSize = 13;
                status.ScreenName.FontSize = status.Timestamp.FontSize = 12;
            }
        }

        private void ContractCommandBar()
        {
            if (RootPanel.ActualHeight > 40)
                RootPanel.Height = RootPanel.ActualHeight - 40;
            AppBar.Visibility = Visibility.Collapsed;
            AppBar.Height = 0;
            Body.IsTextSelectionEnabled = false;
        }

        private void ExpandCommandBar()
        {
            RootPanel.Height = RootPanel.ActualHeight + 40;
            AppBar.Visibility = Visibility.Visible;
            AppBar.Height = 40;
            Body.IsTextSelectionEnabled = true;
        }
    }
}