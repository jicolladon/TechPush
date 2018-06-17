using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using TechPush.Core.Data;
using UIKit;

namespace TechPush.iOS.Services
{
    public class DatabasePathProvider : IDatabasePathProvider
    {
        public bool DatabaseFileExists(string path)
        {
            return System.IO.File.Exists(path);

        }


        public string GetDatabasePath()
        {
            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "TechPush.store");
            return path;
        }
    }
}