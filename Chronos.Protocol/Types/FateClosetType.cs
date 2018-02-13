using Chronos.Core.Extensions;
using Chronos.Core.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Chronos.Protocol.Types
{
    public class FateClosetType
    {
        public int closet_index;
        public int index;
        public int level;
        public int equipped;
        public int[] closet_params;

        public FateClosetType(int closet_index, int index, int level, int equipped, int[] closet_params)
        {
            this.closet_index = closet_index;
            this.index = index;
            this.level = level;
            this.equipped = equipped;
            this.closet_params = closet_params;
        }
        public void Serialize(IDataWriter writer)
        {
            
            writer.WriteInt(closet_index);
            writer.WriteInt(index);
            writer.WriteInt(level);
            writer.WriteInt(equipped);
            //writer.WriteInt(/*DateTime.Now.GetUnixTimeStamp()*/0);
            foreach (int param in closet_params)
                writer.WriteInt(param);
        }
    }
}