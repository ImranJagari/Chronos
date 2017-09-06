using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronos.Core.IO;
using Chronos.Protocol.Enums;
using Chronos.Protocol.Types.ObjectsType;

namespace Chronos.Protocol.Messages.Snapshots
{
    public class AddObjectSnapshot : Snapshot
    {
        public override SnapshotTypeEnum SnapshotType => SnapshotTypeEnum.ADDOBJ;

        public ObjectType obj;
        public AddObjectSnapshot(ObjectType obj)
        {
            this.obj = obj;
        }
        public override void Serialize(IDataWriter writer)
        {
            obj.SerializeNeverChange(writer);
            obj.SerializeSometimesChange(writer);
            obj.SerializeAlwaysChange(writer);
        }
    }
}
