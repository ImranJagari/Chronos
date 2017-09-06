using Chronos.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Protocol.Types
{
    public class SkillType
    {
        public int skillId;
        public uint start_time;
        public SkillType(int skillId, uint start_time)
        {
            this.skillId = skillId;
            this.start_time = start_time;
        }
        public void Serialize(IDataWriter writer)
        {
            writer.WriteInt(skillId);
            writer.WriteUInt(start_time);
        }
    }
}
