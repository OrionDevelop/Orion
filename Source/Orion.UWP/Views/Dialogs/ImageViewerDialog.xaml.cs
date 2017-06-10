using System.Linq;

using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Orion.UWP.ViewModels.Dialogs;

// コンテンツ ダイアログの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace Orion.UWP.Views.Dialogs
{
    public sealed partial class ImageViewerDialog : ContentDialog
    {
        private MediaPlayer _mediaPlayer;
        public ImageViewerDialogViewModel ViewModel => DataContext as ImageViewerDialogViewModel;

        public ImageViewerDialog()
        {
            InitializeComponent();
        }

        private void ImageViewerDialog_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (ViewModel.Attachments.Any(w => w.IsVideoMode))
            {
                // Single content
                var video = ViewModel.Attachments.First();
                _mediaPlayer = new MediaPlayer {Source = video.VideoSource};
                var mediaPlayerElement = new MediaPlayerElement
                {
                    AreTransportControlsEnabled = true,
                    AutoPlay = true
                };
                if (!double.IsNaN(video.Height))
                {
                    var height = video.Height;
                    if (height >= MaxHeight - 100)
                        height = MaxHeight - 100;
                    var width = video.Width;
                    if (width >= MaxWidth - 100)
                        width = MaxWidth - 100;
                    mediaPlayerElement.Height = height;
                    mediaPlayerElement.Width = width;
                }
                mediaPlayerElement.SetMediaPlayer(_mediaPlayer);
                RootGrid.Children.Add(mediaPlayerElement);
            }
            else
            {
                var flipView = new FlipView
                {
                    ItemTemplate = RootGrid.Resources["DataTemplate"] as DataTemplate,
                    ItemsSource = ViewModel.Attachments
                };
                RootGrid.Children.Add(flipView);
            }
        }

        private void ImageViewerDialog_OnUnloaded(object sender, RoutedEventArgs e)
        {
            RootGrid.Children.Clear(); // nnn
            ViewModel?.Dispose();
            _mediaPlayer?.Dispose();
        }
    }
}