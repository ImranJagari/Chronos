using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronos.Core.IO;
using Chronos.Protocol.Enums;

namespace Chronos.Protocol.Messages.Snapshots
{
    public class SetValueObjectSnapshot : Snapshot
    {
        public override SnapshotTypeEnum SnapshotType => SnapshotTypeEnum.SET_VAL;
        public uint objectId;
        public short attrId;
        public int value;

        public SetValueObjectSnapshot(uint objectId, short attrId, int value)
        {
            this.objectId = objectId;
            this.attrId = attrId;
            this.value = value;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUInt(objectId);
            writer.WriteShort(attrId);
            writer.WriteInt(value);
        }
    }
}
