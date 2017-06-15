using System;
using System.Reactive.Linq;

using Orion.Scripting.Parsing;
using Orion.Shared.Absorb.Objects;
using Orion.UWP.Models;
using Orion.UWP.Mvvm;
using Orion.UWP.Services.Interfaces;

using Reactive.Bindings;

namespace Orion.UWP.ViewModels.Dialogs
{
    public class SettingsDialogViewModel : ViewModel
    {
        public ReactiveProperty<bool> IsIconRounded { get; }
        public ReactiveProperty<string> Query { get; }
        public ReactiveProperty<string> ErrorMessage { get; }

        public SettingsDialogViewModel(GlobalNotifier globalNotifier, IConfigurationService configurationService)
        {
            IsIconRounded = new ReactiveProperty<bool>(globalNotifier.IsIconRounded);
            IsIconRounded.Subscribe(w =>
            {
                globalNotifier.IsIconRounded = w;
                configurationService.Save(OrionUwpConstants.Configuration.IsIconRoundedKey, IsIconRounded.Value);
            }).AddTo(this);
            Query = new ReactiveProperty<string>(configurationService.Load(OrionUwpConstants.Configuration.MuteFilterQueryKey, "true"));
            Query.Throttle(TimeSpan.FromMilliseconds(500)).Where(w => !string.IsNullOrWhiteSpace(w)).Select(w =>
            {
                try
                {
                    globalNotifier.CompiledMuteFilter = QueryCompiler.Compile<Status>($"WHERE {w}").Delegate;
                    configurationService.Save(OrionUwpConstants.Configuration.MuteFilterQueryKey, Query.Value);
                    return null;
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }).Subscribe(w => ErrorMessage.Value = w).AddTo(this);
            ErrorMessage = new ReactiveProperty<string>();
        }
    }
}