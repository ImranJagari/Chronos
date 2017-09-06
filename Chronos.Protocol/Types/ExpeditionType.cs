using Chronos.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Protocol.Types
{
    public class ExpeditionType
    {
        public int level;
        public int exp;
        public int medal;
        public int coins;
        public string discharged_items_serialized;
        public uint random_time;
        public ExpeditionType(int level, int exp, int medal, int coins, string discharged_items_serialized, uint random_time)
        {
            this.level = level;
            this.exp = exp;
            this.medal = medal;
            this.coins = coins;
            this.discharged_items_serialized = discharged_items_serialized;
            this.random_time = random_time;
        }
        public void Serialize(IDataWriter writer)
        {
            writer.WriteInt(level);
            writer.WriteInt(exp);
            writer.WriteInt(medal);
            writer.WriteInt(coins);
            writer.WriteUTF(discharged_items_serialized);
            writer.WriteUInt(random_time);
        }
    }
}
