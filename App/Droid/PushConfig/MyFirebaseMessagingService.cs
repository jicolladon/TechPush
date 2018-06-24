using System;
using Android.App;
using Android.Content;
using Android.Media;
using Android.Support.V7.App;
using Firebase.Messaging;
using TechPush.Helper;
using Xamarin.Forms;

namespace TechPush.Droid.PushConfig
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        public override void OnMessageReceived(RemoteMessage message)
        {
            //message.Data.TryGetValue("idUr", out var idUr);
            var msg = message.GetNotification();
            CreateNotification(ApplicationContext, msg?.Title, msg?.Body);
        }

        //Recepcion y creación de la notificación

        private void CreateNotification(Context context,string title, string desc)
        {
            MessagingCenter.Send(CommonHelper.UpdateListRequests, CommonHelper.UpdateListRequests);

            var notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;
            var uiIntent = new Intent(this, typeof(SplashActivity));
            NotificationCompat.Builder builder = new NotificationCompat.Builder(this);

            var notification = builder.SetContentIntent(PendingIntent.GetActivity(this, 0, uiIntent, 0))
                .SetSmallIcon(Resource.Drawable.LogoNT)
                .SetContentText(desc)
                .SetContentTitle(title)
                .SetDefaults((int)(NotificationDefaults.Sound | NotificationDefaults.Vibrate))
                .SetSound(RingtoneManager.GetActualDefaultRingtoneUri(context, RingtoneType.Notification))
                .SetAutoCancel(true).Build();

            notificationManager?.Notify(1, notification);
        }
    }
}