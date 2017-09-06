using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Chronos.Core.IO;
using Chronos.Protocol.Enums;

namespace Chronos.Protocol.Messages.Snapshots
{
    public class UpdateServerTimeSnapshot : Snapshot
    {
        public override SnapshotTypeEnum SnapshotType => SnapshotTypeEnum.UPDATE_SERVER_TIME;

        public int serverTime;

        public UpdateServerTimeSnapshot() { }
        public UpdateServerTimeSnapshot(int serverTime)
        {
            this.serverTime = serverTime;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(serverTime);
        }
    }
}