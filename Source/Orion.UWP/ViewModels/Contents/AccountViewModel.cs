﻿using Orion.Shared.Models;
using Orion.UWP.Mvvm;

namespace Orion.UWP.ViewModels.Contents
{
    public class AccountViewModel : ViewModel
    {
        public Account Account { get; }

        public string IconUrl => Account.Credential.User.IconUrl;
        public string ScreenName => Account.Credential.User.ScreenNameWithHost;

        public AccountViewModel(Account account)
        {
            Account = account;
        }
    }
}