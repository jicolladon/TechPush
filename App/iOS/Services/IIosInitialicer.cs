using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Prism;
using Prism.Ioc;
using TechPush.Core.Data;
using TechPush.Core.Services;
using UIKit;

namespace TechPush.iOS.Services
{
    public class IIosInitialicer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IDatabasePathProvider, DatabasePathProvider>();
            containerRegistry.RegisterSingleton<IDeviceService, DeviceService>();
            containerRegistry.RegisterSingleton<IMessage, MessageIOS>();

        }
    }
}