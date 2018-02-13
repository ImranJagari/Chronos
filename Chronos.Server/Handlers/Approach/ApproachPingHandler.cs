using Chronos.Protocol.Enums;
using Chronos.Protocol.Messages;
using Chronos.Server.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Chronos.Server.Handlers.Approach
{
    public partial class ApproachHandler
    {
        [HeaderPacket(HeaderEnum.PING)]
        public static void HandlePingMessage(SimpleClient client, PingMessage message)
        {
            //SendLoginMessage(client, message.unknown);
        }
        public static void SendLoginMessage(IPacketInterceptor client, uint unknown)
        {
            client.Send(new LoginMessage(unknown));
        }
    }
}