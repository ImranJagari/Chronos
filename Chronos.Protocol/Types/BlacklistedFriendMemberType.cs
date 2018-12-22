using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronos.Core.IO;
using Chronos.Protocol.Enums;

namespace Chronos.Protocol.Types
{
    public class BlacklistedFriendMemberType : FriendMemberType
    {
        public int blackListedPlayerId;
        public string playerName;

        public BlacklistedFriendMemberType(int blackListerBlackListedPlayerId, string playerName, int playerId, byte sex, byte job, short level, FriendStateEnum state, int close_points, byte issf_, string name, string class_name) : base(playerId, sex, job, level, state, close_points, issf_, name, class_name)
        {
            this.blackListedPlayerId = blackListerBlackListedPlayerId;
            this.playerName = playerName;
        }

        public void Serialize(IDataWriter writer)
        {
            writer.WriteInt(blackListedPlayerId);
            writer.WriteUTF(playerName);
            base.Serialize(writer);
        }
    }
}
