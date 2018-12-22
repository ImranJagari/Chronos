using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronos.Core.IO;
using Chronos.Protocol.Enums;

namespace Chronos.Protocol.Types.ObjectsType
{
    public class MonsterObjectType : SpriteObjectType
    {
        public byte active_atk;
        public bool m_atkable_special;
        public byte m_diebuff;
        public uint m_activity;
        public uint m_owner_family_id;
        public uint m_quest_owner_playerid;
        public byte owner_count;
        public uint[] m_owner_players;
        public int m_owner_team;
        public bool m_atkable_owner;
        public bool m_atkable_player;
        public bool m_attack_protect;
        public uint m_first_team_id;
        public uint m_first_player_id;
        public byte m_visiable;
        public int m_blend_factor;
        public uint m_manor_familyid;

        public MonsterObjectType(ObjectTypeEnum objectType, uint objectId, uint id, uint linkId, float x, float y, float z,
            float angle, float angleX, short scale, string name, int count_data, ushort[] rIndexes_data, int[] values_data,
            byte count_buff, byte[] buffsns, int[] buffIds, int[] ticks, byte active_atk, bool m_atkable_special, byte m_diebuff,
            uint m_activity, uint m_owner_family_id, uint m_quest_owner_playerid, byte owner_count, uint[] m_owner_players, 
            int m_owner_team, bool m_atkable_owner, bool m_atkable_player, bool m_attack_protect, uint m_first_team_id, 
            uint m_first_player_id, byte m_visiable, int m_blend_factor, uint m_manor_familyid) 
            : base(objectType, objectId, id, linkId, x, y, z, angle, angleX, 
                  scale, name, count_data, rIndexes_data, values_data, count_buff,
                  buffsns, buffIds, ticks)
        {
            this.active_atk = active_atk;
            this.m_atkable_special = m_atkable_special;
            this.m_diebuff = m_diebuff;
            this.m_activity = m_activity;
            this.m_owner_family_id = m_owner_family_id;
            this.m_quest_owner_playerid = m_quest_owner_playerid;
            this.owner_count = owner_count;
            this.m_owner_players = m_owner_players;
            this.m_owner_team = m_owner_team;
            this.m_atkable_owner = m_atkable_owner;
            this.m_atkable_player = m_atkable_player;
            this.m_attack_protect = m_attack_protect;
            this.m_first_team_id = m_first_team_id;
            this.m_first_player_id = m_first_player_id;
            this.m_visiable = m_visiable;
            this.m_blend_factor = m_blend_factor;
            this.m_manor_familyid = m_manor_familyid;
        }
        public override void SerializeNeverChange(IDataWriter writer)
        {
            base.SerializeNeverChange(writer);
            writer.WriteByte(active_atk);
            writer.WriteBoolean(m_atkable_special);
            writer.WriteByte(m_diebuff);
            writer.WriteUInt(m_activity);
            writer.WriteUInt(m_owner_family_id);
            writer.WriteUInt(m_quest_owner_playerid);
        }
        public override void SerializeSometimesChange(IDataWriter writer)
        {
            base.SerializeSometimesChange(writer);
            writer.WriteByte(owner_count);
            for (int i = 1; i < owner_count; i++)
                writer.WriteUInt(m_owner_players[i]);
            writer.WriteInt(m_owner_team);
            writer.WriteBoolean(m_atkable_owner);
            writer.WriteBoolean(m_atkable_player);
            writer.WriteBoolean(m_attack_protect);
            writer.WriteUInt(m_first_team_id);
            writer.WriteUInt(m_first_player_id);
            writer.WriteByte(m_visiable);
            writer.WriteInt(m_blend_factor);
            writer.WriteUInt(m_manor_familyid);
        }
        public override void SerializeAlwaysChange(IDataWriter writer)
        {
            base.SerializeAlwaysChange(writer);
        }
    }
}
