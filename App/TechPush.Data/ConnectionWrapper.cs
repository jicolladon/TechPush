using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using TechPush.Core.Data;
using TechPush.Core.Domain;

namespace TechPush.Data
{
    public class ConnectionWrapper : IConnectionWrapper
    {
        private readonly IDatabasePathProvider _databasePathProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="databasePathProvider">Provider to obtain the database path</param>
        public ConnectionWrapper(IDatabasePathProvider databasePathProvider)
        {
            _databasePathProvider = databasePathProvider;
        }

        public object GetConnection()
        {
            string path = _databasePathProvider.GetDatabasePath();

            if (!_databasePathProvider.DatabaseFileExists(path))
            {
                CreateDataBase(path);
            }

            return CreateNewConnection(path);
        }

        protected virtual void CreateDataBase(string path)
        {
            var conn = CreateNewConnection(path);
            conn.CreateTable<UserEntity>();
        }

        protected virtual SQLiteConnection CreateNewConnection(string path)
        {
            return new SQLiteConnection(path);

        }

    }
}