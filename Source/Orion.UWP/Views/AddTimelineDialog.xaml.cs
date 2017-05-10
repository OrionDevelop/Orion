using System;
using System.Reactive.Linq;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Orion.UWP.ViewModels;

using Reactive.Bindings.Extensions;

// コンテンツ ダイアログの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace Orion.UWP.Views
{
    public sealed partial class AddTimelineDialog : ContentDialog
    {
        public AddTimelineDialogViewModel ViewModel => DataContext as AddTimelineDialogViewModel;

        public AddTimelineDialog()
        {
            InitializeComponent();
        }
    }
}