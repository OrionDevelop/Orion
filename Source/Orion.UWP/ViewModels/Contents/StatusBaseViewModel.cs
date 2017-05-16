using Orion.Shared.Absorb.Objects;
using Orion.UWP.Mvvm;

namespace Orion.UWP.ViewModels.Contents
{
    public class StatusBaseViewModel : ViewModel
    {
        private readonly StatusBase _status;

        public string CreatedAt => _status.CreatedAt.ToLocalTime().ToString("g");

        protected StatusBaseViewModel(StatusBase status)
        {
            _status = status;
        }
    }
}