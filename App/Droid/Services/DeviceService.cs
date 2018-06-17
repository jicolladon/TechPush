using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Iid;
using TechPush.Core.Enum;
using TechPush.Core.Services;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace TechPush.Droid.Services
{
    public class DeviceService : IDeviceService
    {
        public DeviceTypeEnum GetDeviceType()
        {
            return DeviceTypeEnum.Android;
        }

        public string GetToken()
        {
            return FirebaseInstanceId.Instance.Token;
        }

        public void CloseApp()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());

        }

        public async Task Share(string subject, string message, byte[] image)
        {
            Intent intent = new Intent(Intent.ActionSend);
            intent.PutExtra(Intent.ExtraSubject, subject);
            intent.PutExtra(Intent.ExtraText, message);
            intent.SetType("image/png");

            if (image != null && image.Length > 0)
            {
                Bitmap bitmap = BitmapFactory.DecodeByteArray(image, 0, image.Length);

                Java.IO.File path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads + Java.IO.File.Separator + "shareImage.png");

                using (System.IO.FileStream os = new System.IO.FileStream(path.AbsolutePath, System.IO.FileMode.Create))
                {
                    bitmap.Compress(Bitmap.CompressFormat.Png, 100, os);
                }
                intent.PutExtra(Intent.ExtraStream, Android.Net.Uri.FromFile(path));

            }
            Android.App.Application.Context.StartActivity(Intent.CreateChooser(intent, "Share Image"));
        }
    }
}