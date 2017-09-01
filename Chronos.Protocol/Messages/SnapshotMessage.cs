using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Chronos.Core.IO;
using Chronos.Protocol.Enums;
using static Chronos.Protocol.MessageReceiver;

namespace Chronos.Protocol.Messages
{
    public class SnapshotMessage : NetworkMessage
    {
        public const HeaderEnum Header = HeaderEnum.SNAPSHOT;

        public ushort snapshotsCount;
        public Snapshot[] snapshots;

        public override ushort MessageId => (ushort)Header;

        public SnapshotMessage() { }
        public SnapshotMessage(ushort snapshotsCount, Snapshot[] snapshots)
        {
            this.snapshotsCount = snapshotsCount;
            this.snapshots = snapshots;
        }

        public override void Deserialize(IDataReader reader)
        {
            throw NetworkMessageException.DeserializeException;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUShort(snapshotsCount);
            foreach(Snapshot snapshot in snapshots)
            {
                snapshot.Pack(writer);
            }
        }
    }
}