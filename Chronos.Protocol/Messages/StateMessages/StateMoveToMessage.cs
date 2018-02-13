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

        public int angle;
        public int x;
        public int y;
        public int z;

        public StateMoveToMessage() { }

        public override void Deserialize(IDataReader reader)
        {
            angle = reader.ReadInt();
            x = reader.ReadInt();
            y = reader.ReadInt();
            z = reader.ReadInt();
        }
    }
}