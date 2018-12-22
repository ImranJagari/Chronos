using Chronos.Protocol.Enums;
using Chronos.Protocol.Messages.StateMessages;
using Chronos.Server.Game.World;
using Chronos.Server.Handlers.Roleplay;
using Chronos.Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Server.Handlers.States
{
    public partial class StateMessageHandler
    {
        [StateId(StateTypeEnum.STATE_MOVE_TO)]
        public static void HandleStateMoveTo(SimpleClient client, StateMoveToMessage message)
        {
            
            if (!client.Character.Position.IsInRange(new ObjectPosition(message.x / 1000f, message.y / 1000f, message.z / 1000f), (int)DefineEnum.MAXDISTANCE))
            {
                client.Disconnect();
                return;
            }

            client.Character.Position.X = message.x / 1000f;
            client.Character.Position.Y = message.y / 1000f;
            client.Character.Position.Z = message.z / 1000f;

            client.Character.Map.SendMouvementMessageToNearestClients(client.Character);
        }

    }
}
