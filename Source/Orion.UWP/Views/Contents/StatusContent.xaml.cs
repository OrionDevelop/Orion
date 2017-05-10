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
            DependencyProperty.Register(nameof(IsSelected), typeof(bool), typeof(StatusContent), new PropertyMetadata(false, PropertyChangedCallback));

        public bool IsSelected
        {
            get => (bool) GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        public StatusContent()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            AppBar.Visibility = Visibility.Collapsed;
            AppBar.Height = 0;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var root = this.GetFirstAncestorOfType<ListViewItem>();
            if (root == null)
                return;

            SetBinding(IsSelectedProperty, new Binding
            {
                Source = root,
                Path = new PropertyPath(nameof(root.IsSelected)),
                Mode = BindingMode.TwoWay
            });
        }

        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if ((bool) args.NewValue)
                (dependencyObject as StatusContent)?.ExpandCommandBar();
            else
                (dependencyObject as StatusContent)?.ContractCommandBar();
        }

        private void ContractCommandBar()
        {
            if (RootPanel.ActualHeight > 40)
                RootPanel.Height = RootPanel.ActualHeight - 40;
            AppBar.Visibility = Visibility.Collapsed;
            AppBar.Height = 0;
        }

        private void ExpandCommandBar()
        {
            RootPanel.Height = RootPanel.ActualHeight + 40;
            AppBar.Visibility = Visibility.Visible;
            AppBar.Height = 40;
        }
    }
}