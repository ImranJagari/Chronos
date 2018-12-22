using Chronos.Core.IO;
using Chronos.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Protocol.Types
{
    public class ProtegeType
    {
        public int playerId;
        public string name;
        public int job;
        public byte sex;
        public int level;
        public int close_points;
        public int close_points_total;
        public int time;
        public byte guide_quest_count;
        public uint[] questids;
        public int last_logout_time;
        public FriendStateEnum state;

        public ProtegeType(int playerId, string name, int job, byte sex, int level, int close_points, int close_points_total,
            int time, byte guide_quest_count, uint[] questids, int last_logout_time, FriendStateEnum state)
        {
            this.playerId = playerId;
            this.name = name;
            this.job = job;
            this.sex = sex;
            this.level = level;
            this.close_points = close_points;
            this.close_points_total = close_points_total;
            this.time = time;
            this.guide_quest_count = guide_quest_count;
            this.questids = questids;
            this.last_logout_time = last_logout_time;
            this.state = state;
        }
        public void Serialize(IDataWriter writer)
        {
            writer.WriteInt(playerId);
            writer.WriteUTF(name);
            writer.WriteInt(job);
            writer.WriteByte(sex);
            writer.WriteInt(level);
            writer.WriteInt(close_points);
            writer.WriteInt(close_points_total);
            writer.WriteInt(time);
            writer.WriteByte(guide_quest_count);
            foreach(uint quest in questids)
                writer.WriteUInt(quest);
            writer.WriteInt(last_logout_time);
            writer.WriteByte((byte)state);
        }
    }
}
