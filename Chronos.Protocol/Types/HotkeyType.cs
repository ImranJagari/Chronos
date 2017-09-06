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
        public int hotkey_count;
        public HotkeyEnum[] vk1;
        public HotkeyEnum[] vk2;
        public HotkeyType(int hotkey_count, HotkeyEnum[] vk1, HotkeyEnum[] vk2)
        {
            this.hotkey_count = hotkey_count;
            this.vk1 = vk1;
            this.vk2 = vk2;
        }
        public void Serialize(IDataWriter writer)
        {
            writer.WriteInt(hotkey_count);
            for(int i = 0; i < hotkey_count; i++)
            {
                writer.WriteInt((int)vk1[i]);
                writer.WriteInt((int)vk2[i]);
            }
        }
    }
}