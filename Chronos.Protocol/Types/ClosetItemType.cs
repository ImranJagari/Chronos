using Chronos.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Protocol.Types
{
    public class ClosetItemType
    {
        public int id;

        public ClosetItemType() { }
        public ClosetItemType(int id)
        {
            this.id = id;
        }
        public virtual void Deserialize(IDataReader reader)
        {
            id = reader.ReadInt();
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteInt(id);
        }
    }
}
