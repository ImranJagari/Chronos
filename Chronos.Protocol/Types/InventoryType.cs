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
        public ushort[] itemIds;
        public int itemElement_count;
        public ItemElementType[] item_elements;
        public short[] objectIds;

        public InventoryType(short itemMax, short itemNum, ushort[] itemIds, int itemElement_count, ItemElementType[] item_elements, short[] objectIds)
        {
            item_max = itemMax;
            index_num = itemNum;
            this.itemIds = itemIds;
            this.itemElement_count = itemElement_count;
            this.item_elements = item_elements;
            this.objectIds = objectIds;
        }
        public void Serialize(IDataWriter writer)
        {
            writer.WriteUShort((ushort)item_max);
            writer.WriteUShort((ushort)index_num);
            foreach (ushort itemId in itemIds)
            {
                writer.WriteUShort(itemId);
            }
            writer.WriteInt((byte)itemElement_count);
            for(int i = 0; i < itemElement_count; i++)
            {
                item_elements[i].Serialize(writer);
                writer.WriteUShort((ushort)objectIds[i]);
            }
        }
    }
}
