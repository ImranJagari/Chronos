using System;

namespace Chronos.ORM
{
    [Serializable]
    public class DatabaseConfiguration
    {
        public DatabaseConfiguration()
        {
            
        }

        public DatabaseConfiguration(string host, string user, string password, string dbName, string providerName)
        {
            Host = host;
            User = user;
            Password = password;
            DbName = dbName;
            ProviderName = providerName;
        }

        public string User
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public string DbName
        {
            get;
            set;
        }

        public string Host
        {
            get;
            set;
        }

        public string ProviderName
        {
            get;
            set;
        }

        public string GetConnectionString()
        {
            return string.Format("database={0};uid={1};password={2};server={3};Convert Zero Datetime=true;Allow Zero Datetime=true", DbName, User, Password, Host);
        }
    }
}