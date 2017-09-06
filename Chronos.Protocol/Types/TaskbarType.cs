using Chronos.Core.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Chronos.Protocol.Types
{
    public class TaskbarType
    {
        public int count_left;
        public ShortcutType[] left_shortcut;
        public int count_right;
        public ShortcutType[] right_shortcut;
        public int count_sub;
        public ShortcutType[] sub_shortcut;
        public int count_show;
        public byte[] show_data;
        public int lock_check;

        public TaskbarType(int count_left, ShortcutType[] left_shortcut, int count_right, ShortcutType[] right_shortcut,
            int count_sub, ShortcutType[] sub_shortcut, int count_show, byte[] show_data, int lock_check)
        {
            this.count_left = count_left;
            this.left_shortcut = left_shortcut;
            this.count_right = count_right;
            this.right_shortcut = right_shortcut;
            this.count_sub = count_sub;
            this.sub_shortcut = sub_shortcut;
            this.count_show = count_show;
            this.show_data = show_data;
            this.lock_check = lock_check;
        }
        public void Serialize(IDataWriter writer)
        {
            writer.WriteInt(count_left);
            foreach (ShortcutType shortcut in left_shortcut)
                shortcut.Serialize(writer);
            writer.WriteInt(count_right);
            foreach (ShortcutType shortcut in right_shortcut)
                shortcut.Serialize(writer);
            writer.WriteInt(count_sub);
            foreach (ShortcutType shortcut in sub_shortcut)
                shortcut.Serialize(writer);
            writer.WriteInt(count_show);
            foreach (byte show in show_data)
                writer.WriteByte(show);
            writer.WriteInt(lock_check);
        }
    }
}