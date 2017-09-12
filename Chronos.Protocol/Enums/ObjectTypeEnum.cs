namespace Chronos.Protocol.Enums
{
    /// <summary>
    /// Describes the object types available in CFlyff.
    /// </summary>
    public enum ObjectTypeEnum
    {
        OT_OBJ = 1,
        OT_STATIC = 2,
        OT_CTRL = 4,
        OT_COMMON = 8,
        OT_ITEM = 16,
        OT_SFX = 32,
        OT_SHIP = 64,
        OT_SPRITE = 128,
        OT_PLAYER = 256,
        OT_CLIENT = OT_OBJ | OT_CTRL | OT_SPRITE | OT_PLAYER,
        OT_MONSTER = 512,
        OT_MOB = OT_OBJ | OT_CTRL | OT_SPRITE | OT_MONSTER,
        OT_NPC = 1024,
        OT_NPC2 = OT_OBJ | OT_CTRL | OT_SPRITE | OT_MONSTER | OT_NPC,
        OT_GUARD = 2048,
        OT_PET = 4096,
        OT_REGION = 8192,
        OT_COMMON_SFX = 16384,
        OT_SHOP = 32768,
        OT_BODYGUARD = 65536,
        OT_TRAINBALL = 131072,
        MAX_OBJTYPE = 18
    }
}