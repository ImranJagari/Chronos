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
    public class TickCountMessage : NetworkMessage
    {
        public const HeaderEnum Header = HeaderEnum.TICKCOUNT;
        public uint tickCountServer;
        public override ushort MessageId => (ushort)Header;
        public TickCountMessage() { }
        public TickCountMessage(uint tickCountServer)
        {
            this.tickCountServer = tickCountServer;
        }
        public override void Deserialize(IDataReader reader)
        {
            throw NetworkMessageException.DeserializeException;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUInt(tickCountServer);
        }
    }
}
