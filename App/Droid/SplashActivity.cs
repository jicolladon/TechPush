using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace TechPush.Droid
{
    [Activity(Icon = "@drawable/LogoNT", MainLauncher = true, NoHistory = true, Theme = "@style/Theme.Splash",ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, LaunchMode = LaunchMode.SingleInstance)]
    public class SplashActivity : Activity
    {
        public bool IsBound = false;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var list = new Dictionary<string, string>();
            if (Intent.Extras != null)
            {
                foreach (var key in Intent.Extras.KeySet())
                {
                    list.Add(key, Intent.Extras.Get(key).ToString());
                }
            }
           
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }

        protected override void OnStart()
        {
            base.OnStart();

        }


    }
}