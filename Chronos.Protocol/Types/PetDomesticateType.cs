using Chronos.Core.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Chronos.Protocol.Types
{
    public class PetDomesticateType
    {
        public int id;
        public int count;
        public PetDomesticateStatsType[] stats;
        public PetDomesticateType(int id,int count, PetDomesticateStatsType[] stats)
        {
            this.id = id;
            this.count = count;
            this.stats = stats;
        }
        public void Serialize(IDataWriter writer)
        {
            writer.WriteInt(id);
            writer.WriteInt(count);
            foreach (PetDomesticateStatsType stat in stats)
                stat.Serialize(writer);
        }
    }
}