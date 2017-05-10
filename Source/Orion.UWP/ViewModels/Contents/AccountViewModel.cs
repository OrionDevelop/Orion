using Orion.UWP.Models;
using Orion.UWP.Mvvm;

namespace Orion.UWP.ViewModels.Contents
{
    public class AccountViewModel : ViewModel
    {
        public Account Account { get; }

        public string IconUrl => Account.ClientWrapper.User.Icon;
        public string ScreenName => Account.ClientWrapper.User.ScreenName;

        public AccountViewModel(Account account)
        {
            Account = account;
        }
    }
}