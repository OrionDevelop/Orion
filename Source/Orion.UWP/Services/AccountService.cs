using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Orion.UWP.Models;
using Orion.UWP.Services.Interfaces;

namespace Orion.UWP.Services
{
    internal class AccountService : IAccountService
    {
        private readonly List<Account> _accounts;

        public AccountService()
        {
            _accounts = new List<Account>();
        }

        public ReadOnlyCollection<Account> Accounts => _accounts.AsReadOnly();

        public Task CleanAsync()
        {
            _accounts.Clear();
            return Task.CompletedTask;
        }

        public Task<bool> AuthorizeAsync(Provider provider)
        {
            throw new NotImplementedException();
        }
    }
}