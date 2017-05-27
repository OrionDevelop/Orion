using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Orion.UWP.ViewModels.Contents;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Orion.UWP.Views.Contents
{
    public sealed partial class NotificationContent : UserControl
    {
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel), typeof(NotificationViewModel), typeof(NotificationContent), new PropertyMetadata(null));

        public NotificationViewModel ViewModel
        {
            get => (NotificationViewModel) GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        public NotificationContent()
        {
            InitializeComponent();
        }
    }
}