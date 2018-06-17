using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TechPush.Core.Data;

namespace TechPush.Droid.Services
{
    public class DatabasePathProvider : IDatabasePathProvider
    {

        public bool DatabaseFileExists(string path)
        {
            return System.IO.File.Exists(path);
        }

        public string GetDatabasePath()
        {
            string path = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),
                                                                                    "TechPush.store");
            return path;
        }
    }
}