﻿using System;
using System.Reactive.Linq;

using Orion.Shared.Absorb.Objects;
using Orion.Shared.Absorb.Objects.Events;
using Orion.Shared.Models;

namespace Orion.Shared.Absorb.Extensions
{
    // ReSharper disable once InconsistentNaming
    public static class IObservableStatusBaseEx
    {
        public static IObservable<StatusBase> AsStatus(this IObservable<StatusBase> obj)
        {
            return obj.OfType<Status>();
        }

        public static IObservable<StatusBase> AsMentions(this IObservable<StatusBase> obj, Account account)
        {
            return obj.Where(w =>
            {
                if (w is Status status)
                {
                    if (status.InReplyToUserId.HasValue && status.InReplyToUserId.Value == account.Credential.User.Id)
                        return true;
                    return status.Text.Contains($"@{account.Credential.User.ScreenName}");
                }
                return false;
            });
        }

        public static IObservable<StatusBase> AsMessages(this IObservable<StatusBase> obj)
        {
            return obj.OfType<Message>();
        }

        public static IObservable<StatusBase> AsNotifications(this IObservable<StatusBase> obj)
        {
            return obj.OfType<EventBase>();
        }
    }
}