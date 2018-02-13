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
    public class StateMessage : NetworkMessage
    {
        public const HeaderEnum Header = HeaderEnum.STATE_MSG;

        public StateMessageEnum stateMessage;
        public StateTypeEnum stateType;
        public byte groupId;
        public IDataReader data;

        public override ushort MessageId => (ushort)Header;

        public override void Deserialize(IDataReader reader)
        {
            stateMessage = (StateMessageEnum)reader.ReadByte();
            stateType = (StateTypeEnum)reader.ReadUShort();
            groupId = reader.ReadByte();
            data = reader;
        }

        public override void Serialize(IDataWriter writer)
        {
            throw NetworkMessageException.SerializeException;
        }
    }
}