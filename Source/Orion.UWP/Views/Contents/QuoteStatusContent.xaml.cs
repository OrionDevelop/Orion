using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

using Orion.UWP.ViewModels.Contents;

using WinRTXamlToolkit.Controls.Extensions;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Orion.UWP.Views.Contents
{
    public sealed partial class QuoteStatusContent : UserControl
    {
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel), typeof(QuoteStatusViewModel), typeof(QuoteStatusContent),
                                        new PropertyMetadata(null, OnViewModelChanged));

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register(nameof(IsSelected), typeof(bool), typeof(StatusContent), new PropertyMetadata(false, OnIsSelectedChanged));

        public static readonly DependencyProperty IsIconRoundedProperty =
            DependencyProperty.Register(nameof(IsIconRounded), typeof(bool), typeof(StatusContent), new PropertyMetadata(false, OnIsIconRoundedChanged));

        public QuoteStatusViewModel ViewModel
        {
            get => (QuoteStatusViewModel) GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        public bool IsSelected
        {
            get => (bool) GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        public bool IsIconRounded
        {
            get => (bool) GetValue(IsIconRoundedProperty);
            set => SetValue(IsIconRoundedProperty, value);
        }

        public QuoteStatusContent()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            AppBar.Visibility = Visibility.Collapsed;
            AppBar.Height = 0;
            Body.IsTextSelectionEnabled = false;
        }

        private static void OnViewModelChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (args.NewValue != null && args.NewValue != args.OldValue)
                (dependencyObject as QuoteStatusContent)?.ResetLayouts();
        }

        private static void OnIsIconRoundedChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (dependencyObject is QuoteStatusContent status)
                status.UpdateIcon();
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

        private static void OnIsSelectedChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if ((bool) args.NewValue)
                (dependencyObject as QuoteStatusContent)?.ExpandCommandBar();
            else
                (dependencyObject as QuoteStatusContent)?.ContractCommandBar();
        }

        private void UpdateIcon()
        {
            if (IsIconRounded)
                Icon.CornerRadius = new CornerRadius(16);
            else if (IsIconRounded)
                Icon.CornerRadius = new CornerRadius(24);
            else
                Icon.CornerRadius = new CornerRadius(4);
        }

        private void ResetLayouts()
        {
            AppBar.Visibility = Visibility.Collapsed;
            AppBar.Height = 0;
            Body.IsTextSelectionEnabled = false;
            Body.Measure(new Size());
            ImagePreviews.Measure(new Size());
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