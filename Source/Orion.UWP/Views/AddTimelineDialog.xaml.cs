﻿using Windows.UI.Xaml.Controls;

using Orion.UWP.ViewModels;

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