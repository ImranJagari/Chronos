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
            if (client.GetHashCode() == message.objId) //Check if objId == -1, if it's that means that's the player is moving by clicking somewhere else, it means that is targeting something and it send the id of the object
            {
                Console.WriteLine("It's an attack !");
            }
            if (!client.Character.Position.IsInRange(new ObjectPosition(message.param1 / 1000f, message.param2 / 1000f, message.param3 / 1000f), (int)DefineEnum.MAXDISTANCE))
            {
               // client.Disconnect();
                return;
            }

            client.Character.Position.X = message.param1 / 1000f;
            client.Character.Position.Y = message.param2 / 1000f;
            client.Character.Position.Z = message.param3 / 1000f;

            client.Character.Map.SendMouvementMessageToNearestClients(client.Character);
        }

    }
}
