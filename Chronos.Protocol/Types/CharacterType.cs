using Chronos.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Protocol.Types
{
    public class CharacterType
    {
        public int slot;
        public string name;
        public int id;
        public int sceneId;
        public byte sex;
        public PositionType position;
        public int level;
        public int job;
        public int strenght;
        public int stamina;
        public int dexterity;
        public int intelligence;
        public int spi;// ????
        public int hair_mesh;
        public int hair_color;
        public int head_mesh;
        public int is_block;
        public int block_time;
        public int items_count;
        public ItemType[] items;
        public ClosetItemType[] closetItems;
        public CharacterType() { }
        public CharacterType(int slot, string name, int id, int sceneId, byte sex, PositionType position, int level,
            int job, int strenght, int stamina, int dexterity, int intelligence, int spi, int hair_mesh, int hair_color,
            int head_mesh, int is_block, int block_time, int items_count, ItemType[] items, ClosetItemType[] closetItems)
        {
            this.slot = slot;
            this.name = name;
            this.id = id;
            this.sceneId = sceneId;
            this.sex = sex;
            this.position = position;
            this.level = level;
            this.job = job;
            this.strenght = strenght;
            this.stamina = stamina;
            this.dexterity = dexterity;
            this.intelligence = intelligence;
            this.spi = spi;
            this.hair_mesh = hair_mesh;
            this.hair_color = hair_color;
            this.head_mesh = head_mesh;
            this.is_block = is_block;
            this.block_time = block_time;
            this.items = items;
            this.closetItems = closetItems;
        }
        public virtual void Deserialize(IDataReader reader)
        {
            slot = reader.ReadInt();
            name = reader.ReadUTF();
            id = reader.ReadInt();
            sceneId = reader.ReadInt();
            sex = reader.ReadByte();
            position = new PositionType();
            position.Deserialise(reader);
            level = reader.ReadInt();
            job = reader.ReadInt();
            strenght = reader.ReadInt();
            stamina = reader.ReadInt();
            dexterity = reader.ReadInt();
            intelligence = reader.ReadInt();
            spi = reader.ReadInt();
            hair_mesh = reader.ReadInt();
            hair_color = reader.ReadInt();
            head_mesh = reader.ReadInt();
            is_block = reader.ReadInt();
            block_time = reader.ReadInt();
            items_count = reader.ReadInt();
            for (int i = 0; i < items_count; i++)
            {
                items[i] = new ItemType();
                items[i].Deserialize(reader);
            }
            for (int i = 0; i < 5; i++)
            {
                closetItems[i] = new ClosetItemType();
                closetItems[i].Deserialize(reader);
            }
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteInt(slot);
            writer.WriteUTF(name);
            writer.WriteInt(id);
            writer.WriteInt(sceneId);
            writer.WriteByte(sex);
            position.Serialize(writer);
            writer.WriteInt(level);
            writer.WriteInt(job);
            writer.WriteInt(strenght);
            writer.WriteInt(stamina);
            writer.WriteInt(dexterity);
            writer.WriteInt(intelligence);
            writer.WriteInt(spi);
            writer.WriteInt(hair_mesh);
            writer.WriteInt(hair_color);
            writer.WriteInt(head_mesh);
            writer.WriteInt(is_block);
            writer.WriteInt(block_time);
            writer.WriteInt(items_count);
            for (int i = 0; i < items_count; i++)
            {
                items[i].Serialize(writer);
            }
            for (int i = 0; i < 5; i++)
            {
                closetItems[i].Serialize(writer);
            }

        }
    }
}
/*
          for i = 1, player_count do
    local slot = LAr:ReadInt(ar) + 1
    self.m_slots[slot] = {}
    self.m_slots[slot].m_data = {}
    local player = self.m_slots[slot].m_data
    player.player_name = LAr:ReadString(ar)
    player.player_id = LAr:ReadInt(ar)
    player.scene_id = LAr:ReadInt(ar)
    player.sex = LAr:ReadByte(ar)
    player.pox_x = LAr:ReadFloat(ar)
    player.pox_y = LAr:ReadFloat(ar) + 20
    player.pox_z = LAr:ReadFloat(ar)
    player.player_level = LAr:ReadInt(ar)
    player.player_job = LAr:ReadInt(ar)
    player.player_str = LAr:ReadInt(ar)
    player.player_sta = LAr:ReadInt(ar)
    player.player_dex = LAr:ReadInt(ar)
    player.player_int = LAr:ReadInt(ar)
    player.player_spi = LAr:ReadInt(ar)
    player.skinset = 0
    player.hair_mesh = LAr:ReadInt(ar)
    player.hair_color = LAr:ReadDword(ar)
    player.head_mesh = LAr:ReadInt(ar)
    player.is_block = LAr:ReadInt(ar)
    player.block_time = LAr:ReadInt(ar)
    player.equip_info = {}
    for i = 0, NEUZ.MAX_HUMAN_PARTS - 1 do
      player.equip_info[i] = equip_info_t.new()
    end
    local parts, item_id, flag, attr = nil, nil, nil, nil
    local count = LAr:ReadInt(ar)
    for i = 1, count do
      parts = LAr:ReadByte(ar)
      item_id = LAr:ReadDword(ar)
      flag = LAr:ReadDword(ar)
      attr = LAr:ReadInt(ar)
      if parts >= 0 and parts<NEUZ.MAX_HUMAN_PARTS then
        player.equip_info[parts].m_itemid = item_id
        player.equip_info[parts].m_word = flag
        player.equip_info[parts].m_option = attr
      end
    end
    for j = 1, 5 do
      item_id = LAr:ReadInt(ar)
      local prop = propItem[item_id]
      if prop then
        parts = prop.Parts
      end
      if parts >= 0 and parts < NEUZ.MAX_HUMAN_PARTS then
        player.equip_info[parts].m_itemid = item_id
        player.equip_info[parts].m_word = 0
        player.equip_info[parts].m_option = 0
      end
    end
  end*/
