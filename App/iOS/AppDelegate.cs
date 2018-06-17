using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using TechPush.Helper;
using TechPush.iOS.Services;
using UIKit;
using Xamarin.Forms;

namespace TechPush.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {

        public static string PushToken;
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            RegisterPush();

            LoadApplication(new App(new IIosInitialicer()));

            return base.FinishedLaunching(app, options);
        }

        private void RegisterPush()
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                var pushSettings = UIUserNotificationSettings.GetSettingsForTypes(
                    UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
                    new NSSet());

                UIApplication.SharedApplication.RegisterUserNotificationSettings(pushSettings);
                UIApplication.SharedApplication.RegisterForRemoteNotifications();
            }
            else
            {
                UIRemoteNotificationType notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
                UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationTypes);
            }
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            // Get current device token
            var token = deviceToken.Description;
            if (!string.IsNullOrWhiteSpace(token))
            {
                token = token.Replace("<", "").Replace(">", "").Replace(" ", "");
                PushToken = token;
            }
        }

        public override void OnActivated(UIApplication uiApplication)
        {
            UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
        }


        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo,
            Action<UIBackgroundFetchResult> completionHandler)
        {
            MessagingCenter.Send(CommonHelper.UpdateListRequests, CommonHelper.UpdateListRequests);
            ProcessNotification(userInfo, true);
        }

        private void ProcessNotification(NSDictionary options, bool showAlert)
        {
            if (null != options && options.ContainsKey(new NSString("aps")))
            {
                NSDictionary aps = options.ObjectForKey(new NSString("aps")) as NSDictionary;

                string title = string.Empty;
                string body = string.Empty;

                if (aps != null && aps.ContainsKey(new NSString("alert")))
                {
                    title = (aps[new NSString("alert")] as NSString)?.ToString();
                }

                if (aps != null && aps.ContainsKey(new NSString("body")))
                {
                    body = aps[new NSString("body")].ToString();
                }


                if (showAlert)
                {
                    if (!string.IsNullOrEmpty(body) || !string.IsNullOrEmpty(title))
                    {
                        var avAlert = new UIAlertView(title, body, null, "Ok", null);
                        avAlert.Show();
                    }
                }
            }
        }
    }
}
