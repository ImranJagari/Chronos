using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Chronos.Core.IO;
using Chronos.Protocol.Enums;

namespace Chronos.Protocol.Messages.Snapshots
{
    public class MoveObjectSnapshot : Snapshot
    {
        public override SnapshotTypeEnum SnapshotType => SnapshotTypeEnum.MOVEOBJ;

        public int objId;
        public int x;
        public int y;
        public int z;

        public MoveObjectSnapshot(int objId, int x, int y, int z)
        {
            this.objId = objId;
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(objId);
            writer.WriteBytes(new byte[] { 0x00, 0x0E, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF });
            writer.WriteInt(x * 1000);
            writer.WriteInt(y * 1000);
            writer.WriteInt(z * 1000);
        }
    }
}