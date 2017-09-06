using Chronos.Core.IO;
using Chronos.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Protocol.Types
{
    public class MasterFactionType
    {
        public int playerId;
        public string name;
        public int job;
        public byte sex;
        public int level;
        public int close_points;
        public int close_points_total;
        public int time;
        public int titleid;
        public byte m_is_faction;
        public string m_faction_name;
        public int last_logout_time;
        public FriendStateEnum state;

        public MasterFactionType(int playerId, string name, int job, byte sex, int level, int close_points, int close_points_total,
            int time, int titleid, byte m_is_faction, string m_faction_name, int last_logout_time, FriendStateEnum state)
        {
            this.playerId = playerId;
            this.name = name;
            this.job = job;
            this.sex = sex;
            this.level = level;
            this.close_points = close_points;
            this.close_points_total = close_points_total;
            this.time = time;
            this.titleid = titleid;
            this.m_is_faction = m_is_faction;
            this.m_faction_name = m_faction_name;
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
            writer.WriteInt(titleid);
            writer.WriteByte(m_is_faction);
            writer.WriteUTF(m_faction_name);
            writer.WriteInt(last_logout_time);
            writer.WriteByte((byte)state);
        }
    }
}
