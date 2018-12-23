using Chronos.Core.IO;
using Chronos.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Chronos.Protocol.Messages.StateMessages
{
    public class StateMoveToMessage : StateDataMessage
    {
        public const StateTypeEnum TypeId = StateTypeEnum.STATE_MOVE_TO;

        public override ushort MessageId => (ushort)TypeId;

        public int objId;
        public int param1;
        public int param2;
        public int param3;

        public StateMoveToMessage() { }

        public override void Deserialize(IDataReader reader)
        {
            objId = reader.ReadInt();
            param1 = reader.ReadInt();
            param2 = reader.ReadInt();
            param3 = reader.ReadInt();
        }
    }
}