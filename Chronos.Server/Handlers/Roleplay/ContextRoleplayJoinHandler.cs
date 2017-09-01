using Chronos.Protocol.Enums;
using Chronos.Protocol.Messages;
using Chronos.Protocol.Types;
using Chronos.Server.Game.Actors.Characters;
using Chronos.Server.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Chronos.Server.Handlers.Roleplay
{
    public partial class ContextRoleplayHandler
    {
        [HeaderPacket(HeaderEnum.JOIN)]
        public static void HandleJoinMessage(SimpleClient client, JoinMessage message)
        {
            Character character = client.Account.Characters.FirstOrDefault(x => x.Id == message.characterId);
            if(character == null)
            {
                client.Disconnect();
                return;
            }
            client.Character = character;

            SendJoinRightMessage(client, message.characterId, character.SceneId, character.X, character.Y, character.Z, false, 0);//Send all players in the map

        }
        public static void SendJoinRightMessage(IPacketInterceptor client, int characterId, int sceneId, float x, float y, float z, bool isFestival, uint festivalDay)
        {
            client.Send(new JoinRightMessage(characterId, sceneId, new PositionType(x, y, z), isFestival, festivalDay));
        }
    }
}