using Chronos.Core.IO;
using Chronos.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Chronos.Protocol.Types
{
    public class HotkeyType
    {
        public HotkeyEnum[] vk1;
        public HotkeyEnum[] vk2;
        public HotkeyType(HotkeyEnum[] vk1, HotkeyEnum[] vk2)
        {
            this.vk1 = vk1;
            this.vk2 = vk2;
        }
        public void Serialize(IDataWriter writer)
        {
            for(int i = 0; i < 47; i++)
            {
                writer.WriteInt((int)vk1[i]);
                writer.WriteInt((int)vk2[i]);
            }
        }
    }
}