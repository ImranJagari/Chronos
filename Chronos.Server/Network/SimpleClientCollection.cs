using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Chronos.Protocol.Messages;

namespace Chronos.Server.Network
{
    public class SimpleClientCollection : List<SimpleClient>, IPacketInterceptor
    {
        public void Send(NetworkMessage message, bool encrypt = true)
        {
            foreach(SimpleClient client in this)
            {
                client.Send(message, encrypt);
            }
        }
    }
}