using System.Diagnostics;
using System.Threading.Tasks;

using Windows.ApplicationModel.Activation;

using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.HockeyApp;
using Microsoft.Practices.Unity;

using Orion.UWP.Models;
using Orion.UWP.Services;
using Orion.UWP.Services.Interfaces;

using Prism.Unity.Windows;

namespace Orion.UWP
{
    /// <summary>
    ///     既定の Application クラスを補完するアプリケーション固有の動作を提供します。
    /// </summary>
    public sealed partial class App : PrismUnityApplication
    {
        /// <summary>
        ///     単一アプリケーション オブジェクトを初期化します。これは、実行される作成したコードの
        ///     最初の行であるため、main() または WinMain() と論理的に等価です。
        /// </summary>
        public App()
        {
            HockeyClient.Current.Configure("a8287f6a29c64f408d09605296e192d8").SetExceptionDescriptionLoader(w => w.ToString());
            InitializeComponent();
            UnhandledException += (sender, e) =>
            {
                if (Debugger.IsAttached)
                    Debugger.Break();
            };
        }

        protected override async Task OnInitializeAsync(IActivatedEventArgs args)
        {
            // Prism
            Container.RegisterInstance(NavigationService);
            Container.RegisterInstance(SessionStateService);
            // Container.RegisterInstance<IResourceLoader>(new ResourceLoaderAdapter(new ResourceLoader()));

            // Internal
            var accountService = new AccountService();
            // await accountService.ClearAsync();
            await accountService.RestoreAsync();

            Container.RegisterInstance<IAccountService>(accountService, new ContainerControlledLifetimeManager());
            Container.RegisterType<IConfigurationService, ConfigurationService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IDialogService, DialogService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IOrionNavigationService, OrionNavigationService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ITimelineService, TimelineService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<GlobalNotifier>(new ContainerControlledLifetimeManager());

            await base.OnInitializeAsync(args);
        }

        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            MobileCenter.Start("da64efe6-0b35-4c47-bd6d-e5ef603162bf", typeof(Analytics));
            NavigationService.Navigate("Main", null);
            return Task.CompletedTask;
        }
    }
}