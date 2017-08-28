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
    public partial class ApproachPingHandler
    {
        [HeaderPacket(HeaderEnum.PING)]
        public static void HandlePingMessage(SimpleClient client, PingMessage message)
        {
            client.Send(new LoginMessage(message.unknown));
        }
    }
}