using System.Collections.Generic;

using Orion.UWP.Mvvm;
using Orion.UWP.Services.Interfaces;
using Orion.UWP.Views;

using Prism.Windows.Navigation;

namespace Orion.UWP.ViewModels
{
    public class MainPageViewModel : ViewModel
    {
        private readonly IAccountService _accountService;
        private readonly IDialogService _dialogService;

        public MainPageViewModel(IAccountService accountService, IDialogService dialogService)
        {
            _accountService = accountService;
            _dialogService = dialogService;
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);

            if (_accountService.Accounts.Count == 0)
                _dialogService.ShowDialogAsync(typeof(AuthorizationDialog));
        }
    }
}