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
    public class PingMessage : NetworkMessage
    {
        public const HeaderEnum Header = HeaderEnum.PING;
        public uint unknown; // is it position ???

        public override ushort MessageId => (ushort)Header;
        public PingMessage() { }

        public override void Deserialize(IDataReader reader)
        {
            unknown = reader.ReadUInt();
        }

        public override void Serialize(IDataWriter writer)
        {
            throw NetworkMessageException.SerializeException;
        }
    }
}