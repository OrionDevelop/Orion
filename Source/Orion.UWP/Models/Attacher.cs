﻿using Orion.Shared.Absorb.Objects;
using Orion.Shared.Absorb.Objects.Events;
using Orion.UWP.ViewModels.Contents;

namespace Orion.UWP.Models
{
    public static class Attacher
    {
        public static StatusBaseViewModel Attach(GlobalNotifier globalNotifier, StatusBase @base)
        {
            if (@base is Status status)
                if (status.RebloggedStatus == null)
                    return new StatusViewModel(globalNotifier, status);
                else
                    return new ReblogStatusViewModel(globalNotifier, status);
            if (@base is EventBase @event)
                return new NotificationViewModel(globalNotifier, @event);
            return null;
        }
    }
}