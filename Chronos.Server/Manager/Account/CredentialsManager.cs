using Chronos.Core.Extensions;
using Chronos.Core.Reflection;
using Chronos.Server.Databases.Account;
using Chronos.Server.Game.Account;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Chronos.Server.Manager.Account
{
    public class CredentialsManager : Singleton<CredentialsManager>
    {
        public bool CheckAccountValidity(out GameAccount account, string username, string password)
        {
            AccountRecord record = AccountManager.Instance.GetAccountByUsername(username);
            if(record == null || record.Password != password)
            {
                account = null;
                return false;
            }
            else
            {
                account = new GameAccount(record);
                return true;
            }
        }
    }
}