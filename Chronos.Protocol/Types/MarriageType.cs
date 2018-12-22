using Chronos.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Protocol.Types
{
    public class MarriageType
    {
        public bool enable;
        public byte kind;
        public uint marriage_stamp1;
        public uint divorce_stamp1;
        public uint current_state1;
        public int active1;
        public uint pid2;
        public uint marriage_stamp2;
        public uint divorce_stamp2;
        public uint current_state2;
        public int active2;
        public string name2;
        public int title;
        public int value;

        public MarriageType()
        {
            this.enable = false;
        }
        public MarriageType(byte kind = 0)
        {
            this.enable = true;
            this.kind = kind;
        }
        public MarriageType(byte kind, uint marriage_stamp1, uint divorce_stamp1, uint current_state1, int active1) : this(kind)
        {
            this.marriage_stamp1 = marriage_stamp1;
            this.divorce_stamp1 = divorce_stamp1;
            this.current_state1 = current_state1;
            this.active1 = active1;
        }
        public MarriageType(byte kind, uint marriage_stamp1, uint divorce_stamp1, uint current_state1, int active1, uint pid2, 
            uint marriage_stamp2, uint divorce_stamp2, uint current_state2, int active2, int title, int value) : this(kind, marriage_stamp1, divorce_stamp1,current_state1, active1)
        {
            this.marriage_stamp2 = marriage_stamp2;
            this.divorce_stamp2 = divorce_stamp2;
            this.current_state2 = current_state2;
            this.active2 = active2;
            this.title = title;
            this.value = value;
        }
        public void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(enable);
            if (!enable)
                return;
            writer.WriteByte(kind);
            if (kind <= 0)
                return;
            if (kind >= 1)
            {
                writer.WriteUInt(marriage_stamp1);
                writer.WriteUInt(divorce_stamp1);
                writer.WriteUInt(current_state1);
                writer.WriteInt(active1);
            }
            if(kind == 2)
            {
                writer.WriteUInt(marriage_stamp2);
                writer.WriteUInt(divorce_stamp2);
                writer.WriteUInt(current_state2);
                writer.WriteInt(active2);
                writer.WriteInt(title);
                writer.WriteInt(value);

            }
        }
    }
}
