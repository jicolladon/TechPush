
using Prism;
using Prism.Ioc;
using TechPush.Core.Data;
using TechPush.Core.Services;

namespace TechPush.Droid.Services
{
    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IDatabasePathProvider, DatabasePathProvider>();
            containerRegistry.RegisterSingleton<IDeviceService, DeviceService>();
            containerRegistry.RegisterSingleton<IMessage, MessageAndroid>();

        }
    }
}