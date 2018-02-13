using Chronos.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Protocol.Types
{
    public class ItemType
    {
        public byte slot;
        public uint itemId;
        public uint m_word;
        public int m_option;
        public ItemType() { }
        public ItemType(byte slot, uint itemId, uint m_word, int m_option)
        {
            this.slot = slot;
            this.itemId = itemId;
            this.m_word = m_word;
            this.m_option = m_option;
        }
        public virtual void Deserialize(IDataReader reader)
        {
            slot = reader.ReadByte();
            itemId = reader.ReadUInt();
            m_word = reader.ReadUInt();
            m_option = reader.ReadInt();
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteShort(slot);
            writer.WriteUInt(itemId);
            writer.WriteUInt(m_word);
            writer.WriteInt(m_option);
        }
    }
}

/*
    parts = LAr:ReadByte(ar)
      item_id = LAr:ReadDword(ar)
      flag = LAr:ReadDword(ar)
      attr = LAr:ReadInt(ar)
      if parts >= 0 and parts<NEUZ.MAX_HUMAN_PARTS then
        player.equip_info[parts].m_itemid = item_id
        player.equip_info[parts].m_word = flag
        player.equip_info[parts].m_option = attr
      end
*/
