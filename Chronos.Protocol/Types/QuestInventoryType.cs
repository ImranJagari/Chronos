using Chronos.Core.IO;
using Chronos.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Chronos.Protocol.Types
{
    public class QuestInventoryType
    {
        public short item_max;
        public short index_num;
        public ushort[] itemIds;
        public int itemElement_count;
        public ItemElementType[] item_elements;
        public short[] objectIds;

        public QuestInventoryType(ushort[] itemIds, int itemElement_count, ItemElementType[] item_elements, short[] objectIds)
        {
            item_max = (short)DefineEnum.MAX_INVENTORY;
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
            foreach (ushort itemId in itemIds)
            {
                writer.WriteUShort((ushort)itemId);
            }
            writer.WriteInt((int)itemElement_count);
            for (int i = 0; i < itemElement_count; i++)
            {
                item_elements[i].Serialize(writer);
                writer.WriteUShort((ushort)objectIds[i]);
            }
        }
    }
}