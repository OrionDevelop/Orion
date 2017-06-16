using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Orion.UWP.ViewModels.Contents;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Orion.UWP.Views.Contents
{
    public sealed partial class ReblogStatusContent : UserControl
    {
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel), typeof(ReblogStatusViewModel), typeof(ReblogStatusContent), new PropertyMetadata(null));

        public static readonly DependencyProperty IsIconRoundedProperty =
            DependencyProperty.Register(nameof(IsIconRounded), typeof(bool), typeof(StatusContent), new PropertyMetadata(false));

        public ReblogStatusViewModel ViewModel
        {
            get => (ReblogStatusViewModel) GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        public bool IsIconRounded
        {
            get => (bool) GetValue(IsIconRoundedProperty);
            set => SetValue(IsIconRoundedProperty, value);
        }

        public ReblogStatusContent()
        {
            InitializeComponent();
        }
    }
}