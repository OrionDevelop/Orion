using Orion.Shared.Emoji;
using Orion.Shared.Models;
using Orion.UWP.Mvvm;

namespace Orion.UWP.ViewModels.Contents
{
    public class AccountViewModel : ViewModel
    {
        public Account Account { get; }

        public string FaviconUrl => $"http://www.google.com/s2/favicons?domain={Account.Provider.Host}";
        public string IconUrl => Account.Credential.User.IconUrl;
        public string Username => EmojiConverter.Convert(Account.Credential.User.Name);
        public string ScreenName => Account.Credential.User.ScreenNameWithHost;

        public AccountViewModel(Account account)
        {
            Account = account;
        }
    }
}