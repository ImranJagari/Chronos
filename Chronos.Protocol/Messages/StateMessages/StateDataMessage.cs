using Chronos.Core.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Chronos.Protocol.Messages.StateMessages
{
    public abstract class StateDataMessage
    {
        public abstract ushort MessageId { get; }

        public void Unpack(IDataReader reader)
        {
            Deserialize(reader);
        }
        public abstract void Deserialize(IDataReader reader);
    }
}