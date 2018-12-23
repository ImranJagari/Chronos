using Chronos.Core.IO;
using Chronos.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Protocol.Types
{
    public class FriendMemberType
    {
        public int playerId;
        public byte sex;
        public byte job;
        public short level;
        public FriendStateEnum state;
        public int close_points;
        public byte issf_;
        public string name;
        public string class_name;

        public FriendMemberType(int playerId, byte sex, byte job, short level, FriendStateEnum state, int close_points, byte issf_, string name, string class_name)
        {
            this.playerId = playerId;
            this.sex = sex;
            this.job = job;
            this.level = level;
            this.state = state;
            this.close_points = close_points;
            this.issf_ = issf_;
            this.name = name;
            this.class_name = class_name;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteInt(playerId);
            writer.WriteByte(sex);
            writer.WriteByte(job);
            writer.WriteShort(level);
            writer.WriteByte((byte)state);
            writer.WriteInt(close_points);
            writer.WriteByte(issf_);
            writer.WriteUTF(name);
            writer.WriteUTF(class_name);
        }
    }
}
