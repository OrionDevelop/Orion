using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

using Orion.UWP.ViewModels.Contents;

using WinRTXamlToolkit.AwaitableUI;
using WinRTXamlToolkit.Controls.Extensions;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Orion.UWP.Views.Contents
{
    public sealed partial class StatusContent : UserControl
    {
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel), typeof(StatusViewModel), typeof(StatusContent), new PropertyMetadata(null, OnViewModelChanged));

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register(nameof(IsSelected), typeof(bool), typeof(StatusContent), new PropertyMetadata(false, OnIsSelectedChanged));

        public static readonly DependencyProperty IsMiniModeProperty =
            DependencyProperty.Register(nameof(IsMiniMode), typeof(bool), typeof(StatusContent), new PropertyMetadata(false, OnIsMiniModeChanged));

        public static readonly DependencyProperty IsShowIconProperty =
            DependencyProperty.Register(nameof(IsShowIcon), typeof(bool), typeof(StatusContent), new PropertyMetadata(true, OnIsShowIconChanged));

        public static readonly DependencyProperty IsShowImagePreviewsProperty =
            DependencyProperty.Register(nameof(IsShowImagePreviews), typeof(bool), typeof(StatusContent),
                                        new PropertyMetadata(true, OnIsShowImagePreviewsChanged));

        public static readonly DependencyProperty IsShowTimestampProperty =
            DependencyProperty.Register(nameof(IsShowTimestamp), typeof(bool), typeof(StatusContent), new PropertyMetadata(true, OnIsShowTimestampChanged));

        public StatusViewModel ViewModel
        {
            get => (StatusViewModel) GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

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

        public bool IsShowIcon
        {
            get => (bool) GetValue(IsShowIconProperty);
            set => SetValue(IsShowIconProperty, value);
        }

        public bool IsShowImagePreviews
        {
            get => (bool) GetValue(IsShowImagePreviewsProperty);
            set => SetValue(IsShowImagePreviewsProperty, value);
        }

        public bool IsShowTimestamp
        {
            get => (bool) GetValue(IsShowTimestampProperty);
            set => SetValue(IsShowTimestampProperty, value);
        }

        public StatusContent()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            AppBar.Visibility = Visibility.Collapsed;
        }

        private static void OnViewModelChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            (dependencyObject as StatusContent)?.CalculateCellSize();
        }

        private static void OnIsShowTimestampChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (dependencyObject is StatusContent status)
                status.Timestamp.Visibility = (bool) e.NewValue ? Visibility.Visible : Visibility.Collapsed;
        }

        private static void OnIsShowImagePreviewsChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (dependencyObject is StatusContent status)
                status.ImagePreviews.Visibility = (bool) e.NewValue ? Visibility.Visible : Visibility.Collapsed;
        }

        private static void OnIsShowIconChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (dependencyObject is StatusContent status)
                if ((bool) e.NewValue)
                {
                    status.Icon.Visibility = Visibility.Visible;
                    status.UserLine.Padding = status.Body.Padding = new Thickness(10, 0, 0, 0);
                }
                else
                {
                    status.Icon.Visibility = Visibility.Collapsed;
                    status.UserLine.Padding = status.Body.Padding = new Thickness(0);
                }
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (IsMiniMode)
                return;

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

        private async void CalculateCellSize()
        {
            await this.WaitForLayoutUpdateAsync();

            var height = 0d;
            // UserLine does not change height.
            if (UserLine.DesiredSize == new Size(0, 0))
                UserLine.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            height += UserLine.DesiredSize.Height;
            // Body height is changed by text length. Must be re-calc.
            Body.Measure(new Size(250, double.PositiveInfinity));
            height += Body.DesiredSize.Height;
            // ImagePreviews is changed by attachments. Must be re-calc. (But when it don't have attachments, set to 0)
            if (ImagePreviews.Visibility != Visibility.Collapsed)
            {
                ImagePreviews.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                height += ImagePreviews.DesiredSize.Height;
            }
            // AppBar is changed by selection state. Must be re-calc.
            if (IsSelected)
            {
                AppBar.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                height += AppBar.DesiredSize.Height;
            }
            // Icon does not change height.
            if (Icon.DesiredSize == new Size(0, 0))
                Icon.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            if (height <= Icon.DesiredSize.Height)
                height = Icon.DesiredSize.Height;

            RootPanel.Height = height;
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
                status.Previews.ItemHeight = 40;
                status.Loaded += StatusOnLoaded;
            }
        }

        private static void StatusOnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (sender is StatusContent status)
            {
                status.Previews.ItemWidth = status.DesiredSize.Width / 2 - 10;
                status.Loaded -= StatusOnLoaded;
            }
        }

        private void ContractCommandBar()
        {
            AppBar.Visibility = Visibility.Collapsed;
            CalculateCellSize();
        }

        private void ExpandCommandBar()
        {
            AppBar.Visibility = Visibility.Visible;
            CalculateCellSize();
        }
    }
}