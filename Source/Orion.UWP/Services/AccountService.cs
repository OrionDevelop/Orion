using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Windows.Security.Credentials;

using Newtonsoft.Json;

using Orion.Shared.Models;
using Orion.UWP.Services.Interfaces;

namespace Orion.UWP.Services
{
    internal class AccountService : IAccountService
    {
        private readonly ObservableCollection<Account> _accounts;

        public AccountService()
        {
            _accounts = new ObservableCollection<Account>();
        }

        public ReadOnlyObservableCollection<Account> Accounts => new ReadOnlyObservableCollection<Account>(_accounts);

        public Task ClearAsync()
        {
            _accounts.Clear();
            var vault = new PasswordVault();
            foreach (var credential in vault.RetrieveAll())
                vault.Remove(credential);
            _accounts.Clear();
            return Task.CompletedTask;
        }

        public async Task RestoreAsync()
        {
            try
            {
                var vault = new PasswordVault();
                foreach (var credential in vault.RetrieveAll())
                {
                    credential.RetrievePassword();
                    var account = JsonConvert.DeserializeObject<Account>(credential.Password);
                    if (await account.RestoreAsync())
                        await RegisterAsync(account, false);
                    else
                        vault.Remove(credential);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public Task RegisterAsync(Account account)
        {
            return RegisterAsync(account, true);
        }

        public Task DeleteAsync(Account account)
        {
            try
            {
                var vault = new PasswordVault();
                foreach (var credential in vault.RetrieveAll())
                {
                    if (credential.UserName != account.Id)
                        continue;
                    credential.RetrievePassword();
                    vault.Remove(credential);
                    _accounts.Remove(_accounts.Single(w => w.Id == account.Id));
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return Task.CompletedTask;
        }

        private Task RegisterAsync(Account account, bool isInint)
        {
            try
            {
                if (_accounts.Count == 0 && isInint)
                    account.IsMarkAsDefault = true;
                var vault = new PasswordVault();
                vault.Add(new PasswordCredential("Orion.Accounts", account.Id, JsonConvert.SerializeObject(account)));
                if (_accounts.All(w => w.OrderIndex <= account.OrderIndex))
                {
                    _accounts.Add(account);
                }
                else
                {
                    var index = _accounts.Select((w, i) => (w, i)).First(w => w.Item1.OrderIndex > account.OrderIndex).Item2;
                    _accounts.Insert(index, account);
                }
            }
            catch
            {
                // ignored
            }
            return Task.CompletedTask;
        }
    }
}