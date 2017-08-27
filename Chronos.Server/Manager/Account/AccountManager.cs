using Chronos.Server.Databases.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Server.Manager.Account
{
    public class AccountManager : DatabaseManager<AccountManager>
    {
        public AccountRecord GetAccountByUsername(string username)
        {
            return Database.Fetch<AccountRecord>(string.Format(AccountRecordRelator.FetchQueryWithUsername, username)).FirstOrDefault();
        }
    }
}
