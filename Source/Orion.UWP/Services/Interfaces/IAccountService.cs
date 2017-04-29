using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Orion.UWP.Models;

namespace Orion.UWP.Services.Interfaces
{
    public interface IAccountService
    {
        /// <summary>
        ///     Account list
        /// </summary>
        ReadOnlyCollection<Account> Accounts { get; }

        /// <summary>
        ///     Remove all account
        /// </summary>
        /// <returns></returns>
        Task CleanAsync();

        /// <summary>
        ///     Authorize
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        Task<bool> AuthorizeAsync(Provider provider);
    }
}