using Chronos.Core.IO;
using Chronos.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Protocol.Types
{
    public class InventoryType
    {
        public short item_max;
        public short index_num;
        public short[] itemIds;
        public int itemElement_count;
        public ItemElementType[] item_elements;
        public short[] objectIds;

        public InventoryType(short[] itemIds, int itemElement_count, ItemElementType[] item_elements, short[] objectIds)
        {
            item_max = ((short)DefineEnum.MAX_INVENTORY + (short)DefineEnum.MAX_HUMAN_PARTS);
            index_num = (short)DefineEnum.MAX_INVENTORY;
            this.itemIds = itemIds;
            this.itemElement_count = itemElement_count;
            this.item_elements = item_elements;
            this.objectIds = objectIds;
        }
        public void Serialize(IDataWriter writer)
        {
            writer.WriteUShort((ushort)item_max);
            writer.WriteUShort((ushort)index_num);
            foreach (short itemId in itemIds)
            {
                writer.WriteUShort((ushort)itemId);
            }
            //writer.WriteShort((byte)item_max);
            //writer.WriteShort((byte)index_num);
            //foreach(short itemId in itemIds)
            //{
            //    writer.WriteShort((byte)itemId);
            //}
            writer.WriteInt((byte)itemElement_count);
            for(int i = 0; i < itemElement_count; i++)
            {
                item_elements[i].Serialize(writer);
                writer.WriteUShort((ushort)objectIds[i]);
            }
        }
    }
}
