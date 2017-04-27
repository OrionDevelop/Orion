using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

using Orion.UWP.Models;
using Orion.UWP.Mvvm;

using Reactive.Bindings;

namespace Orion.UWP.ViewModels
{
    public class AuthorizationDialogViewModel : ViewModel
    {
        public ReadOnlyCollection<Provider> Providers { get; } = Constants.Providers;

        public ReactiveProperty<Provider> SelectedProvider { get; }
        public ReactiveProperty<string> Host { get; }
        public ReactiveProperty<string> ConsumerKey { get; }
        public ReactiveProperty<string> ConsumerSecret { get; }
        public ReactiveCommand AuthorizeCommand { get; }

        public AuthorizationDialogViewModel()
        {
            SelectedProvider = new ReactiveProperty<Provider>();
            SelectedProvider.Where(w => w != null).Subscribe(w =>
            {
                HasHost = w.RequireHost;
                HasApiKeys = w.RequireApiKeys;
                Rejudge(w);
            }).AddTo(this);
            Host = new ReactiveProperty<string>();
            Host.Subscribe(w => Rejudge(host: w)).AddTo(this);
            ConsumerKey = new ReactiveProperty<string>();
            ConsumerKey.Subscribe(w => Rejudge(consumerKey: w)).AddTo(this);
            ConsumerSecret = new ReactiveProperty<string>();
            ConsumerSecret.Subscribe(w => Rejudge(consumerSecret: w)).AddTo(this);
            AuthorizeCommand = new ReactiveCommand();

            void Rejudge(Provider provider = null, string host = null, string consumerKey = null, string consumerSecret = null)
            {
                provider = provider ?? SelectedProvider?.Value;
                host = host ?? Host?.Value;
                consumerKey = consumerKey ?? ConsumerKey?.Value;
                consumerSecret = consumerSecret ?? ConsumerSecret?.Value;

                if (provider == null)
                {
                    CanExecuteAuthorize = false;
                    return;
                }
                if (!provider.RequireApiKeys && !provider.RequireHost)
                {
                    CanExecuteAuthorize = true;
                }
                else
                {
                    var b = !(provider.RequireHost && string.IsNullOrWhiteSpace(host));
                    if (provider.RequireApiKeys)
                        b = !(string.IsNullOrWhiteSpace(consumerKey) || string.IsNullOrWhiteSpace(consumerSecret));
                    CanExecuteAuthorize = b;
                }
            }
        }

        #region HasHost

        private bool _hasHost;

        public bool HasHost
        {
            get => _hasHost;
            set => SetProperty(ref _hasHost, value);
        }

        #endregion

        #region HasApiKeys

        private bool _hasApiKeys;

        public bool HasApiKeys
        {
            get => _hasApiKeys;
            set => SetProperty(ref _hasApiKeys, value);
        }

        #endregion

        #region CanExecuteAuthorize

        private bool _canExecuteAuthorize;

        public bool CanExecuteAuthorize
        {
            get => _canExecuteAuthorize;
            set => SetProperty(ref _canExecuteAuthorize, value);
        }

        #endregion
    }
}