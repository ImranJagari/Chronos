using Chronos.Core.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Chronos.Protocol.Types
{
    public class VesselType
    {
        public int level;
        public int slot;
        public int spirit;
        public int battle;
        public int wings_count;
        public int[] slots;
        public WingType[] wings;
        public VesselType(int level, int slot, int spirit, int battle, int wings_count, int[] slots, WingType[] wings)
        {
            this.level = level;
            this.slot = slot;
            this.spirit = spirit;
            this.battle = battle;
            this.wings_count = wings_count;
            this.slots = slots;
            this.wings = wings;
        }
        public void Serialize(IDataWriter writer)
        {
            writer.WriteInt(level);
            writer.WriteInt(slot);
            writer.WriteInt(spirit);
            writer.WriteInt(battle);
            writer.WriteInt(wings_count);
            for(int i = 0; i < wings_count; i++)
            {
                writer.WriteInt(slots[i]);
                wings[i].Serialize(writer);
            }
        }
    }
}