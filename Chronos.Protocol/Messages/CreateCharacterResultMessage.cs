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
    public class CreateCharacterResultMessage : NetworkMessage
    {
        public const HeaderEnum Header = HeaderEnum.CREATEPLAYERRESULT;

        public int result;

        public override ushort MessageId => (ushort)Header;

        public CreateCharacterResultMessage() { }
        public CreateCharacterResultMessage(int result)
        {
            this.result = result;
        }

        public override void Deserialize(IDataReader reader)
        {
            throw NetworkMessageException.DeserializeException;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(result);
        }
    }
}