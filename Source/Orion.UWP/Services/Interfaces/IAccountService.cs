using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Orion.UWP.Models;

namespace Orion.UWP.Services.Interfaces
{
    public interface IAccountService
    {
        ReadOnlyObservableCollection<Account> Accounts { get; }

        Task ClearAsync();

        Task RegisterAsync(Account account);

        Task RestoreAsync();
    }
}