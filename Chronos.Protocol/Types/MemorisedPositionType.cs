using Chronos.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Protocol.Types
{
    public class MemorisedPositionType
    {
        public int index;
        public int worldid;
        public int x;
        public int z;
        public string name;
        public MemorisedPositionType(int index, int worldid, int x, int z, string name)
        {
            this.index = index;
            this.worldid = worldid;
            this.x = x;
            this.z = z;
            this.name = name;
        }
        public void Serialize(IDataWriter writer)
        {
            writer.WriteInt(index);
            writer.WriteInt(worldid);
            writer.WriteInt(x);
            writer.WriteInt(z);
            writer.WriteUTF(name);
        }
    }
}
