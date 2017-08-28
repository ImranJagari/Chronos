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
    public class LoginMessage : NetworkMessage
    {
        public const HeaderEnum Header = HeaderEnum.LOGIN;
        public uint unknown;
        public override ushort MessageId => (ushort)Header;
        
        public LoginMessage() { }
        public LoginMessage(uint unknown)
        {
            this.unknown = unknown;
        }

        public override void Deserialize(IDataReader reader)
        {
            throw NetworkMessageException.DeserializeException;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUInt(unknown);
        }
    }
}