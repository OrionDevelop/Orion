using System.Collections.Generic;

using Orion.UWP.Mvvm;
using Orion.UWP.Services.Interfaces;
using Orion.UWP.Views;

using Prism.Windows.Navigation;

namespace Orion.UWP.ViewModels
{
    public class MainPageViewModel : ViewModel
    {
        private readonly IDialogService _dialogService;

        public MainPageViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);

            _dialogService.ShowDialogAsync(typeof(AuthorizationDialog));
        }
    }
}