using System;

using Orion.Shared.Absorb.Objects;
using Orion.Shared.Models;

using Prism.Mvvm;

namespace Orion.UWP.Models
{
    public class GlobalNotifier : BindableBase
    {
        #region Compiled Filter

        public Delegate CompiledMuteFilter { get; set; }

        #endregion

        #region Reply func

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

        #region InReplyTimeline

        private TimelineBase _inReplyTimeline;

        public TimelineBase InReplyTimeline
        {
            get => _inReplyTimeline;
            set => SetProperty(ref _inReplyTimeline, value);
        }

        #endregion

        public void ClearInReply()
        {
            InReplyStatus = null;
            InReplyTimeline = null;
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