using Chronos.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Protocol.Types
{
    public class PositionType
    {
        public float x;
        public float y;
        public float z;
        public PositionType() { }
        public PositionType(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public virtual void Deserialise(IDataReader reader)
        {
            x = reader.ReadFloat();
            y = reader.ReadFloat();
            z = reader.ReadFloat();
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteFloat(x);
            writer.WriteFloat(y);
            writer.WriteFloat(z);
        }
       
    }
}
