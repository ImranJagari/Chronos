using Chronos.Core.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Chronos.Protocol.Types
{
    public class LoverType
    {
        public uint playerId;
        public string name;
        public uint lover_date;
        public int lover_intimacy;
        public LoverType(uint playerId, string name, uint lover_date, int lover_intimacy)
        {
            this.playerId = playerId;
            this.name = name;
            this.lover_date = lover_date;
            this.lover_intimacy = lover_intimacy;
        }
        public void Serialize(IDataWriter writer)
        {
            writer.WriteUInt(playerId);
            writer.WriteUTF(name);
            writer.WriteUInt(lover_date);
            writer.WriteInt(lover_intimacy);
        }
    }
}