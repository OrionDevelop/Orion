using Orion.Shared.Absorb.Objects;

using Prism.Mvvm;

namespace Orion.UWP.Models
{
    public class GlobalNotifier : BindableBase
    {
        #region InReplyStatus

        private Status _inReplyStatuss;

        /// <summary>
        ///     In reply to status
        ///     Subscribed on: PostAreaPageViewModel
        /// </summary>
        public Status InReplyStatus
        {
            get => _inReplyStatuss;
            set => SetProperty(ref _inReplyStatuss, value);
        }

        #endregion

        #region TimelineAreaHorizontalOffset

        private double _timelineAreaHorizontalOffset;

        /// <summary>
        ///     Horizontal offset of timeline area's ScrollViewer.
        ///     Subscribed on: MainPageViewModel
        /// </summary>
        public double TimelineAreaHorizontalOffset
        {
            get => _timelineAreaHorizontalOffset;
            set => SetProperty(ref _timelineAreaHorizontalOffset, value);
        }

        #endregion
    }
}