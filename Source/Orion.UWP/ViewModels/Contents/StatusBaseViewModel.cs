using Orion.Shared.Absorb.Objects;
using Orion.UWP.Mvvm;

namespace Orion.UWP.ViewModels.Contents
{
    public class StatusBaseViewModel : ViewModel
    {
        private readonly long _id;
        private readonly StatusBase _status;

        public string CreatedAt => _status.CreatedAt.ToLocalTime().ToString("g");

        protected StatusBaseViewModel(StatusBase status)
        {
            _status = status;
            _id = _status.Id;
        }

        public StatusBaseViewModel(long id)
        {
            _id = id;
        }

        public override bool Equals(object obj)
        {
            return _id == (obj as StatusBaseViewModel)?._id;
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }
    }
}