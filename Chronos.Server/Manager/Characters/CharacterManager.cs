using Chronos.Protocol.Enums;
using Chronos.Protocol.Messages;
using Chronos.Server.Databases.Breeds;
using Chronos.Server.Databases.Characters;
using Chronos.Server.Databases.Items;
using Chronos.Server.Game.Actors.Context.Characters;
using Chronos.Server.Handlers.Characters;
using Chronos.Server.Manager.Breeds;
using Chronos.Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Chronos.Server.Manager.Characters
{
    public class CharacterManager : DatabaseManager<CharacterManager>
    {
        private static readonly Regex m_nameCheckerRegex = new Regex(
    "^[A-Z][a-z]{2,9}(?:-[A-Za-z][a-z]{2,9}|[a-z]{1,10})$", RegexOptions.Compiled);

        public List<Character> GetCharactersByAccountId(int accountId)
        {
            return Database.Fetch<CharacterRecord>(CharacterRecordRelator.FetchQueryByAccountId, accountId).Select(x => new Character(x)).ToList();
        }
        public bool IsCharacterNameExist(string name)
        {
            return Database.Fetch<CharacterRecord>(CharacterRecordRelator.FetchQueryByName, name).Count() > 0;
        }
        public ErrorEnum CreateCharacter(SimpleClient client, CreateCharacterMessage message)
        {
            if(!m_nameCheckerRegex.IsMatch(message.name))
            {
                return ErrorEnum.ERR_NOCREATE;
            }
            if(IsCharacterNameExist(message.name))
            {
                return ErrorEnum.ERR_PLAYER_EXIST;
            }
            BreedRecord breed = BreedManager.Instance.GetBreedByJobId(message.job);
            if(breed == null)
            {
                return ErrorEnum.ERR_NOCREATE;
            }
            CharacterRecord record = new CharacterRecord()
            {
                AccountId = client.Account.Id,
                Name = message.name,
                HD_MD5 = message.hd_md5,
                SceneId = breed.StartMap,
                Sex = message.sex == 1,
                X = breed.StartX,
                Y = breed.StartY,
                Z = breed.StartZ,
                Level = 1,
                Experience = 60,
                Job = breed.Job,
                Money = (uint)breed.StartMoney,
                HP = breed.StartHP,
                DamageTaken = 0,
                Strenght = breed.StartStrenght,
                Stamina = breed.StartStamina,
                Dexterity = breed.StartDexterity,
                Intelligence = breed.StartIntelligence,
                SPI = breed.StartSPI,
                HairMesh = message.hair_mesh,
                HairColor = (uint)message.hair_color,
                HeadMesh = message.head_mesh,
                City_Code = message.city_code,
                Constellation = message.constellation,
                Country = message.country,
                SN_Card = message.sn_card,
                Card_Type = message.card_type,
                HD_SN = message.hd_sn,
                Bin_Account = message.bin_account,
                BlockTime = DateTime.MinValue,
                DeletedDate = null
            };
            var closets = message.closets.Select(x => new ClosetItemRecord()
            {
                ClosetItemId = x.id,
                Equipped = true,
                OwnerId = record.Id
            });
            try
            {
                Database.Insert(record);
                foreach(var closet in closets)
                {
                    Database.Insert(closet);
                }
            }
            catch
            {
                return ErrorEnum.ERR_NOCREATE;
            }

            client.Character = new Character(record);
            client.Character.Client = client;

            client.Account.LoadRecord();

            return ErrorEnum.ERR_SUCCESS;
        }
        public void DeleteCharacter(SimpleClient client, int characterIdToDelete)
        {
            int index = client.Account.Characters.FindIndex(x => x.Id == characterIdToDelete);
            if (index < 0)
            {
                CharacterHandler.SendDeleteCharacterResultMessage(client, ErrorEnum.ERR_SUCCESS, characterIdToDelete, 2);
                return;
            }
            if (client.Account.Characters[index].IsBlocked)
            {
                CharacterHandler.SendDeleteCharacterResultMessage(client, ErrorEnum.ERR_PLAYER_NOT_EXIST, characterIdToDelete, 1);
                return;

            }

            client.Account.Characters[index].DeletedDate = DateTime.Now;

            try
            {
                Database.Update(client.Account.Characters[index].Record);
                CharacterHandler.SendDeleteCharacterResultMessage(client, ErrorEnum.ERR_SUCCESS, characterIdToDelete, 2);
            }
            catch
            {
                CharacterHandler.SendDeleteCharacterResultMessage(client, ErrorEnum.ERR_PLAYER_NOT_EXIST, characterIdToDelete, 0);
            }
        }
    }
}