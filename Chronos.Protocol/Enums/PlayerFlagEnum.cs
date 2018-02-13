using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Chronos.Protocol.Enums
{
    public enum PlayerFlagEnum
    {
        flag_base = 0x77777777,
        flag_extend = 0x11111111,
        flag_inventory = 0x22222222,
        flag_taskbar = 0x33333333,
        flag_quest = 0x44444444,
        flag_messenger = 0x55555555,
        flag_skill = 0x66666666,
        flag_tbag = 0x7777777a,
        flag_credit = 0x7777777b,
        flag_faction = 0x7777777c,
        flag_lover = 0x77777790,
        flag_closet = 0x77777791,
        flag_vessel = 0x77777792,
        flag_hotkey = 0x77777793,
        flag_magic_t = 0x77777794,
        flag_advent = 0x77777795,
        flag_domesticate = 0x77777796,
        flag_expedition = 0x77777797
    }
}