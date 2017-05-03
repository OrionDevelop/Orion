using Orion.UWP.Models;
using Orion.UWP.Mvvm;

namespace Orion.UWP.ViewModels.Contents
{
    public class AccountViewModel : ViewModel
    {
        private readonly Account _account;

        public string IconUrl => _account.ClientWrapper.User.Icon;
        public string ScreenName => _account.ClientWrapper.User.ScreenName;

        public AccountViewModel(Account account)
        {
            _account = account;
        }
    }
}