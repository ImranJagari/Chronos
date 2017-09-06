using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos.Protocol.Enums
{
    public enum FriendStateEnum
    {
        FRS_ONLINE = 0,
        FRS_OFFLINE = 1,
        FRS_BLOCK = 2,
        FRS_ABSENT = 3,
        FRS_HARDPLAY = 4,
        FRS_EAT = 5,
        FRS_REST = 6,
        FRS_MOVE = 7,
        FRS_DIE = 8,
        FRS_DANGER = 9,
        FRS_OFFLINEBLOCK = 10,
        FRS_AUTOABSENT = 11,
        FRS_MURDERER = 12,
        MAX_FRIENDSTAT = 13
    }
}
