using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Chronos.Protocol.Enums;
using Chronos.Core.IO;

namespace Chronos.Protocol.Types.ObjectsType
{
    public class CharacterObjectType : SpriteObjectType
    {
        public uint characterId;
        public byte sex;
        public int job;
        public int authority;
        public int serverId;
        public string vipbar_name;

        public short constellation;
        public short city;
        public byte hair_mesh;
        public uint hair_color;
        public byte head_mesh;
        public uint option;
        public uint teamId;
        public string vendor_sentence;
        public int slaughter;
        public int kill_num;
        public int fame;
        public byte pk_mode;
        public byte pk_name;
        public int gray_time;
        public int prot_cool_down_start;
        public int issf_;

        public int items_count;
        public ItemType[] items;
        public byte ext_param_count;
        public byte[] index;
        public int[] value; //WriteX
        public int title_count;
        public int[] titles;
        public int title_flag;
        public byte title_count2;
        public int[] titles2;
        public int[] host_ids;
        public KingdomType kingdom;
        public MasterType master;
        public MarriageType marriage;
        public int show_count;
        public int[] indexes_show;
        public int vessel_refine_lv;
        public int vessel_equip_index;
        public bool vessel_is_equip;
        public bool flag_data;
        public FlagsDataType flagsData;
        //TO CONTINUE
        public CharacterObjectType(ObjectTypeEnum objectType, uint objectId, uint id, uint linkId, float x, float y, float z, float angle, float angleX,
            short scale, string name, int count_data, ushort[] rIndexes_data, int[] values_data, byte count_buff, byte[] buffsns, int[] buffIds,
            int[] ticks, uint characterId, byte sex, int job, int authority, int serverId, string vipbar_name, short constellation, short city,
            byte hair_mesh, uint hair_color, byte head_mesh, uint option, uint teamId, string vendor_sentence, int slaughter, int kill_num, int fame,
            byte pk_mode, byte pk_name, int gray_time, int prot_cool_down_start, int issf_, int items_count, ItemType[] items, byte ext_param_count,
            byte[] index, int[] value, int title_count, int[] titles, int title_flag, byte title_count2, int[] titles2, int[]host_ids, KingdomType kingdom,
            MasterType master, MarriageType marriage, int show_count, int[] indexes_show, int vessel_refine_lv, int vessel_equip_index, bool vessel_is_equip, bool flag_data, FlagsDataType flagsData) 
            : base(objectType, objectId, id, linkId, x, y, z, angle, angleX, scale, name, count_data, rIndexes_data, values_data, count_buff, buffsns, buffIds, ticks)
        {
            this.characterId = characterId;
            this.sex = sex;
            this.job = job;
            this.authority = authority;
            this.serverId = serverId;
            this.vipbar_name = vipbar_name;
            this.constellation = constellation;
            this.city = city;
            this.hair_mesh = hair_mesh;
            this.hair_color = hair_color;
            this.head_mesh = head_mesh;
            this.option = option;
            this.teamId = teamId;
            this.vendor_sentence = vendor_sentence;
            this.slaughter = slaughter;
            this.kill_num = kill_num;
            this.fame = fame;
            this.pk_mode = pk_mode;
            this.pk_name = pk_name;
            this.gray_time = gray_time;
            this.prot_cool_down_start = prot_cool_down_start;
            this.issf_ = issf_;
            this.items_count = items_count;
            this.items = items;
            this.ext_param_count = ext_param_count;
            this.index = index;
            this.value = value;
            this.title_count = title_count;
            this.titles = titles;
            this.title_flag = title_flag;
            this.title_count = title_count;
            this.titles2 = titles2;
            this.kingdom = kingdom;
            this.master = master;
            this.marriage = marriage;
            this.show_count = show_count;
            this.indexes_show = indexes_show;
            this.vessel_refine_lv = vessel_refine_lv;
            this.vessel_equip_index = vessel_equip_index;
            this.vessel_is_equip = vessel_is_equip;
            this.flag_data = flag_data;
            this.flagsData = flagsData;
        }
        public override void SerializeNeverChange(IDataWriter writer)
        {
            base.SerializeNeverChange(writer);
            writer.WriteUInt(characterId);
            writer.WriteByte(sex);
            writer.WriteInt(job);
            writer.WriteInt(authority);
            writer.WriteInt(serverId);
            writer.WriteUTF(vipbar_name);
        }
        public override void SerializeSometimesChange(IDataWriter writer)
        {
            base.SerializeSometimesChange(writer);
            writer.WriteShort(constellation);
            writer.WriteShort(city);
            writer.WriteByte(hair_mesh);
            writer.WriteUInt(hair_color);
            writer.WriteByte(head_mesh);
            writer.WriteUInt(option);
            writer.WriteUInt(teamId);
            writer.WriteUTF(vendor_sentence);
            writer.WriteInt(slaughter);
            writer.WriteInt(kill_num);
            writer.WriteInt(fame);
            writer.WriteByte(pk_mode);
            writer.WriteByte(pk_name);
            writer.WriteInt(gray_time);
            writer.WriteInt(prot_cool_down_start);
            writer.WriteInt(issf_);
        }
        public override void SerializeAlwaysChange(IDataWriter writer)
        {
            base.SerializeAlwaysChange(writer);
            writer.WriteUInt((uint)items_count);
            foreach (ItemType item in items)
            {
                item.Serialize(writer);
            }
            writer.WriteByte(ext_param_count);
            for (int i = 0; i < ext_param_count; i++)
            {
                writer.WriteByte(index[i]);
                writer.WriteLittleX(value[i]);
            }
            writer.WriteUInt((uint)title_count);
            foreach (int title in titles)
            {
                writer.WriteInt(title);
            }
            writer.WriteUInt((uint)title_flag);
            writer.WriteByte(title_count2);
            for (int i = 0; i < title_count2; i++)
            {
                writer.WriteInt(titles2[i]);
                writer.WriteInt(host_ids[i]);
            }
            kingdom.Serialize(writer);
            master.Serialize(writer);
            marriage.Serialize(writer);
            writer.WriteInt(show_count);
            foreach (int index_show in indexes_show)
            {
                writer.WriteInt(index_show);
            }
            writer.WriteUInt((uint)vessel_refine_lv);
            writer.WriteUInt((uint)vessel_equip_index);
            writer.WriteBoolean(vessel_is_equip);
            writer.WriteByte(flag_data ? (byte)1 : (byte)0);
            if (flag_data)
                flagsData.Serialize(writer);
        }
    }
}