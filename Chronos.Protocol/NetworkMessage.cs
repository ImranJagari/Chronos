using Chronos.Core.IO;
using Chronos.Protocol.Enums;
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
            if ((HeaderEnum)MessageId == HeaderEnum.SESSION_KEY)
            {
                writer.WriteUInt(MessageId);
            }
            else
            {
                writer.WriteInt(0);
                writer.WriteShort((short)MessageId);
            }
            Serialize(writer);
        }
        public void Unpack(IDataReader reader)
        {
            Deserialize(reader);
        }

        public override string ToString()
        {
            return base.ToString().Split('.').Last();
        }
    }
}
