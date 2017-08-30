using System;

using Orion.Scripting.Parsing;
using Orion.Shared.Absorb.Objects;
using Orion.Shared.Models;
using Orion.UWP.Services.Interfaces;

using Prism.Mvvm;

namespace Orion.UWP.Models
{
    public class GlobalNotifier : BindableBase
    {
        private readonly IConfigurationService _configurationService;

        #region Compiled Filter

        public Delegate CompiledMuteFilter { get; set; }

        #endregion

        public GlobalNotifier(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
            var query = $"WHERE {_configurationService.Load(OrionUwpConstants.Configuration.MuteFilterQueryKey, "true")}";
            CompiledMuteFilter = QueryCompiler.Compile<Status>(query).Delegate;
            IsIconRounded = _configurationService.Load(OrionUwpConstants.Configuration.IsIconRoundedKey, false);
            EnableSensitiveFlag = _configurationService.Load(OrionUwpConstants.Configuration.EnableSensitiveFlagKey, true);
        }

        #region General

        #region IsIconRounded

        private bool _isIconRounded;

        public bool IsIconRounded
        {
            get => _isIconRounded;
            set
            {
                if (SetProperty(ref _isIconRounded, value))
                    _configurationService.Save(OrionUwpConstants.Configuration.IsIconRoundedKey, value);
            }
        }

        #endregion

        #region EnableSensitiveFlag

        private bool _enableSensitiveFlag;

        public bool EnableSensitiveFlag
        {
            get => _enableSensitiveFlag;
            set
            {
                if (SetProperty(ref _enableSensitiveFlag, value))
                    _configurationService.Save(OrionUwpConstants.Configuration.EnableSensitiveFlagKey, value);
            }
        }

        #endregion

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