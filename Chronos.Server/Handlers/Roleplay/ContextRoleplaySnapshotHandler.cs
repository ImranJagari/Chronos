using Chronos.Protocol;
using Chronos.Protocol.Messages;
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
        public static void SendSnapshotMessage(IPacketInterceptor client, Snapshot[] snapshots)
        {
            client.Send(new SnapshotMessage((ushort)snapshots.Length, snapshots));
        }
    }
}