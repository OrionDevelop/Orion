using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;

using Orion.Scripting.Parsing;
using Orion.Shared;
using Orion.Shared.Absorb.Objects;
using Orion.UWP.Mvvm;
using Orion.UWP.Services.Interfaces;
using Orion.UWP.ViewModels.Contents;

using Reactive.Bindings;

namespace Orion.UWP.ViewModels.Dialogs
{
    public class AddTimelineDialogViewModel : ViewModel
    {
        public ReadOnlyObservableCollection<AccountViewModel> Accounts { get; }
        public ObservableCollection<TimelinePresetViewModel> Timelines { get; }
        public ReactiveProperty<AccountViewModel> SelectedAccount { get; }
        public ReactiveProperty<TimelinePresetViewModel> SelectedTimeline { get; }
        public ReactiveProperty<string> Name { get; }
        public ReactiveProperty<string> Query { get; }
        public ReactiveProperty<string> ErrorMessage { get; }
        public AsyncReactiveCommand OkCommand { get; }

        public AddTimelineDialogViewModel(IAccountService accountService, ITimelineService timelineService)
        {
            Accounts = accountService.Accounts.ToReadOnlyReactiveCollection(w => new AccountViewModel(w));
            IsEnableOkCommand = false;
            IsEditable = false;
            Timelines = new ObservableCollection<TimelinePresetViewModel>();
            SelectedAccount = new ReactiveProperty<AccountViewModel>();
            SelectedAccount.Where(w => w != null).Subscribe(w =>
            {
                UpdateCanExecuteOkCommand();
                Timelines.Clear();
                foreach (var timelineType in SharedConstants.TimelinePresets.Where(v => v.ProviderType == w.Account.Provider.ProviderType))
                    Timelines.Add(new TimelinePresetViewModel(timelineType));
            }).AddTo(this);
            SelectedTimeline = new ReactiveProperty<TimelinePresetViewModel>();
            SelectedTimeline.Subscribe(w =>
            {
                UpdateCanExecuteOkCommand();
                IsEditable = w?.IsEditable ?? false;
            }).AddTo(this);
            Name = new ReactiveProperty<string>();
            Name.Subscribe(_ => UpdateCanExecuteOkCommand()).AddTo(this);
            Query = new ReactiveProperty<string>();
            Query.Throttle(TimeSpan.FromMilliseconds(500)).Select(w =>
            {
                try
                {
                    QueryCompiler.Compile<Status>(w);
                    return null;
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }).Subscribe(w => ErrorMessage.Value = w).AddTo(this);
            ErrorMessage = new ReactiveProperty<string>();
            OkCommand = new AsyncReactiveCommand();
            OkCommand.Subscribe(async () =>
            {
                var account = SelectedAccount.Value.Account;
                await timelineService.AddAsync(SelectedTimeline.Value.TimelinePreset.CreateTimeline(account, Name.Value, Query.Value));
            });
        }

        private void UpdateCanExecuteOkCommand()
        {
            var b = SelectedAccount.Value != null && SelectedTimeline.Value != null;
            if (SelectedTimeline.Value != null && SelectedTimeline.Value.IsEditable)
                b = b && !string.IsNullOrWhiteSpace(Name.Value) && string.IsNullOrWhiteSpace(ErrorMessage.Value);
            IsEnableOkCommand = b;
        }

        #region IsEditable

        private bool _isEditable;

        public bool IsEditable
        {
            get => _isEditable;
            set => SetProperty(ref _isEditable, value);
        }

        #endregion

        #region IsEnableOkCommand

        private bool _isEnableOkCommand;

        public bool IsEnableOkCommand
        {
            get => _isEnableOkCommand;
            set => SetProperty(ref _isEnableOkCommand, value);
        }

        #endregion
    }
}