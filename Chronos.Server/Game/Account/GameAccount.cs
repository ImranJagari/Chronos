using Chronos.Protocol.Enums;
using Chronos.Server.Databases.Account;
using Chronos.Server.Game.Actors.Context.Characters;
using Chronos.Server.Manager.Characters;
using System.Collections.Generic;

namespace Chronos.Server.Game.Account
{
    public class GameAccount
    {
        public AccountRecord Record { get; }

        public GameAccount(AccountRecord record)
        {
            Record = record;
            Characters = new List<Character>();
            LoadRecord();
        }
        public List<Character> Characters;
        public int Id
        {
            get
            {
                return Record.Id;
            }
            set
            {
                Record.Id = value;
            }
        }
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
        public AuthorityEnum Authority
        {
            get
            {
                return (AuthorityEnum)Record.Authority;
            }
            set
            {
                Record.Authority = (byte)value;
            }
        }
        public void LoadRecord()
        {
            Characters = CharacterManager.Instance.GetCharactersByAccountId(Id);
        }
    }
}
