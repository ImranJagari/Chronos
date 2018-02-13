using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Protocol.Enums
{
    /// <summary>
    /// cFlyff State types
    /// </summary>
    public enum StateTypeEnum : ushort
    {
        STATE_NONE = 0,
        STATE_SIT = 1,
        STATE_MELEE_ATK = 2,
        STATE_SKILLING = 3,
        STATE_CASTING = 4,
        STATE_FSKILLING = 5,
        STATE_FCASTING = 6,
        STATE_STAND_G = 7,
        STATE_STAND_F = 8,
        STATE_STUN = 9,
        STATE_FALL = 10,
        STATE_JUMP = 11,
        STATE_MOVE_G = 12,
        STATE_MOVE_F = 13,
        STATE_MOVE_TO = 14,
        STATE_TRANSFORM = 15,
        STATE_AIRBOAT = 16,
        STATE_SNEAK = 17,
        STATE_FREEZE = 18,
        STATE_CHAOS = 19,
        STATE_PUSH = 20,
        STATE_MOTION = 21,
        STATE_DAMAGE = 22,
        STATE_FLY = 23,
        STATE_GROUND = 24,
        STATE_COMBAT = 25,
        STATE_DEAD = 26,
        STATE_COLLECT = 27,
        STATE_TRADE = 28,
        STATE_LINK = 29,
        STATE_NAVIGATE = 30,
        STATE_QUIZ = 31,
        STATE_MAX = 32,
        STATE_GUARD = 100
    }

}
