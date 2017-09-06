using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Chronos.Core.IO;
using Chronos.Protocol.Enums;

namespace Chronos.Protocol.Messages.Snapshots
{
    public class SetStateVerSnapshot : Snapshot
    {
        public override SnapshotTypeEnum SnapshotType => SnapshotTypeEnum.SET_STATE_VER;

        public uint state;

        public SetStateVerSnapshot() { }
        public SetStateVerSnapshot(uint state)
        {
            this.state = state;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUInt(state);
        }
    }
}