using Chronos.Server.Databases.Characters;
using Chronos.Server.Game.Actors.Characters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Chronos.Server.Manager.Characters
{
    public class CharacterManager : DatabaseManager<CharacterManager>
    {
        public List<Character> GetCharactersByAccountId(int accountId)
        {
            return Database.Fetch<CharacterRecord>(CharacterRecordRelator.FetchQueryByAccountId, accountId).Select(x => new Character(x)).ToList();
        }
    }
}