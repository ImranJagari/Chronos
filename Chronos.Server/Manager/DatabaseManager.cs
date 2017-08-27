using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronos.Core.Reflection;
using Chronos.ORM;

namespace Chronos.Server.Manager
{
    public abstract class DatabaseManager
    {
        public static Database DefaultDatabase;
        public static Database DefaultAuthDatabase;
        private Database m_database;
        public Database Database
        {
            get
            {
                return this.m_database ?? DatabaseManager.DefaultDatabase;
            }
        }
        public void ChangeDataSource(Database datasource)
        {
            if (this.m_database == null)
            {
                this.m_database = datasource;
            }
            else
            {
                this.m_database = datasource;
                this.TearDown();
                this.Initialize();
            }
        }
        public virtual void Initialize()
        {
        }
        public virtual void TearDown()
        {
        }
    }
    public class DatabaseManager<T> : Singleton<T> where T : class
    {
        private Database m_database;
        public Database Database
        {
            get
            {
                return this.m_database ?? DatabaseManager.DefaultDatabase;
            }
        }
        public Database AuthDatabase
        {
            get
            {
                return this.m_database ?? DatabaseManager.DefaultAuthDatabase;
            }
        }
        public void ChangeDataSource(Database datasource)
        {
            if (this.m_database == null)
            {
                this.m_database = datasource;
            }
            else
            {
                this.m_database = datasource;
                this.TearDown();
                this.Initialize();
            }
        }
        public virtual void Initialize()
        {
        }
        public virtual void TearDown()
        {
        }
    }
}
