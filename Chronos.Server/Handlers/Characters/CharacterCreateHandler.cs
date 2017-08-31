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

namespace Chronos.Server.Handlers.Characters
{
    public partial class CharacterHandler
    {
        [HeaderPacket(HeaderEnum.CREATEPLAYER)]
        public static void HandleCreateCharacterMessage(SimpleClient client, CreateCharacterMessage message)
        {
            ErrorEnum error = CharacterManager.Instance.CreateCharacter(client, message);
            SendCreateCharacterResultMessage(client, error);
            if (error == ErrorEnum.ERR_SUCCESS)
                SendCharacterSlotMessage(client, client.Character, client.Account.Characters.Count(x => x.DeletedDate.HasValue));
        }
        public static void SendCreateCharacterResultMessage(IPacketInterceptor client, ErrorEnum result)
        {
            client.Send(new CreateCharacterResultMessage((int)result));
        }
    }
}