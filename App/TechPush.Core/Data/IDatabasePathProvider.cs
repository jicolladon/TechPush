using System;
using System.Collections.Generic;
using System.Text;

namespace TechPush.Core.Data
{
    public interface IDatabasePathProvider
    {
        string GetDatabasePath();
        bool DatabaseFileExists(string path);
    }
}
