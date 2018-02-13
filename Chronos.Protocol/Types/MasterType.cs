using Chronos.Core.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Chronos.Protocol.Types
{
    public class MasterType
    {
        public string master_name;
        public string master_function_name;
        public string function_name;
        public int close_points_total_with_master;
        public int safety_immunity;
        public int lover_count;

        public MasterType(string master_name, string master_function_name, string function_name, int close_points_total_with_master, int safety_immunity, int lover_count)
        {
            this.master_name = master_name;
            this.master_function_name = master_function_name;
            this.function_name = function_name;
            this.close_points_total_with_master = close_points_total_with_master;
            this.safety_immunity = safety_immunity;
            this.lover_count = lover_count;
        }

        public void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(master_name);
            writer.WriteUTF(master_function_name);
            writer.WriteUTF(function_name);
            writer.WriteInt(close_points_total_with_master);
            writer.WriteUInt((uint)safety_immunity);
            writer.WriteByte((byte)lover_count);
        }
    }
}