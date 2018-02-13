using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Protocol.Enums
{
    /// <summary>
    /// cFlyff State messages
    /// </summary>
    public enum StateMessageEnum : byte
    {
        SYS_SET_STATE = 0,
        SYS_DEL_STATE = 1,
        MSG_INITIAL = 3,
        MSG_DESTORY = 4,
        MSG_UPDATE = 6,
        MSG_SETDATA = 7,
        MSG_USER = 10,
        MSG_STAND_UP = 11,
        MSG_SIT_DOWN = 12,
        MSG_BE_PUSH = 13,
        MSG_START = 14,
        MSG_STOP = 15,
        MSG_OVER = 16,
        MSG_CHANGE_RANGE = 17,
        MSG_ASK_SKILL = 18,
        MSG_HIT = 19,
        MSG_BUFF = 20,
        MSG_COLLECT = 21,
        MSG_PICKUP = 22,
        MSG_DOPICKUP = 23,
        MSG_MARKPOS = 24,
        MSG_ADDSKILL = 25,
        MSG_DELSKILL = 26,
        MSG_REVIVAL = 27,
        MSG_IALINK = 28,
        MSG_NEXTPOINT = 29,
        MSG_ADV_STA_RECOVER = 30,
        MSG_AUTO_ASSIGN = 31,
        MSG_SET_SPEED = 32,
        MSG_GAME_TIP = 33,
        MSG_VOBJ = 34,
        MSG_FLYHEIGHT = 35,
        MSG_WORLD_GONE = 36,
        MSG_SETSCALE = 37,
        MSG_LINK = 38,
        MSG_TRIGGER = 39,
        MSG_SHIPSTOP = 40,
        MSG_GRASS = 41,
        MSG_REPLACE = 42,
        MSG_CANNON = 43,
        MSG_RESPAWN = 44,
        MSG_MAX = 45
    }

}


