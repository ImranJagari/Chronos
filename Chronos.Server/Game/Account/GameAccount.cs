using Chronos.Server.Databases.Account;
using Chronos.Server.Game.Actors.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Server.Game.Account
{
    public class GameAccount
    {
        public AccountRecord Record { get; }

        public GameAccount(AccountRecord record)
        {
            Record = record;
            Characters = new List<Character>();
        }
        public List<Character> Characters;
        public string HDSN
        {
            get
            {
                return Record.HDSN;
            }
            set
            {
                Record.HDSN = value;
            }
        }
        public string IP_Key
        {
            get
            {
                return Record.IP_Key;
            }
            set
            {
                Record.IP_Key = value;
            }
        }
    }
}
