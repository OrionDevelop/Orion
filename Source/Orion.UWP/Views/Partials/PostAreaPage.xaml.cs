using Windows.UI.Xaml.Controls;

using Orion.UWP.ViewModels.Contents;
using Orion.UWP.ViewModels.Partials;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Orion.UWP.Views.Partials
{
    public sealed partial class PostAreaPage : Page
    {
        public PostAreaPageViewModel ViewModel => DataContext as PostAreaPageViewModel;

        public PostAreaPage()
        {
            InitializeComponent();
        }

        private void Accounts_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (AccountViewModel item in e.AddedItems)
                ViewModel.SelectedAccounts.Add(item);
            foreach (AccountViewModel item in e.RemovedItems)
                ViewModel.SelectedAccounts.Remove(item);
        }
    }
}