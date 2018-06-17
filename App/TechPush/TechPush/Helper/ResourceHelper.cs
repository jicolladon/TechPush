using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace TechPush.Helper
{
    public static class ResourceHelper
    {
        public static T GetResource<T>(string resource)
        {
            var res = (App.Current.Resources.ContainsKey(resource) && App.Current.Resources[resource] is T)
                ? (T)App.Current.Resources[resource] : default(T);
            return res;
        }

        public static byte[] GetEmbeddedResource(Assembly assembly, string resource)
        {
            byte[] buffer = null;

            using (Stream s = assembly.GetManifestResourceStream(resource))
            {
                buffer = new byte[s.Length];
                s.Read(buffer, 0, buffer.Length);
            }

            return buffer;
        }

        public static string GetImage(string resource)
        {
            string result = string.Empty;
            switch (Device.RuntimePlatform)
            {
                case Device.UWP:
                    result = "Resources/" + resource;
                    break;
                default:
                    result = resource;
                    break;
            }

            return result;
        }
    }
}