using Chronos.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Chronos.Core.IO;

namespace Chronos.Protocol.Types.ObjectsType
{
    public class SpriteObjectType : ObjectType
    {
        public string name;

        public int count_data;
        public ushort[] rIndexes_data;
        public int[] values_data; //WriteX
        public byte count_buff;
        public byte[] buffsns;
        public int[] buffIds;
        public int[] ticks;

        public SpriteObjectType(ObjectTypeEnum objectType, uint objectId, uint id, uint linkId, float x, float y, float z,
            float angle, float angleX, short scale, string name, int count_data, ushort[] rIndexes_data, int[] values_data, byte count_buff,
            byte[] buffsns, int[] buffIds, int[] ticks) : base(objectType, objectId, id, linkId, x, y, z, angle, angleX, scale)
        {
            this.name = name;
            this.count_data = count_data;
            this.rIndexes_data = rIndexes_data;
            this.values_data = values_data;
            this.count_buff = count_buff;
            this.buffsns = buffsns;
            this.buffIds = buffIds;
            this.ticks = ticks;
        }

        public override void SerializeNeverChange(IDataWriter writer)
        {
            base.SerializeNeverChange(writer);
        }
        public override void SerializeSometimesChange(IDataWriter writer)
        {
            base.SerializeSometimesChange(writer);
            writer.WriteUTF(name);
        }
        public override void SerializeAlwaysChange(IDataWriter writer)
        {
            base.SerializeAlwaysChange(writer);

            writer.WriteUShort(9);
            writer.WriteBytes(new Byte[] { 0x76, 0x31, 0x24, 0x32, 0x34, 0x2C, 0x24, 0x24, 0x24 });

            writer.WriteInt(count_data);
            for(int i = 0; i < count_data; i++)
            {
                writer.WriteUShort((ushort)rIndexes_data[i]);
                writer.WriteX(values_data[i]);
            }
            writer.WriteByte(count_buff);
            for (int i = 0; i < count_buff; i++)
            {
                writer.WriteByte(buffsns[i]);
                writer.WriteInt(buffIds[i]);
                writer.WriteX(ticks[i]);
            }
        }
    }
}