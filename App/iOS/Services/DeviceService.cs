using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Foundation;
using TechPush.Core.Enum;
using TechPush.Core.Services;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace TechPush.iOS.Services
{
    public class DeviceService : IDeviceService
    {
        public void CloseApp()
        {
            Thread.CurrentThread.Abort();
        }

        public DeviceTypeEnum GetDeviceType()
        {
            return DeviceTypeEnum.IOS;
        }

        public string GetToken()
        {
            return AppDelegate.PushToken;
        }

        public async Task Share(string subject, string message, byte[] image)
        {
            var handler = new ImageLoaderSourceHandler();
            var data = NSData.FromArray(image);
            var uiimage = UIImage.LoadFromData(data);
            var img = NSObject.FromObject(uiimage);
            var mess = NSObject.FromObject(message);
            var activityItems = new[] { mess, img };
            var activityController = new UIActivityViewController(activityItems, null);
            var topController = UIApplication.SharedApplication.KeyWindow.RootViewController;

            while (topController.PresentedViewController != null)
            {
                topController = topController.PresentedViewController;
            }

            topController.PresentViewController(activityController, true, () => { });
        }
    }
}