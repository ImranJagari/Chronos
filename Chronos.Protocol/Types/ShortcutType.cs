using Chronos.Core.IO;
using Chronos.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Chronos.Protocol.Types
{
    public class ShortcutType
    {
        public int index_line;
        public int index_column;
        public ShortcutEnum shortcutType;
        public int id;
        public int typeId;
        public int index;
        public int userId;
        public int data;
        public string string_data;
        public ShortcutType(int index_line, int index_column, ShortcutEnum shortcutType, int id, int typeId, int index, int userId, int data, string string_data)
        {
            this.index_line = index_line;
            this.index_column = index_column;
            this.shortcutType = shortcutType;
            this.id = id;
            this.typeId = typeId;
            this.index = index;
            this.userId = userId;
            this.data = data;
            this.string_data = string_data;
        }
        public void Serialize(IDataWriter writer)
        {
            writer.WriteInt(index_line);
            writer.WriteInt(index_column);
            writer.WriteInt((int)shortcutType);
            writer.WriteInt(id);
            writer.WriteInt(typeId);
            writer.WriteInt(index);
            writer.WriteInt(userId);
            writer.WriteInt(data);
            writer.WriteUTF(string_data);
        }
    }
}