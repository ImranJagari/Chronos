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
    public class SessionKeyMessage : NetworkMessage
    {
        public const HeaderEnum Header = HeaderEnum.SESSION_KEY;

        public int seed;

        public override ushort MessageId => (ushort)Header;
        public SessionKeyMessage() { }
        public SessionKeyMessage(int seed)
        {
            this.seed = seed;
        }
        public override void Deserialize(IDataReader reader)
        {
            throw NetworkMessageException.DeserializeException;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(seed);
        }
    }
}
