using Chronos.Protocol.Enums;
using Chronos.Protocol.Messages;
using Chronos.Server.Databases.Characters;
using Chronos.Server.Manager.Characters;
using Chronos.Server.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Chronos.Core.Extensions;

namespace Chronos.Server.Handlers.Characters
{
    public partial class CharacterHandler
    {
        [HeaderPacket(HeaderEnum.CREATEPLAYER)]
        public static void HandleCreateCharacterMessage(SimpleClient client, CreateCharacterMessage message)
        {
            ErrorEnum error = CharacterManager.Instance.CreateCharacter(client, message, client.Account.Characters.Count());
            SendCreateCharacterResultMessage(client, error);
            if (error == ErrorEnum.ERR_SUCCESS)
            {
                CharacterHandler.SendCharactersListMessage(client, DateTime.UtcNow.GetUnixTimeStamp(), 0,0,0,
                    (byte)client.Account.Characters.Count, 0, 0, client.Account.Characters.Where(x => !x.DeletedDate.HasValue).ToArray(),
                    client.Account.Characters.Count(x => x.DeletedDate.HasValue), 1, 1, 0);

                foreach (var character in client.Account.Characters.Where(x => !x.DeletedDate.HasValue))
                {
                    CharacterHandler.SendCharacterSlotMessage(client, character, client.Account.Characters.Count(x => x.DeletedDate.HasValue));
                }
            }
        }
        public static void SendCreateCharacterResultMessage(IPacketInterceptor client, ErrorEnum result)
        {
            client.Send(new CreateCharacterResultMessage((int)result));
        }
    }
}