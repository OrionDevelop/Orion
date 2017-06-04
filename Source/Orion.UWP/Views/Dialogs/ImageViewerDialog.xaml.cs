using Windows.UI.Xaml.Controls;

using Orion.UWP.ViewModels.Dialogs;

// コンテンツ ダイアログの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace Orion.UWP.Views.Dialogs
{
    public sealed partial class ImageViewerDialog : ContentDialog
    {
        public ImageViewerDialogViewModel ViewModel => DataContext as ImageViewerDialogViewModel;

        public ImageViewerDialog()
        {
            InitializeComponent();
        }
    }
}