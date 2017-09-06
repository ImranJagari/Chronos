using Chronos.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Protocol.Types
{
    public class FactionType
    {
        bool is_master;
        MasterFactionType master;
        int protege_count;
        int honor_points;
        int honor_points_total;
        int level_points;
        byte m_is_faction;
        string faction_name;
        public ProtegeType[] proteges;

        public FactionType(int protege_count, int honor_points, int honor_points_total, int level_points, byte m_is_faction, string faction_name, ProtegeType[] proteges)
        {
            this.protege_count = protege_count;
            this.honor_points = honor_points;
            this.honor_points_total = honor_points_total;
            this.level_points = level_points;
            this.m_is_faction = m_is_faction;
            this.faction_name = faction_name;
            this.proteges = proteges;
        }
        public FactionType(bool is_master, MasterFactionType master, int protege_count, int honor_points, int honor_points_total,
            int level_points, byte m_is_faction, string faction_name, ProtegeType[] proteges) 
            : this( protege_count,  honor_points,  honor_points_total,  level_points,  m_is_faction,  faction_name, proteges)
        {
            this.is_master = is_master;
            this.master = master;
        }
        public void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(is_master);
            if (is_master)
                master.Serialize(writer);
            writer.WriteInt(protege_count);
            writer.WriteInt(honor_points);
            writer.WriteInt(honor_points_total);
            writer.WriteInt(level_points);
            writer.WriteByte(m_is_faction);
            writer.WriteUTF(faction_name);
            foreach (ProtegeType protege in proteges)
                protege.Serialize(writer);
        }
    }
}
