using Chronos.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Protocol.Messages
{

//w => Word => ushort
//u => uint ????
//s => string
//Dword => uint
//i => int
//b => byte

    public abstract class NetworkMessage
    {
        public abstract ushort MessageId { get; }
        public abstract void Serialize(IDataWriter writer);
        public abstract void Deserialize(IDataReader reader);
        public void Pack(IDataWriter writer)
        {
            Serialize(writer);
        }
        public void Unpack(IDataReader reader)
        {
            Deserialize(reader);
        }
    }
}
