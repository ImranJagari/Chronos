using Chronos.Core.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Chronos.Protocol.Types
{
    public class WingType
    {
        public int index;
        public int rand_speed;
        public int add_time;
        public int[] attributes;
        public WingType(int index, int rand_speed, int add_time, int[] attributes)
        {
            this.index = index;
            this.rand_speed = rand_speed;
            this.add_time = add_time;
            this.attributes = attributes;
        }
        public void Serialize(IDataWriter writer)
        {
            writer.WriteInt(index);
            writer.WriteInt(rand_speed);
            writer.WriteInt(add_time);
            for(int i = 0; i < 3; i++)
                writer.WriteInt(attributes[i]);
        }
    }
}