using Windows.UI.Xaml.Controls;

using Orion.UWP.ViewModels.Partials;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace Orion.UWP.Views.Partials
{
    /// <summary>
    ///     それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class AccountsPage : Page
    {
        public AccountsPageViewModel ViewModel => DataContext as AccountsPageViewModel;

        public AccountsPage()
        {
            InitializeComponent();
        }
    }
}