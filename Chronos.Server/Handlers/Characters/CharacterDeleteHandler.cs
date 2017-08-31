using Chronos.Core.Extensions;
using Chronos.Protocol.Enums;
using Chronos.Protocol.Messages;
using Chronos.Server.Manager.Characters;
using Chronos.Server.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Chronos.Server.Handlers.Characters
{
    public partial class CharacterHandler
    {
        [HeaderPacket(HeaderEnum.DELETEPLAYER)]
        public static void HandleDeleteCharacterMessage(SimpleClient client, DeleteCharacterMessage message)
        {
            if (message.password == "111111")
                CharacterManager.Instance.DeleteCharacter(client, message.characterId);
            else
                SendDeleteCharacterResultMessage(client, ErrorEnum.ERR_PASSWORD, message.characterId, 0);

            SendCharactersListMessage(client, DateTime.UtcNow.GetUnixTimeStamp(), 0,
    (byte)client.Account.Characters.Count, 0, 0, client.Account.Characters.Where(x => !x.DeletedDate.HasValue).ToArray(),
    client.Account.Characters.Count(x => x.DeletedDate.HasValue), 0, 0, 0);

            foreach (var character in client.Account.Characters.Where(x => !x.DeletedDate.HasValue))
            {
                SendCharacterSlotMessage(client, character, client.Account.Characters.Count(x => x.DeletedDate.HasValue));
            }
        }
        public static void SendDeleteCharacterResultMessage(IPacketInterceptor client, ErrorEnum error, int characterId, byte flagBlocked)
        {
            client.Send(new DeleteCharacterResultMessage(error, characterId, flagBlocked));
        }
    }
}