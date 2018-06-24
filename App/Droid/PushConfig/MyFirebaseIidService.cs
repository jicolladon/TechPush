using Android.App;
using Android.Util;
using Firebase.Iid;

namespace TechPush.Droid.PushConfig
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class MyFirebaseIidService : FirebaseInstanceIdService
    {
        //Obtencion del Token
        const string Tag = "MyFirebaseIIDService";
        public override void OnTokenRefresh()
        {
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            Log.Debug(Tag, "Refreshed token: " + refreshedToken);
        }

     
    }
}