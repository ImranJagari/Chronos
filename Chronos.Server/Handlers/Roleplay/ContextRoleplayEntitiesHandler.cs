using Chronos.Protocol;
using Chronos.Protocol.Messages.Snapshots;
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
        public static void SendMoveObjectSnapshotMessage(IPacketInterceptor client, int objId, int x, int y, int z)
        {
            SendSnapshotMessage(client, new Snapshot[] { new MoveObjectSnapshot(objId, x, y, z) });
        }
    }
}