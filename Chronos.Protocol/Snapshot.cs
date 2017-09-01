using Chronos.Core.IO;
using Chronos.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Chronos.Protocol
{
    public abstract class Snapshot
    {
        public abstract SnapshotTypeEnum SnapshotType { get; }
        public abstract void Serialize(IDataWriter writer);
        public void Pack(IDataWriter writer)
        {
            if (SnapshotType < (SnapshotTypeEnum)128) // less than a 'Char' or 'Byte' (8bits)
            {
                writer.WriteByte((byte)SnapshotType);
            }
            else
            {
                writer.WriteBytes(BitConverter.GetBytes((ushort)SnapshotType).Reverse().ToArray());
            }
            Serialize(writer);
        }
    }
}