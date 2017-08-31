using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronos.Core.IO;
using Chronos.Protocol.Enums;
using static Chronos.Protocol.MessageReceiver;

namespace Chronos.Protocol.Messages
{
    public class QueryTickCountMessage : NetworkMessage
    {
        public const HeaderEnum Header = HeaderEnum.QUERYTICKCOUNT;
        public uint tickCountClient;
        public override ushort MessageId => (ushort)Header;

        public QueryTickCountMessage() { }

        public override void Deserialize(IDataReader reader)
        {
            tickCountClient = reader.ReadUInt();
        }

        public override void Serialize(IDataWriter writer)
        {
            throw NetworkMessageException.SerializeException;
        }
    }
}
