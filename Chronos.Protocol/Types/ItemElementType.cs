using Chronos.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Protocol.Types
{
    public class ItemElementType
    {
        public ushort objectId;
        public uint itemId;
        public uint serial_number;
        public int item_num/*WriteX*/;
        public int hitpoint /*WriteX*/;
        public int max_hitpoint/*WriteX*/;
        public uint word;
        public byte ability_option;
        public byte item_resist;
        public byte item_resist_ability_option;
        public int keep_time /*WriteX*/;
        public byte itemlock;
        public int bindEndTime;
        public byte stability;
        public byte quality;
        public sbyte ability_rate;
        public int useTime/*WriteX*/;
        public int buyTm/*WriteX*/;
        public int price /*WriteX*/;
        public int payyb/*WriteX*/;
        public int freeyb/*WriteX*/;
        public int serverId;

        public short[] attributes;

        public ItemElementType(ushort objectId, uint itemId, uint serial_number, int item_num/*WriteX*/, int hitpoint /*WriteX*/,int max_hitpoint/*WriteX*/, uint word,
            byte ability_option, byte item_resist, byte item_resist_ability_option, int keep_time /*WriteX*/, byte itemlock, int bindEndTime, byte stability, byte quality,
            sbyte ability_rate, int useTime/*WriteX*/, int buyTm/*WriteX*/, int price /*WriteX*/, int payyb/*WriteX*/, int freeyb/*WriteX*/, int serverId, short[] attributes)
        {
            this.objectId = objectId;
            this.itemId = itemId;
            this.serial_number = serial_number;
            this.item_num = item_num;
            this.hitpoint = hitpoint;
            this.max_hitpoint = max_hitpoint;
            this.word = word;
            this.ability_option = ability_option;
            this.item_resist = item_resist;
            this.item_resist_ability_option = item_resist_ability_option;
            this.keep_time = keep_time;
            this.itemlock = itemlock;
            this.bindEndTime = bindEndTime;
            this.stability = stability;
            this.quality = quality;
            this.ability_rate = ability_rate;
            this.useTime = useTime;
            this.buyTm = buyTm;
            this.price = price;
            this.payyb = payyb;
            this.freeyb = freeyb;
            this.serverId = serverId;
        }
        public void Serialize(IDataWriter writer)
        {
            writer.WriteUShort(objectId);
            writer.WriteUInt(itemId);
            writer.WriteUInt(serial_number);
            writer.WriteX(item_num);
            writer.WriteX(hitpoint);
            writer.WriteX(max_hitpoint);
            writer.WriteUInt(word);
            writer.WriteByte(ability_option);
            writer.WriteByte(item_resist);
            writer.WriteByte(item_resist_ability_option);
            writer.WriteX(keep_time);
            writer.WriteByte(itemlock);
            writer.WriteInt(bindEndTime);
            writer.WriteByte(stability);
            writer.WriteByte(quality);
            writer.WriteSByte(ability_rate);
            writer.WriteX(useTime);
            writer.WriteX(buyTm);
            writer.WriteX(price);
            writer.WriteX(payyb);
            writer.WriteX(freeyb);
            writer.WriteInt(serverId);
            writer.WriteUShort((ushort)attributes.Length);
            foreach (short attribute in attributes)
                writer.WriteShort(attribute);
        }
    }
}
