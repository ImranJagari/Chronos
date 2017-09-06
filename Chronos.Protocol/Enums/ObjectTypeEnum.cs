using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Chronos.Protocol.Enums
{
    public enum ObjectTypeEnum
    {
        OT_OBJ = 1,
        OT_CTRL = 4,
        OT_SFX = 32,
        OT_ITEM = 16,
        OT_COMMON = 8,
        OT_SHIP = 64,
        OT_STATIC = 2,
        OT_SPRITE = 128,
        OT_GUARD = 2048,
        OT_NPC = 1024,
        OT_MONSTER = 512,
        OT_PET = 4096,
        OT_REGION = 8192,
        OT_PLAYER = 256,
        OT_COMMON_SFX = 16384,
        OT_SHOP = 32768,
        OT_BODYGUARD = 65536,
        OT_TRAINBALL = 131072
    }
}