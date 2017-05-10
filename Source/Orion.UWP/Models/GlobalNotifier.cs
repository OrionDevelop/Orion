using Orion.UWP.Models.Absorb;

using Prism.Mvvm;

namespace Orion.UWP.Models
{
    public class GlobalNotifier : BindableBase
    {
        #region InReplyStatus

        private Status _inReplyStatuss;

        public Status InReplyStatus
        {
            get => _inReplyStatuss;
            set => SetProperty(ref _inReplyStatuss, value);
        }

        #endregion
    }
}