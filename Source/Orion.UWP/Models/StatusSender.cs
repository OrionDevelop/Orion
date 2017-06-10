using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Orion.Shared.Models;

using Prism.Mvvm;

namespace Orion.UWP.Models
{
    internal class StatusSender : BindableBase
    {
        private readonly ObservableCollection<string> _errorMessages;
        public ReadOnlyObservableCollection<string> ErrorMessages => new ReadOnlyObservableCollection<string>(_errorMessages);

        public StatusSender()
        {
            _errorMessages = new ObservableCollection<string>();
        }

        /// <summary>
        ///     Send status.
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public async Task SendAsync(List<Account> accounts, string body)
        {
            _errorMessages.Clear();
            foreach (var account in accounts)
                try
                {
                    await account.ClientWrapper.UpdateAsync(body);
                }
                catch (Exception e)
                {
                    _errorMessages.Add($"Status from @{account.Credential.User.ScreenNameWithHost} failed: {e.Message}");
                }
            HasErrorMessages = _errorMessages.Count > 0;
        }

        /// <summary>
        ///     Send status with in reply.
        /// </summary>
        /// <param name="account"></param>
        /// <param name="body"></param>
        /// <param name="inReplyToStatusId"></param>
        /// <returns></returns>
        public async Task SendReplyAsync(Account account, string body, long inReplyToStatusId)
        {
            _errorMessages.Clear();
            try
            {
                await account.ClientWrapper.UpdateAsync(body, inReplyToStatusId);
            }
            catch (Exception e)
            {
                _errorMessages.Add($"Status from @{account.Credential.User.ScreenNameWithHost} failed: {e.Message}");
            }
            HasErrorMessages = _errorMessages.Count > 0;
        }

        #region HasErrorMessages

        private bool _hasErrorMessages;

        public bool HasErrorMessages
        {
            get => _hasErrorMessages;
            set => SetProperty(ref _hasErrorMessages, value);
        }

        #endregion
    }
}