using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Chronos.Core.IO;
using Chronos.Protocol.Enums;

namespace Chronos.Protocol.Messages.Snapshots
{
    public class RemoveObjectSnapshot : Snapshot
    {
        public override SnapshotTypeEnum SnapshotType => SnapshotTypeEnum.REMOVEOBJ;
        public uint objId;

        public RemoveObjectSnapshot(uint objId)
        {
            this.objId = objId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUInt(objId);
        }
    }
}