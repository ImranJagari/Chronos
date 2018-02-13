using Chronos.Protocol;
using Chronos.Protocol.Enums;
using Chronos.Protocol.Messages;
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
        [HeaderPacket(HeaderEnum.STATE_MSG)]
        public static void HandleStateMessage(SimpleClient client, StateMessage message)
        {
            var stateMessage = MessageReceiver.BuildStateMessage(message.stateType, message.data);

            if (stateMessage == null)
                return;

            switch (message.stateMessage)
            {
                case StateMessageEnum.SYS_SET_STATE:
                    PacketManager.ParseStateHandler(client, stateMessage);
                    break;
            }
        }
    }
}
