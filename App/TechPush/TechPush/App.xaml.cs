using Prism;
using Prism.Ioc;
using Prism.Unity;
using System;
using TechPush.Bussiness;
using TechPush.Bussiness.Mapper;
using TechPush.Bussiness.RemoteServices;
using TechPush.Core;
using TechPush.Core.Data;
using TechPush.Core.Mapper;
using TechPush.Core.RemoteServices;
using TechPush.Core.Repositories;
using TechPush.Core.Services;
using TechPush.Data;
using TechPush.Data.Repositories;
using TechPush.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace TechPush
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }
        //public App(IPlatformInitializer initializer = null) : base(initializer) { }
        public static IContainerProvider AppContainer;
        public static System.Globalization.CultureInfo Culture;

        protected async override void OnInitialized()
        {
            SetInitialConfiguration();
            InitializeComponent();
            await NavigationService.NavigateAsync("NavigationPage/MainPage");
            AppContainer = Container;
        }

        private void SetInitialConfiguration()
        {
            //var configuration = Container.Resolve<IConfigurationService>();
            //var cultureCode = configuration.GetCultureCode();
            //if (string.IsNullOrEmpty(cultureCode))
            //{
            //    var localizationService = Container.Resolve<ILocale>() as ILocale;
            //    cultureCode = localizationService.GetCurrentCultureInfo().Name;
            //    configuration.SaveCulture(cultureCode);
            //}
            //Culture = new System.Globalization.CultureInfo(cultureCode);
            //AppResources.Culture = Culture;
        }


        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<AdminPage>();
            containerRegistry.RegisterForNavigation<RegisterPage>();
            containerRegistry.RegisterForNavigation<PrincipalPage>();


            containerRegistry.RegisterSingleton<IConnectionWrapper, ConnectionWrapper>();
            containerRegistry.RegisterSingleton<IUserRepository, UserRepository>();
            containerRegistry.RegisterSingleton<IUserService, UserService>();
            containerRegistry.RegisterSingleton<IPushService, PushService>();
            containerRegistry.RegisterSingleton<IBaseClientService, BaseClientService>();
            containerRegistry.RegisterSingleton<IAppService, AppService>();
            containerRegistry.RegisterSingleton<IRestClientService, RestClientService>();
            containerRegistry.RegisterSingleton<IEntityMapper, EntityMapper>();

            //containerRegistry.Register<ISettingsFactory<AquarisApp.Settings.ApplicationSettings>, ApplicationSettingsFactory>();
            //containerRegistry.Register<IConnectionWrapper, ConnectionWrapper>();
            //containerRegistry.Register<IUserRepository, UserRepository>();
            //containerRegistry.Register<IConfigurationRepository, ConfigurationRepository>();
            //containerRegistry.Register<IConfigurationService, ConfigurationService>();

            //containerRegistry.Register<ILocalizationService, LocalizationService>();
        }
    }
}
